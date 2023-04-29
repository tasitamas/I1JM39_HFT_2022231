using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231_Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace I1JM39_HFT_2022231_Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        IDeveloperLogic logic;
        IHubContext<SignalRHub> hub;

        public DeveloperController(IDeveloperLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        #region CRUD
        [HttpPost]
        public void Create([FromBody] Developer value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("DeveloperCreated", value);
        }
        [HttpGet]
        public IEnumerable<Developer> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Developer Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] Developer value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("DeveloperUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var devToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("DeveloperDeleted", devToDelete);
        }

        #endregion
    }
}
