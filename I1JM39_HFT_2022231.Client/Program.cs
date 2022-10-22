using I1JM39_HFT_2022231.Repository;
using System;
using System.Linq;

namespace I1JM39_HFT_2022231.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameDbContext db = new GameDbContext();
            var items = db.Games.ToArray();
        }
    }
}
