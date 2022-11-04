using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace I1JM39_HFT_2022231.Logic
{
    public class GameLogic : IGameLogic
    {
        IRepository<Game> gameRepo;
        IRepository<Developer> devRepo;
        IRepository<Character> charRepo;
        public GameLogic(IRepository<Game> repo, IRepository<Developer> devRepo, IRepository<Character> charRepo)
        {
            this.gameRepo = repo;
            this.devRepo = devRepo;
            this.charRepo = charRepo;
        }


        //CRUD Methods
        public void Create(Game item)
        {
            this.gameRepo.Create(item);
        }
        public void Delete(int id)
        {
            this.gameRepo.Delete(id);
        }
        public Game Read(int id)
        {
            var game = this.gameRepo.Read(id);
            if (game == null)
            {
                throw new ArgumentException("This game doesn't exist.");
            }
            return game;
        }
        public IQueryable<Game> ReadAll()
        {
            return this.gameRepo.ReadAll();
        }
        public void Update(Game item)
        {
            this.gameRepo.Update(item);
        }

        //Non CRUD Methods
        public IEnumerable<GameInfo> OldestGameWithDeveloperName()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   let minAge = gameRepo.ReadAll().Min(t => t.Release.Year)
                   where g.DeveloperId == d.DeveloperId && g.Release.Year == minAge
                   select new GameInfo()
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Age = (int)DateTime.Now.Year - (int)g.Release.Year,
                   };
        }
        public IEnumerable<GameInfo> YoungestGameWithDeveloperName()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   let maxAge = gameRepo.ReadAll().Max(t => t.Release.Year)
                   where g.DeveloperId == d.DeveloperId && g.Release.Year == maxAge
                   select new GameInfo()
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Age = (int)DateTime.Now.Year - (int)g.Release.Year,
                   };
        }
        public IEnumerable<object> OlderThan10YearsGames()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   where g.DeveloperId == d.DeveloperId
                   && ((int)DateTime.Now.Year - (int)g.Release.Year > 10)
                   select new
                   {
                       DevName = d.DeveloperName,
                       GameName = g.GameName,
                   };
        }
        public IEnumerable<object> HighestRatingGameWithDevName()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   let maxRating = gameRepo.ReadAll().Max(t => t.Rating)
                   where g.DeveloperId == d.DeveloperId && g.Rating == maxRating
                   select new
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Rating = g.Rating,
                   };
        }
        public IEnumerable<object> GamesWithNpc()
        {
            return from g in this.gameRepo.ReadAll()
                   from c in this.charRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   where (c.Priority == 3 && c.Priority != 2 && c.Priority != 1) && c.GameId == g.GameId && d.DeveloperId == g.DeveloperId
                   select new
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                   };
        }
        public IEnumerable<object> FreeGames()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   where g.DeveloperId == d.DeveloperId && g.Income == 0
                   select new
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       ReleaseDate = g.Release,
                       Rating = g.Rating,
                   };
        }

        //Helping class
        public class GameInfo
        { 
            public string GameName { get; set; }
            public string DevName { get; set; }
            public int Age { get; set; }

            public override bool Equals(object obj)
            {
                GameInfo gameInfo = obj as GameInfo;
                if (gameInfo == null)
                {
                    return false;
                }
                else
                { 
                return this.GameName == gameInfo.GameName && this.DevName == gameInfo.DevName && this.Age == gameInfo.Age;
                }
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(this.GameName, this.DevName, this.Age);
            }
        }
    }
}
