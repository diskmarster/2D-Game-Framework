using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvSoftMandatory2DWorld.Classes.SubWorldObjects
{
    public class Grass : WorldObject
    {
        public Grass(string name, int x, int y, bool lootable, bool removable)
            : base(name, x, y, false, false)
        {

        }
    }
}
