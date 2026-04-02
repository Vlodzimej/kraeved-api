namespace KraevedAPI.Models
{
    /// <summary>
    /// Связь между персоной и гео-объектом (многие-ко-многим)
    /// </summary>
    public class PersonGeoObject
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int GeoObjectId { get; set; }
        public GeoObject? GeoObject { get; set; }
    }
}
