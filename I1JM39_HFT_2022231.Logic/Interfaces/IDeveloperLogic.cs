using I1JM39_HFT_2022231.Models;
using System.Linq;

namespace I1JM39_HFT_2022231.Logic
{
    public interface IDeveloperLogic
    {
        //CRUD Methods
        void Create(Developer item);
        void Delete(int id);
        Developer Read(int id);
        IQueryable<Developer> ReadAll();
        void Update(Developer item);
    }
}