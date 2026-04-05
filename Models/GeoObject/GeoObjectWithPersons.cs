namespace KraevedAPI.Models
{
    public class GeoObjectWithPersons
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? RegionId { get; set; }
        public int? TypeId { get; set; }
        public GeoObjectTypeBriefDto? Type { get; set; }
        public List<ImageInfoDto>? Images { get; set; }
        public string? Thumbnail { get; set; }
        public List<PersonBriefDto>? Persons { get; set; }
    }

    public class PersonBriefDto
    {
        public int? Id { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public List<ImageInfoDto>? Photos { get; set; }
    }

    public class GeoObjectTypeBriefDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
    }
}
