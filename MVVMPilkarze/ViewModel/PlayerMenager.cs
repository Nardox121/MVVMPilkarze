using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MVVMPilkarze.ViewModel
{
    using Model;
    using Base;
    internal class PlayerMenager : ViewModelBase
    {
        #region model
        Players Model = new Players();
        #endregion
        #region constructor
        public PlayerMenager() {
            UpdatePlayers();
        }
        #endregion
        #region public properties
        public string[] players;
        public string[] Players
        {
            get
            {
                return players;
            }
            private set
            {
                players = value;
                onPropertyChanged(nameof(Players));
            }
        }
        private int age=15;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                onPropertyChanged(nameof(Age));
            }
        }

        private int weight=50;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                onPropertyChanged(nameof(Weight));
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                onPropertyChanged(nameof(Surname));
            }
        }

        private string selectedPlayer;
        public string SelectedPlayer
        {
            get
            {
                return selectedPlayer;
            }
            set
            {
                selectedPlayer = value;
                if (value != null)
                {
                    Age = Player.FromString(selectedPlayer).Age;
                    Name = Player.FromString(selectedPlayer).Name;
                    Weight = Player.FromString(selectedPlayer).Weight;
                    Surname = Player.FromString(selectedPlayer).Surname;
                    onPropertyChanged(nameof(SelectedPlayer));
                }
            }
        }

        #endregion

        #region commands
        private ICommand add = null;
        public ICommand Add
        {
            get
            {
                if (add == null)
                {
                    add = new RelayCommand(
                        arg =>
                        {
                            Model.AddPlayer(name, surname, age, weight);
                            UpdatePlayers();
                        },
                        arg =>
                        {
                            if (name == null | surname == null)
                                return false;
                            if (name.Length == 0 | surname.Length == 0)
                                return false;
                            if (!Model.AddPlayer(name, surname, age, weight))
                                return false;
                            return true;
                        }
                        );
                }
                return add;
            }
        }

        private ICommand delete = null;

        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(
                        arg => 
                        {
                            Model.DeletePlayer(Player.FromString(selectedPlayer));
                            UpdatePlayers(); 
                        },
                        arg => (selectedPlayer!=null)
                        );
                }
                return delete;
            }
        }

        private ICommand edit = null;

        public ICommand Edit
        {
            get
            {
                if (edit == null)
                {
                    edit = new RelayCommand(
                        arg =>
                        {
                            Player edited = new Player(name, surname, age, weight);
                            Model.UpdatePlayer(Player.FromString(selectedPlayer), edited);
                            UpdatePlayers();
                        },
                        arg => (selectedPlayer!=null)
                        );
                }
                return edit;
            }
        }
        #endregion
        private void UpdatePlayers()
        {
            Player[] p = Model.players.ToArray();
            string[] s = new string[p.Length];
            for (int i = 0; i < p.Length; i++)
                s[i] = p[i].ToString();
            Players = s;
            Model.SavePlayers();
        }
    }
}
