using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_HFT_2022231.Repository
{
    public class CharacterRepository : Repository<Character>, IRepository<Character>
    {
        public CharacterRepository(GameDbContext ctx) : base(ctx){}
        public override Character Read(int id)
        {
            return ctx.Characters.FirstOrDefault(t => t.CharacterId == id);
        }
        public override void Update(Character item)
        {
            var old = Read(item.CharacterId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
