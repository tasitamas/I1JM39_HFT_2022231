﻿using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
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
            if (item.CharacterName.Length < 2)
            {
                throw new ArgumentException("The name is too short");
            }
            else if (item.CharacterName.Length > 200)
            {
                throw new ArgumentException("The name is too long");
            }
            this.repo.Create(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
        public Character Read(int id)
        {
            var character = this.repo.Read(id);
            if (character == null)
            {
                throw new ArgumentException("This character doesn't exist.");
            }
            return character;
        }
        public IQueryable<Character> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Character item)
        {
            this.repo.Update(item);
        }
    }
}