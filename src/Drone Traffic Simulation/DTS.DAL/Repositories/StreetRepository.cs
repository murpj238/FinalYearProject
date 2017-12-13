using DTS.DAL.Domain;
using DTS.DAL.IRepositories;

namespace DTS.DAL.Repositories
{
    //Extends generic repository
    public class StreetRepository: Repository<Street>, IStreetRepository
    {
        public StreetRepository(DtsContext context) : base(context)
        {
        }
        private DtsContext DtsContext => Context as DtsContext;

    }
}
