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
            //if (item.DeveloperId < 1)
            //{
            //    throw new NullReferenceException("ID can't be less than 1!");
            //}
            //else 
            if (item.DeveloperName == null
                || item.DeveloperName == ""
                || item.DeveloperName == String.Empty)
            {
                throw new NullReferenceException("Name can't be empty!");
            }
            else if (item.DeveloperName.Length > 100)
            {
                throw new ArgumentOutOfRangeException("The name is too long...");
            }
            else
            {
                repo.Create(item);
            }
        }
        public void Delete(int id)
        {
            var dev = this.repo.Read(id);
            if (dev == null)
            {
                throw new NullReferenceException("Item is null, can't be deleted.");
            }
            if (id < 1)
            {
                throw new ArgumentException("ID is not valid! Item doesn't exists!");
            }
            else
            { 
                repo.Delete(id);
            }
        }
        public Developer Read(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("ID is not valid! Item doesn't exists!");
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
            else if (item.DeveloperName == null
                || item.DeveloperName == ""
                || item.DeveloperName == String.Empty)
            {
                throw new NullReferenceException("Name can't be empty!");
            }
            else if (item.DeveloperId < 1)
            {
                throw new ArgumentException("Not a correct ID.");
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
