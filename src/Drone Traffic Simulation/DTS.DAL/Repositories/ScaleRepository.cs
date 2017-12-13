using DTS.DAL.Domain;
using DTS.DAL.IRepositories;

namespace DTS.DAL.Repositories
{
    //Extends generic repository
    public class ScaleRepository: Repository<Scale>, IScaleRepository
    {
        public ScaleRepository(DtsContext context) : base(context)
        {
        }

        public DtsContext DtsContext => Context as DtsContext;

    }
}