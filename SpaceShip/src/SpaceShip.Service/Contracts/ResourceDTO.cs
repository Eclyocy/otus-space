    
    namespace SpaceShip.Service.Contracts;
    public class ResourceDTO
    {
        /// <summary>
        /// Id ресурса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименованеи ресурса
        /// </summary>
        public string ? Name { get; set; }

        /// <summary>
        /// Состояние ресурса (спит, норма, проблема)
        /// </summary>
        public required string State { get; set; }

    }