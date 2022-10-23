using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using System;
using System.Linq;

namespace I1JM39_HFT_2022231.Logic
{
    public class GameLogic : IGameLogic
    {
        IRepository<Game> repo;

        public GameLogic(IRepository<Game> repo)
        {
            this.repo = repo;
        }

        //CRUD Methods
        public void Create(Game item)
        {
            this.repo.Create(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
        public Game Read(int id)
        {
            var game = this.repo.Read(id);
            if (game == null)
            {
                throw new ArgumentException("This game doesn't exist.");
            }
            return game;
        }
        public IQueryable<Game> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Game item)
        {
            this.repo.Update(item);
        }

        //Non CRUD Methods
    }
}
