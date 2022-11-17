using I1JM39_HFT_2022231.Logic;
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
            if (item.DeveloperName == null)
            {
                throw new NullReferenceException();
            }
            else if (item.DeveloperName.Length > 100)
            {
                throw new ArgumentException("The name is too long...");
            }
            else
            { 
                repo.Create(item);
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
                repo.Delete(id);
            }
        }
        public Developer Read(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return repo.Read(id);
            }
        }
        public IQueryable<Developer> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Developer item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else if (item.DeveloperName == null)
            {
                throw new NullReferenceException();
            }
            else if (item.DeveloperId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (item.DeveloperName.Length > 100)
            {
                throw new ArgumentException("The name is too long...");
            }
            else
            {
                repo.Update(item);
            }
        }
    }
}
