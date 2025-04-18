using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandatoryAdvDemo
{
    public class GameConfig
    {
        public WorldConfig? World { get; set; }
        public string GameLevel { get; set; } = "Normal";
    }
}
