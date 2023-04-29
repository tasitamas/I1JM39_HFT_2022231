using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace I1JM39_GUI_2022232.WpfClient
{
    public class GameWindowViewModel : ObservableRecipient
    {
        public RestCollection<Game> Games { get; set; }

        private Game selectedGame;

        public ICommand CreateGameCommand { get; set; }
        public ICommand UpdateGameCommand { get; set; }
        public ICommand DeleteGameCommand { get; set; }

        public Game SelectedGame
        {
            get { return selectedGame; }
            set 
            {
                if(value != null)
                {
                    selectedGame = new Game()
                    {
                        GameId = value.GameId,
                        GameName = value.GameName,
                        Price= value.Price,
                        Rating=value.Rating,
                        Release=value.Release,
                        Characters=value.Characters,
                    };
                }
                OnPropertyChanged();
                (DeleteGameCommand as RelayCommand).NotifyCanExecuteChanged(); 
                (UpdateGameCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public GameWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Games = new RestCollection<Game>("http://localhost:23247/", "game","hub");

                CreateGameCommand = new RelayCommand(() =>
                {
                    Games.Add(new Game()
                    {
                        GameId = Games.Max(t => t.GameId + 1),
                        GameName = SelectedGame.GameName,
                        Price = SelectedGame.Price,
                        Rating = SelectedGame.Rating,
                        Release = SelectedGame.Release,
                    });
                });
                UpdateGameCommand = new RelayCommand(() =>
                { 
                    Games.Update(SelectedGame);
                });
                DeleteGameCommand = new RelayCommand(() =>
                {
                    Games.Delete(SelectedGame.GameId);
                },
                () => { return SelectedGame != null; });

                //Had to set the Default Release Date to an accepted date
                SelectedGame = new Game() { GameName = "New Game", Release = 2000 };
            }
        }
    }
}
