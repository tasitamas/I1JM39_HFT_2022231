using I1JM39_HFT_2022231.Models;
using System.Linq;

namespace I1JM39_HFT_2022231.Logic
{
    public interface ICharacterLogic
    {
        //CRUD Methods
        void Create(Character item);
        void Delete(int id);
        Character Read(int id);
        IQueryable<Character> ReadAll();
        void Update(Character item);
    }
}