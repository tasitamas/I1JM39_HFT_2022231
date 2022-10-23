using I1JM39_HFT_2022231.Models;
using System.Linq;

namespace I1JM39_HFT_2022231.Logic
{
    public interface IGameLogic
    {
        //CRUD Methods
        void Create(Game item);
        void Delete(int id);
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game item);

        //Non CRUD Methods
    }
}