using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces
{
    public interface IProblemService
    {
        public ProblemDTO Create(ProblemDTO model);
    }
}
