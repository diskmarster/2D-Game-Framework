using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvSoftMandatory2DWorld.Classes
{
    public class DefenceItem : WorldObject
    {
        public int ReduceHitPoint { get; set; }

        public DefenceItem(string name, int x, int y, bool lootable, bool removable, int reduceHitPoint)
            : base(name, x, y, lootable, removable)
        {
            ReduceHitPoint = reduceHitPoint;
        }
    }
}

