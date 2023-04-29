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
    public class DeveloperWindowViewModel : ObservableRecipient
    {
        public RestCollection<Developer> Developers { get; set; }

        private Developer selectedDeveloper;

        public ICommand CreateDeveloperCommand { get; set; }
        public ICommand UpdateDeveloperCommand { get; set; }
        public ICommand DeleteDeveloperCommand { get; set; }

        public Developer SelectedDeveloper
        { 
            get { return selectedDeveloper; }
            set
            {
                if (value != null)
                {
                    selectedDeveloper = new Developer()
                    {
                        DeveloperId = value.DeveloperId,
                        DeveloperName = value.DeveloperName,
                    };
                }
                OnPropertyChanged();
                (DeleteDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
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

                CreateDeveloperCommand = new RelayCommand(() =>
                {
                    Developers.Add(new Developer()
                    {
                        DeveloperId = Developers.Max(t => t.DeveloperId + 1),
                        DeveloperName = SelectedDeveloper.DeveloperName,
                    });
                });
                UpdateDeveloperCommand = new RelayCommand(() =>
                {
                    Developers.Update(SelectedDeveloper);
                });
                DeleteDeveloperCommand = new RelayCommand(() =>
                {
                    Developers.Delete(SelectedDeveloper.DeveloperId);
                },
                () => { return SelectedDeveloper != null; });

                SelectedDeveloper = new Developer();
            }
        }
    }
}
