using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_HFT_2022231.Repository
{
    public class GameRepository : Repository<Game>, IRepository<Game>
    {
        public GameRepository(GameDbContext ctx) : base(ctx){}
        public override Game Read(int id)
        {
            return ctx.Games.FirstOrDefault(t => t.GameId == id);
        }
        public override void Update(Game item)
        {
            var old = Read(item.GameId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
