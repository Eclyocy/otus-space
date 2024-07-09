

using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.ProblemServices
{
    public interface IProblemService
    {
        public ProblemDTO Create(ProblemDTO model);
    }
}
