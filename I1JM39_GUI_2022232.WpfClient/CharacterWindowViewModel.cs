using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace I1JM39_GUI_2022232.WpfClient
{
    public class CharacterWindowViewModel
    {
        public RestCollection<Character> Characters { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CharacterWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Characters = new RestCollection<Character>("http://localhost:23247/", "character");
            }
        }
    }
}
