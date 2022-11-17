using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
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

        public GameLogic(IRepository<Game> repo)
        {
            this.gameRepo = repo;
        }
        public GameLogic(IRepository<Game> repo, IRepository<Developer> devRepo, IRepository<Character> charRepo)
        {
            this.gameRepo = repo;
            this.devRepo = devRepo;
            this.charRepo = charRepo;
        }


        //CRUD Methods
        public void Create(Game item)
        {
            if (item.GameName == null)
            {
                throw new NullReferenceException();
            }
            else if (item.Price < 0 || item.Price > 20000)
            {
                throw new ArgumentOutOfRangeException("Not a correct price");
            }
            else if (item.Rating < 0 || item.Rating > 10)
            {
                throw new ArgumentOutOfRangeException("Not a correct rating");
            }
            else if (item.Release < 1900 || item.Release > 2030)
            {
                throw new ArgumentOutOfRangeException("Not a correct release date");
            }
            else
            {
                gameRepo.Create(item);
            }
        }
        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            { 
                gameRepo.Delete(id);
            }
        }
        public Game Read(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return gameRepo.Read(id);
            }
        }
        public IQueryable<Game> ReadAll()
        {
            return this.gameRepo.ReadAll();
        }
        public void Update(Game item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else if (item.GameName == null)
            {
                throw new NullReferenceException();
            }
            else if (item.Price < 0 || item.Price > 20000)
            {
                throw new ArgumentOutOfRangeException("Not a correct price");
            }
            else if (item.Rating < 0 || item.Rating > 10)
            {
                throw new ArgumentOutOfRangeException("Not a correct rating");
            }
            else if (item.Release < 1900 || item.Release > 2030)
            {
                throw new ArgumentOutOfRangeException("Not a correct release date");
            }
            else
            {
                gameRepo.Update(item);
            }
        }

        //Non CRUD Methods
        public IEnumerable<BasicGameInfo> OldestGameWithDeveloperName()
        {
            var q = from g in this.gameRepo.ReadAll()
                    from d in this.devRepo.ReadAll()
                    let minAge = gameRepo.ReadAll().Min(t => t.Release)
                    where g.DeveloperId == d.DeveloperId && g.Release == minAge
                    select new BasicGameInfo()
                    {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Age = (int)DateTime.Now.Year - (int)g.Release,
                    };
            return q.ToList();
        }
        public IEnumerable<BasicGameInfo> YoungestGameWithDeveloperName()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   let maxAge = gameRepo.ReadAll().Max(t => t.Release)
                   where g.DeveloperId == d.DeveloperId && g.Release == maxAge
                   select new BasicGameInfo()
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Age = (int)DateTime.Now.Year - (int)g.Release,
                   };
        }
        public IEnumerable<BasicGameInfo> OlderThan10YearsGames()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   where g.DeveloperId == d.DeveloperId
                   && ((int)DateTime.Now.Year - (int)g.Release > 10)
                   select new BasicGameInfo()
                   {
                       DevName = d.DeveloperName,
                       GameName = g.GameName,
                       Age = (int)DateTime.Now.Year - (int)g.Release,
                   };
        }
        public IEnumerable<BasicGameInfo> GamesWithNpc()
        {
            var q1 = from g in this.gameRepo.ReadAll()
                     from c in this.charRepo.ReadAll()
                     from d in this.devRepo.ReadAll()
                     where (c.Priority == 3 && c.Priority != 2 && c.Priority != 1) && c.GameId == g.GameId && d.DeveloperId == g.DeveloperId
                     select new BasicGameInfo()
                     {
                         GameName = g.GameName,
                         DevName = d.DeveloperName,
                         Age = (int)DateTime.Now.Year - (int)g.Release
                     };
            return q1.Distinct();
        }
        public IEnumerable<RatingInfo> HighestRatingGameWithDevName()
        {
            var q = from g in this.gameRepo.ReadAll()
                    from d in this.devRepo.ReadAll()
                    let maxRating = gameRepo.ReadAll().Max(t => t.Rating)
                    where g.DeveloperId == d.DeveloperId && g.Rating == maxRating
                    select new RatingInfo()
                    {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Rating = g.Rating,
                       Price = g.Price,
                    };
            return q.ToList();
        }
        public IEnumerable<RatingInfo> LowestRatingGameWithDevName()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   let minRating = gameRepo.ReadAll().Min(t => t.Rating)
                   where g.DeveloperId == d.DeveloperId && g.Rating == minRating
                   select new RatingInfo()
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Rating = g.Rating,
                       Price = g.Price,
                   };
        }
        public IEnumerable<RatingInfo> FreeGames()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   where g.DeveloperId == d.DeveloperId && g.Price == 0
                   select new RatingInfo()
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Rating = g.Rating,
                       Price = g.Price,
                   };
        }
        public IEnumerable<RatingInfo> PaidGames()
        {
            return from g in this.gameRepo.ReadAll()
                   from d in this.devRepo.ReadAll()
                   where g.DeveloperId == d.DeveloperId && g.Price > 0
                   select new RatingInfo()
                   {
                       GameName = g.GameName,
                       DevName = d.DeveloperName,
                       Rating = g.Rating,
                       Price= g.Price,
                   };
        }
    }

    //Helping classes
    public class BasicGameInfo
    {
        public string GameName { get; set; }
        public string DevName { get; set; }
        public int Age { get; set; }
        public override bool Equals(object obj)
        {
            BasicGameInfo g = obj as BasicGameInfo;
            if (g == null)
            {
                return false;
            }
            else
            {
                return this.GameName == g.GameName && this.DevName == g.DevName && this.Age == g.Age;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GameName, this.DevName, this.Age);
        }
    }
    public class RatingInfo
    { 
        public string GameName { get; set; }
        public string DevName { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            RatingInfo r = obj as RatingInfo;
            if (r == null)
            {
                return false;
            }
            else
            { 
            return this.GameName == r.GameName && this.DevName == r.DevName && this.Rating == r.Rating && this.Price == r.Price;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GameName, this.DevName,this.Rating,this.Price);
        }
    }
    
}
