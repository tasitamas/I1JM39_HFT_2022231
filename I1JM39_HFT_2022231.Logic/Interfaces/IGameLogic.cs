using I1JM39_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;
using static I1JM39_HFT_2022231.Logic.GameLogic;

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
        public IEnumerable<GameInfo> OldestGameWithDeveloperName();
        public IEnumerable<GameInfo> YoungestGameWithDeveloperName();
        public IEnumerable<object> OlderThan10YearsGames();
        public IEnumerable<object> HighestRatingGameWithDevName();
        public IEnumerable<object> GamesWithNpc();
        public IEnumerable<object> FreeGames();
    }
}