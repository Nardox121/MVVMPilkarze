using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMPilkarze.Model
{
    class Player
    {
        #region Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        #endregion

        #region Constructors
        public Player(string name, string surname, int age, int weight)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Weight = weight;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Player player))
            {
                return false;
            }
            if (player.Surname != Surname) return false;
            if (player.Name != Name) return false;
            if (player.Age != Age) return false;
            if (player.Weight != Weight) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return this.Surname.GetHashCode() ^ this.Name.GetHashCode() ^ this.Weight.GetHashCode() ^ this.Age.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Surname} {Name} lat: {Age} waga: {Weight} kg";
        }
        public static Player FromString(string s)
        {
            string[] p = s.Split(" ");
            Player player = new Player(p[1], p[0], int.Parse(p[3]), int.Parse(p[5]));
            return player;
        }
        public void update(Player player)
        {
            this.Name = player.Name;
            this.Surname = player.Surname;
            this.Age = player.Age;
            this.Weight = player.Weight;
        }
        #endregion
    }
}
