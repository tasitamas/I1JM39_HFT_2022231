using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace I1JM39_HFT_2022231_Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameLogic logic;

        public GameController(IGameLogic logic)
        {
            this.logic = logic;
        }

        #region CRUD
        [HttpPost]
        public void Create([FromBody] Game value)
        {
            this.logic.Create(value);
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
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }

        #endregion

    }
}
