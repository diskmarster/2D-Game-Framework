﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MandatoryAdvDemo
{
    /// <summary> This is the configuration class for the game. It is used to load the game settings from a JSON file. </summary>
    public static class ConfigLoader
    {
    public static GameConfig? Load(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Config file not found: {path}");
                return new GameConfig(); // Incase noget går galt, så returner jeg en tom GameConfig
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<GameConfig>(json);
        }
    }
}
