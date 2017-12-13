using DTS.DAL.Domain;
using DTS.DAL.IRepositories;

namespace DTS.DAL.Repositories
{
    //Extends generic repository
    public class StatisticsRepository : Repository<Statistic>, IStatisticsRepository
    {
        public StatisticsRepository(DtsContext context) : base(context)
        {
        }

        public DtsContext DtsContext => Context as DtsContext;
    }
}
