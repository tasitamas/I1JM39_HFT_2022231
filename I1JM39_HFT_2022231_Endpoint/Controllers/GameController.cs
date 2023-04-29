using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231_Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace I1JM39_HFT_2022231_Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameLogic logic;
        IHubContext<SignalRHub> hub;

        public GameController(IGameLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        #region CRUD
        [HttpPost]
        public void Create([FromBody] Game value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("GameCreated", value);
        }
        [HttpGet]
        public IEnumerable<Game> ReadAll()
        { 
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Game Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] Game value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GameUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GameDeleted", gameToDelete);

        }

        #endregion

    }
}
