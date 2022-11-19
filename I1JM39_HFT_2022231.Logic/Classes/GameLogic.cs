using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
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
            if (    item.GameName == null
                ||  item.GameName == ""
                ||  item.GameName == String.Empty)
            {
                throw new NullReferenceException();
            }
            else if (item.Price < 0 || item.Price > 50000)
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
            else if (item.GameName.Length > 150)
            {
                throw new ArgumentOutOfRangeException("The name is too long...");
            }
            else
            {
                gameRepo.Create(item);
            }
        }
        public void Delete(int id)
        {
            if (id <= 0)
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
            if (id <= 0)
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
            else if (item.GameName == null
                || item.GameName == ""
                || item.GameName == String.Empty)
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
            else if (item.GameName.Length > 150)
            {
                throw new ArgumentOutOfRangeException("The name is too long...");
            }
            else
            {
                gameRepo.Update(item);
            }
        }



        //Non CRUD Methods
        public IEnumerable<object> OldestGameWithDeveloperName()
        {
            var oldest = from x in this.gameRepo.ReadAll()
                         where x.Release.Equals(gameRepo.ReadAll().Min(t => t.Release))
                         select new
                         {
                             _GameName = x.GameName,
                             _DevName = x.Developer.DeveloperName,
                             _Age = DateTime.Now.Year - x.Release,
                         };
            return oldest;
        }
        public IEnumerable<object> YoungestGameWithDeveloperName()
        {
            var youngest = from x in this.gameRepo.ReadAll()
                           where x.Release.Equals(gameRepo.ReadAll().Max(t => t.Release))
                           select new
                           {
                               _GameName = x.GameName,
                               _DevName = x.Developer.DeveloperName,
                               _Age = DateTime.Now.Year - x.Release,
                           };
            return youngest;
        }

        public IEnumerable<GameInfo> OlderThan10YearsGames()
        {
            var older = from x in this.gameRepo.ReadAll()
                        where (DateTime.Now.Year - x.Release) > 10
                        select new GameInfo()
                        {
                            GameName = x.GameName,
                            DevName = x.Developer.DeveloperName,
                            Age = DateTime.Now.Year - x.Release,
                        };
            return older;
        }

        public IEnumerable<KeyValuePair<string, string>> HighestRatingGameWithDevName()
        {
            var highest = from x in this.gameRepo.ReadAll()
                          where x.Rating.Equals(gameRepo.ReadAll().Max(t => t.Rating))
                          select new KeyValuePair<string, string>
                          (x.GameName, x.Developer.DeveloperName);
            return highest;
        }
        public IEnumerable<KeyValuePair<string, string>> LowestRatingGameWithDevName()
        {
            var lowest = from x in this.gameRepo.ReadAll()
                         where x.Rating.Equals(gameRepo.ReadAll().Min(t => t.Rating))
                         select new KeyValuePair<string, string>
                         (x.GameName, x.Developer.DeveloperName);
            return lowest;
        }

        public IEnumerable<RatingInfo> FreeGames()
        {
            var free = from x in this.gameRepo.ReadAll()
                       where x.Price == 0
                       select new RatingInfo()
                       {
                           GameName = x.GameName,
                           DevName = x.Developer.DeveloperName,
                           Rating = x.Rating
                       };
            return free;
        }
        public IEnumerable<RatingInfo> PaidGames()
        {
            var paid = from x in this.gameRepo.ReadAll()
                       where x.Price > 0
                       select new RatingInfo()
                       {
                           GameName = x.GameName,
                           DevName = x.Developer.DeveloperName,
                           Rating = x.Rating
                       };
            return paid;
        }

        public IEnumerable<KeyValuePair<string, int>> GamesCharactersCount()
        {
            var count = from x in charRepo.ReadAll()
                        group x by x.Game.GameName into g
                        orderby g.Count() descending
                        select new KeyValuePair<string, int>
                        (g.Key, g.Count());
            return count;
        }
    }

    //Helping classes
    public class RatingInfo
    {
        public string GameName { get; set; }
        public string DevName { get; set; }
        public double Rating { get; set; }

        public override bool Equals(object obj)
        {
            RatingInfo r = obj as RatingInfo;
            if (r == null)
            {
                return false;
            }
            else
            {
                return this.GameName == r.GameName && this.DevName == r.DevName && this.Rating == r.Rating;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GameName, this.DevName, this.Rating);
        }
    }
    public class GameInfo
    {
        public string GameName { get; set; }
        public string DevName { get; set; }
        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            GameInfo r = obj as GameInfo;
            if (r == null)
            {
                return false;
            }
            else
            {
                return this.GameName == r.GameName && this.DevName == r.DevName && this.Age == r.Age;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GameName, this.DevName, this.Age);
        }
    }

}
