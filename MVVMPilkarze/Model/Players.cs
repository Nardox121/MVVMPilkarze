using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMPilkarze.Model
{
    class Players
    {
        public List<Player> players { get; private set; }
        private string saveName = "save.json";

        public Players()
        {
            this.players = Save.LoadFromFile(this.saveName);
            if (players == null)
                players = new List<Player>();
        }

        public void SavePlayers()
        {
            Save.SaveToFile(this.saveName, this.players);
        }

        public bool AddPlayer(string name, string surname, int age, int weight)
        {
            if (name.Contains(" ") | surname.Contains(" "))
                return false;
            Player player = new Player(name, surname, age, weight);
            this.players.Add(player);
            return true;
        }

        public void UpdatePlayer(Player oldPlayer, Player newPlayer)
        {
            for (int i = 0; i < players.Count; i++)
                if (players[i].Equals(oldPlayer))
                    players[i] = newPlayer;
        }

        public void DeletePlayer(Player player)
        {
            players.Remove(player);
        }

    }
}
