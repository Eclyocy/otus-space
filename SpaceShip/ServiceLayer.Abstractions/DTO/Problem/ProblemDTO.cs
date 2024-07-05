using ServiceLayer.Abstractions.ReturnResult;

namespace Spaceship.DataLayer.EfClasses
{
    public class ProblemDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProblemDTO()
        {
                
        }
    }
}
