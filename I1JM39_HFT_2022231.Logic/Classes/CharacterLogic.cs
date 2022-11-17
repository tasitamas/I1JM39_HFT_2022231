using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace I1JM39_HFT_2022231.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        IRepository<Character> repo;

        public CharacterLogic(IRepository<Character> repo)
        {
            this.repo = repo;
        }

        //CRUD Methods
        public void Create(Character item)
        {
            if (item.CharacterName == null 
                || item.CharacterName == "" 
                || item.CharacterName == String.Empty)
            {
                throw new NullReferenceException();
            }
            else if (item.Priority < 1 || item.Priority > 3)
            {
                throw new ArgumentOutOfRangeException("Not a correct priority");
            }
            else if (item.CharacterName.Length > 50)
            {
                throw new ArgumentOutOfRangeException("The name too long...");
            }
            this.repo.Create(item);
        }
        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.repo.Delete(id);
        }
        public Character Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            { 
                return repo.Read(id);
            }
        }
        public IQueryable<Character> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Character item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else if (item.CharacterName == null
                    || item.CharacterName == ""
                    || item.CharacterName == String.Empty)
            {
                throw new NullReferenceException();
            }
            else if (item.Priority < 1 || item.Priority > 3)
            {
                throw new ArgumentOutOfRangeException("Not a correct priority");
            }
            else if (item.CharacterId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (item.CharacterName.Length > 50)
            {
                throw new ArgumentOutOfRangeException("The name is too long...");
            }
            else
            {
                this.repo.Update(item);
            }
        }
    }
}
