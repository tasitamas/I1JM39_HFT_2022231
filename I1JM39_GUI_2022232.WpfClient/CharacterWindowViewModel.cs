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
    public class CharacterWindowViewModel : ObservableRecipient
    {
        public RestCollection<Character> Characters { get; set; }

        private Character selectedCharacter;

        public ICommand CreateCharacterCommand { get; set; }
        public ICommand UpdateCharacterCommand { get; set; }
        public ICommand DeleteCharacterCommand { get; set; }

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set 
            {
                if (value != null)
                {
                    selectedCharacter = new Character()
                    {
                        CharacterId= value.CharacterId,
                        CharacterName= value.CharacterName,
                        Priority= value.Priority
                    };
                }
                OnPropertyChanged();
                (DeleteCharacterCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateCharacterCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public CharacterWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Characters = new RestCollection<Character>("http://localhost:23247/", "character");

                CreateCharacterCommand = new RelayCommand(() =>
                {
                    Characters.Add(new Character()
                    {
                        CharacterId = Characters.Max(t => t.CharacterId + 1),
                        CharacterName = SelectedCharacter.CharacterName,
                        Priority = SelectedCharacter.Priority,
                    });
                });
                UpdateCharacterCommand = new RelayCommand(() =>
                {
                    Characters.Update(SelectedCharacter);
                });
                DeleteCharacterCommand = new RelayCommand(() =>
                {
                    Characters.Delete(SelectedCharacter.CharacterId);
                },
                () => { return SelectedCharacter != null; }
                );
                SelectedCharacter = new Character() { CharacterName = "My Character"};
            }
        }
    }
}
