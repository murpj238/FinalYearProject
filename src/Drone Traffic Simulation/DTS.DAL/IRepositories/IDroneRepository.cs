using System.Data.Entity.Infrastructure;
using DTS.DAL.Domain;

namespace DTS.DAL.IRepositories
{
    public interface IDroneRepository: IRepository<Drone>
    {
        Drone GetRandom();

        void ReseedIdColumn();
    }
}