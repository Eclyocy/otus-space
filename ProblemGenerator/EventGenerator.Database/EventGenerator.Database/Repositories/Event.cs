namespace EventGenerator.Database.Entity
{
    public class Event
    {
        public string Id { get; set; }
        public Guid IdShip { get; set; }
        public int Volume { get; set; }
        public int IdType { get; set; }
    }
}
