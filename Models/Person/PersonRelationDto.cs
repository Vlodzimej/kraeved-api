namespace KraevedAPI.Models
{
    public class PersonRelationDto
    {
        public int PersonId { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public List<ImageInfoDto>? Photos { get; set; }
        public string? RelationTitle { get; set; }
    }
}
