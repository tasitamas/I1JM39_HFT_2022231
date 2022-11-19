using I1JM39_HFT_2022231.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace I1JM39_HFT_2022231_Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic logic;

        public StatController(IGameLogic logic)
        {
            this.logic = logic;
        }

        #region Non CRUD
        [HttpGet]
        public IEnumerable<KeyValuePair<string,string>> HighestRatedGame()
        {
            return this.logic.HighestRatingGameWithDevName();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> LowestRatedGame()
        {
            return this.logic.LowestRatingGameWithDevName();
        }
        
        [HttpGet]
        public IEnumerable<GameInfo> OlderThan10()
        {
            return this.logic.OlderThan10YearsGames();
        }
        
        [HttpGet]
        public IEnumerable<KeyValuePair<string,int>> GamesCharactersCount()
        {
            return this.logic.GamesCharactersCount();
        }
        
        [HttpGet]
        public IEnumerable<object> OldestGame()
        {
            return this.logic.OldestGameWithDeveloperName();
        }
        [HttpGet]
        public IEnumerable<object> YoungestGame()
        {
            return this.logic.YoungestGameWithDeveloperName();
        }
        
        [HttpGet]
        public IEnumerable<RatingInfo> FreeGames()
        {
            return this.logic.FreeGames();
        }
        [HttpGet]
        public IEnumerable<RatingInfo> PaidGames()
        {
            return this.logic.PaidGames();
        }
        #endregion
    }
}
