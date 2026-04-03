namespace KraevedAPI.Models
{
    public class PersonTreeNode
    {
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public List<string>? Photos { get; set; }
        public PersonTreeNode? Father { get; set; }
        public PersonTreeNode? Mother { get; set; }
        public List<PersonRelationDto>? Spouses { get; set; }
        public List<PersonRelationDto>? Children { get; set; }
        public List<PersonRelationDto>? Siblings { get; set; }
    }
}
