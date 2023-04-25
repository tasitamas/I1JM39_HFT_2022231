using I1JM39_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace I1JM39_GUI_2022232.WpfClient
{
    public class DeveloperWindowViewModel
    {
        public RestCollection<Developer> Developers { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public DeveloperWindowViewModel()
        {
            if(!IsInDesignMode) 
            { 
                Developers = new RestCollection<Developer>("http://localhost:23247/","developer");
            }
        }
    }
}
