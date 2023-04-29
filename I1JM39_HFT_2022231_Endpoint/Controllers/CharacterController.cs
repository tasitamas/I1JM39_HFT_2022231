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
    public class CharacterController : ControllerBase
    {
        ICharacterLogic logic;
        IHubContext<SignalRHub> hub;

        public CharacterController(ICharacterLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        #region CRUD
        [HttpPost]
        public void Create([FromBody] Character value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CharacterCreated", value);
        }
        [HttpGet]
        public IEnumerable<Character> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Character Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] Character value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CharacterUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var charToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CharacterDeleted", charToDelete);
        }

        #endregion
    }
}
