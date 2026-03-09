namespace KraevedAPI.Constants
{

    public static class Roles
    {
        /// <summary>
        /// Уровень доступа: Администратор
        /// </summary>
        public static class Unknown
        {
            private static readonly int _id = 0;
            public static int Id { get { return _id; } }

            private static readonly string _name = new("UNKNOWN");
            public static string Name { get { return _name; } }
        }

        /// <summary>
        /// Уровень доступа: Администратор
        /// </summary>
        public static class Admin
        {
            private static readonly int _id = 1;
            public static int Id { get { return _id; } }

            private static readonly string _name = new("ADMIN");
            public static string Name { get { return _name; } }
        }

        /// <summary>
        /// Уровень доступа: Пользователь
        /// </summary>
        public static class User
        {
            private static readonly int _id = 2;
            public static int Id { get { return _id; } }

            private static readonly string _name = new("USER");
            public static string Name { get { return _name; } }
        }
    }
}