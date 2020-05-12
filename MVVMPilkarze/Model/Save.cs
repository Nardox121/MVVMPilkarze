using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MVVMPilkarze.Model
{
    internal static class Save
    {
        internal static void SaveToFile(string fileName, List<Player> players)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, players);
            }
        }
        internal static List<Player> LoadFromFile(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<Player>)serializer.Deserialize(file, typeof(List<Player>));
            }
        }
    }
}
