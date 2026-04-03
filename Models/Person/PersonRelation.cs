using System.Text.Json.Serialization;

namespace KraevedAPI.Models
{
    /// <summary>
    /// Родственная связь между двумя персонами (многие-ко-многим с типом)
    /// </summary>
    public class PersonRelation
    {
        /// <summary>
        /// Идентификатор персоны 1
        /// </summary>
        public int PersonId1 { get; set; }

        [JsonIgnore]
        public Person? Person1 { get; set; }

        /// <summary>
        /// Идентификатор персоны 2
        /// </summary>
        public int PersonId2 { get; set; }

        [JsonIgnore]
        public Person? Person2 { get; set; }

        /// <summary>
        /// Идентификатор типа связи
        /// </summary>
        public int RelationTypeId { get; set; }

        public PersonRelationType? RelationType { get; set; }
    }
}
