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
        public IEnumerable<object> OldestGameWithDeveloperName();
        public IEnumerable<object> YoungestGameWithDeveloperName();
        public IEnumerable<GameInfo> OlderThan10YearsGames();
        public IEnumerable<KeyValuePair<string, string>> HighestRatingGameWithDevName();
        public IEnumerable<KeyValuePair<string, string>> LowestRatingGameWithDevName();
        public IEnumerable<RatingInfo> FreeGames();
        public IEnumerable<RatingInfo> PaidGames();
        public IEnumerable<KeyValuePair<string, int>> GamesCharactersCount();
    }
}