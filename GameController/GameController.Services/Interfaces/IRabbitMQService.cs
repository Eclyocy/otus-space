using GameController.Services.Models.Message;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with Rabbit MQ.
    /// </summary>
    public interface IRabbitMQService
    {
        /// <summary>
        /// Send a message to a queue in Rabbit MQ.
        /// </summary>
        void SendNewDayMessage(NewDayMessage newDayMessage);
    }
}
