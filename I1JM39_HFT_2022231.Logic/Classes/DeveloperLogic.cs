using I1JM39_HFT_2022231.Logic.Interfaces;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using System;
using System.Linq;

namespace I1JM39_HFT_2022231.Logic
{
    public class DeveloperLogic : IDeveloperLogic
    {
        IRepository<Developer> repo;

        public DeveloperLogic(IRepository<Developer> repo)
        {
            this.repo = repo;
        }

        //CRUD Methods
        public void Create(Developer item)
        {
            repo.Create(item);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public Developer Read(int id)
        {
            var dev = repo.Read(id);
            if (dev == null)
            {
                throw new ArgumentException("This developer doesn't exist.");
            }
            return dev;
        }
        public IQueryable<Developer> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Developer item)
        {
            repo.Update(item);
        }

        //Non CRUD Methods
    }
}
