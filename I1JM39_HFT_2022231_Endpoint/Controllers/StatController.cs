using I1JM39_HFT_2022231.Logic;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<object> HighestRatedGame()
        {
            return this.logic.HighestRatingGameWithDevName();
        }
        [HttpGet]
        public IEnumerable<object> LowestRatedGame()
        {
            return this.logic.LowestRatingGameWithDevName();
        }
        [HttpGet]
        public IEnumerable<object> OlderThan10()
        {
            return this.logic.OlderThan10YearsGames();
        }
        [HttpGet]
        public IEnumerable<object> GamesWithNpc()
        {
            return this.logic.GamesWithNpc();
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
        public IEnumerable<object> FreeGames()
        {
            return this.logic.FreeGames();
        }
        [HttpGet]
        public IEnumerable<object> PaidGames()
        {
            return this.logic.PaidGames();
        }
        #endregion
    }
}
