﻿using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_GUI_2022232.WpfClient
{
    public class CharacterWindowViewModel
    {
        public RestCollection<Character> Characters { get; set; }

        public CharacterWindowViewModel()
        {
            Characters = new RestCollection<Character>("http://localhost:23247","character");
        }
    }
}
