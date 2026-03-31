using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KraevedAPI.ClassObjects;
using KraevedAPI.Constants;
using KraevedAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {

        public async Task<UserOutDto> Register(string? email, string? password) {
            var userEmail = email?.Trim();

            if (email?.Trim() == null) {
                throw new Exception(ServiceConstants.Exception.EmailIsEmpty);
            }

            if (password == null) {
                throw new Exception(ServiceConstants.Exception.InvalidPassword);
            }

            if (_unitOfWork.UsersRepository.Get(x => x.Email == userEmail).FirstOrDefault() != null) {
               throw new Exception(ServiceConstants.Exception.EmailExits); 
            }

            await CreateUser(null, userEmail, password);

            var user = _unitOfWork.UsersRepository.Get(x => x.Email == userEmail).FirstOrDefault();

            if (user == null) {
                throw new Exception(ServiceConstants.Exception.UserCreationError);
            }

            await UpdateUserPassword(user.Id, password);

            var userInfo = _mapper.Map<User, UserOutDto>(user);
            return userInfo;
        }

        /// <summary>
        /// Генерация кода подтверждения по номеру телефона 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<Boolean> SendSms(string phone)
        {
            // Проверяем номер телефона
            if (!VerifyPhone(phone))
            {
                throw new Exception(ServiceConstants.Exception.InvalidPhoneNumber);
            }

            // Ищем коды подтверждения по номеру телефона
            var oldSmsCodes = _unitOfWork.SmsCodesRepository.Get(x => x.Phone == phone) ?? throw new Exception(ServiceConstants.Exception.UnknownError);

            // Проверяем, что за последние 5 минут не было создано более 4 кодов подтверждения
            if (oldSmsCodes.Where(x => x.StartDate > DateTime.Now.AddMinutes(-ServiceConstants.Authentication.SmsCodeTimeout)).
                Count() >= ServiceConstants.Authentication.MaxSmsCodeAttempts)
            {
                throw new Exception(ServiceConstants.Exception.ManyLoginAttempts);
            }

            // Выставляем просроченный статус для старых кодов
            foreach (var oldSmsCode in oldSmsCodes)
            {
                oldSmsCode.IsInvalid = true;
                _unitOfWork.SmsCodesRepository.Update(oldSmsCode);
            }
            await _unitOfWork.SaveAsync();

            // Создаем новый код подтверждения
            var random = new Random();
            var smsCode = random.Next(1000, 9999);

            var smsCodeEntry = new SmsCode()
            {
                Phone = phone,
                Code = smsCode.ToString(),
                StartDate = DateTime.Now
            };

            // Сохраняем новый код подтверждения
            _unitOfWork.SmsCodesRepository.Insert(smsCodeEntry);
            await _unitOfWork.SaveAsync();

            // Отправляем код подтверждения на номер телефона
            var apiKey = _configuration.GetSection("Kraeved:SmsPilotApiKey").Value;
            var smsServiceUrl = _configuration.GetSection("ConnectionStrings:SmsService").Value;
            var url = $"{smsServiceUrl}&send={smsCode}&to={phone}&apikey={apiKey}";

            return true;
            // var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
            // {
            //     Headers = {
            //         { HeaderNames.Accept, "application/json" },
            //         { HeaderNames.UserAgent, "KraevedAPI/1.0" },
            //         { HeaderNames.AcceptCharset, "utf-8" }
            //     }
            // };

            // var httpClient = _httpClientFactory.CreateClient();
            // var httpResponse = await httpClient.SendAsync(httpRequestMessage);


            // if (httpResponse.IsSuccessStatusCode)
            // {
            //     return true;
            // }

            throw new Exception(ServiceConstants.Exception.SmsServiceError);
        }

        public async Task<LoginOutDto> Login(LoginInDto loginDto)
        {
            var phone = loginDto.Phone;
            var email = (loginDto.Email ?? "").ToLower();
            var code = loginDto.Code;
            var password = loginDto.Password;

            if (email != null && password != null && VerifyEmail(email)) {
                return await LoginByEmail(email, password);
            }

            if (!VerifyPhone(phone))
            {
                throw new Exception(ServiceConstants.Exception.InvalidPhoneNumber);
            }

            if (VerifyCode(code))
            {
                return await LoginByCode(phone, code);
            }
            else if (password != null)
            {
                return await LoginByPhone(phone, password);
            }

            throw new Exception(ServiceConstants.Exception.AuthorisationError);
        }

        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="loginDto">Данные для входа</param>
        private async Task<LoginOutDto> LoginByCode(string phone, string code)
        {
            var smsCode = _unitOfWork.SmsCodesRepository
                .Get(x => x.Phone == phone &&
                          x.Code == code &&
                          x.StartDate > DateTime.Now.AddMinutes(-ServiceConstants.Authentication.SmsCodeTimeout) &&
                          x.IsInvalid == false)
               .FirstOrDefault();

            if (!isDevelopment)
            {
                if (smsCode == null)
                {
                    throw new Exception(ServiceConstants.Exception.InvalidSmsCode);
                }

                RemoveSmsCodesByPhone(phone);
            }

            var password = Guid.NewGuid().ToString() ?? throw new Exception(ServiceConstants.Exception.UnknownError);

            var user = _unitOfWork.UsersRepository
                .Get(x => x.Phone == phone)
                .FirstOrDefault() ?? await CreateUser(phone, null, password);

            await UpdateUserPassword(user.Id, password);

            var loginOutDto = new LoginOutDto()
            {
                Password = password
            };

            return loginOutDto;
        }

        private async Task<LoginOutDto> LoginByPhone(string phone, string password)
        {
            if (!VerifyPhone(phone))
            {
                throw new Exception(ServiceConstants.Exception.InvalidPhoneNumber);
            }

            var user = _unitOfWork.UsersRepository.Get(x => x.Phone == phone).FirstOrDefault() ?? throw new HttpResponseException(401, new { message = ServiceConstants.Exception.UserNotFound });

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception(ServiceConstants.Exception.InvalidPassword);
            }

            var loginOutDto = new LoginOutDto()
            {
                Token = GetToken(user.Id, user.RoleId)
            };

            return await Task.FromResult(loginOutDto);
        }

        /// <summary>
        /// Создание нового пользователя по номеру телефона 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private async Task<User> CreateUser(String? phone, String? email, String password)
        {
            var (passwordHash, passwordSalt) = GeneratePasswordHash(password);
            var userRole = _unitOfWork.RolesRepository.GetRoleByName(Roles.User.Name);

            var user = new User()
            {
                Phone = phone ?? "",
                Email = email ?? "",
                Name = "",
                Surname = "",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                StartDate = DateTime.UtcNow,
                RoleId = userRole.Id
            };

            _unitOfWork.UsersRepository.Insert(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        /// <summary>
        /// Удаление всех кодов подтверждения пользователя, которые были созданы более 5 минут назад
        /// </summary>
        /// <returns></returns>
        private async void RemoveSmsCodesByPhone(string phone)
        {
            var smsCodes = _unitOfWork.SmsCodesRepository.Get(x => x.Phone == phone);
            foreach (var smsCode in smsCodes)
            {
                _unitOfWork.SmsCodesRepository.Delete(smsCode.Id);
            }
            await _unitOfWork.SaveAsync();
        }

        private string GetToken(int userId, int roleId)
        {
            var secretKey = _configuration.GetSection("Kraeved:Secret").Value;
            var role = _unitOfWork.RolesRepository.GetRoleById(roleId);

            if (secretKey == null || secretKey == "")
            {
                throw new Exception(ServiceConstants.Exception.InvalidSecretKey);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name),
                ]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// Создание хэша пароля
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new Exception(ServiceConstants.Exception.PasswordErrorSpaces);

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Верификация хэша пароля
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(ServiceConstants.Exception.EmptyStringValue, nameof(password));
            if (storedHash.Length != 64)
                throw new ArgumentException(ServiceConstants.Exception.InvalidPassword);
            if (storedSalt.Length != 128)
                throw new ArgumentException(ServiceConstants.Exception.InvalidPassword);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static (byte[] passwordHash, byte[] passwordSalt) GeneratePasswordHash(string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            return (passwordHash, passwordSalt);
        }

        private async Task<User> UpdateUserPassword(int id, string password)
        {
            var user = _unitOfWork.UsersRepository.GetByID(id) ?? throw new HttpResponseException(401, new { message = ServiceConstants.Exception.UserNotFound });
            var (passwordHash, passwordSalt) = GeneratePasswordHash(password);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return user;
        }

        private async Task<LoginOutDto> LoginByEmail(string email, string password) {
            if (!VerifyEmail(email))
            {
                throw new Exception(ServiceConstants.Exception.InvalidEmail);
            }

            var user = _unitOfWork.UsersRepository.Get(x => x.Email == email).FirstOrDefault() ?? throw new HttpResponseException(401, new { message = ServiceConstants.Exception.UserNotFound });

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception(ServiceConstants.Exception.InvalidPassword);
            }

            var loginOutDto = new LoginOutDto()
            {
                Token = GetToken(user.Id, user.RoleId)
            };

            return await Task.FromResult(loginOutDto);
        }

        private Boolean VerifyPhone(string? phone)
        {
            if (phone == null)
            {
                return false;
            }
            return phone.Trim().Length == ServiceConstants.Authentication.PhoneLength;
        }

        private Boolean VerifyEmail(string email) 
        {
            //TODO: Реализовать полную валидацию
            return !email.Trim().IsNullOrEmpty();
        }

        private Boolean VerifyCode(string? code)
        {
            if (code == null)
            {
                return false;
            }
            return code.Trim().Length == ServiceConstants.Authentication.CodeLength;
        }

    }
}