using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_GUI_2022232.WpfClient
{
    public class GameWindowViewModel
    {
        public RestCollection<Game> Games { get; set; }

        public GameWindowViewModel()
        {
            Games = new RestCollection<Game>("http://localhost:23247","game");
        }
    }
}
