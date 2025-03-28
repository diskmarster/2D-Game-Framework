using System;

namespace AdvSoftMandatory2DWorld.Classes
{
    public abstract class WorldObject
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Lootable { get; set; }
        public bool Removable { get; set; }

        public WorldObject(string name, int x, int y, bool lootable, bool removable)
        {
            Name = name;
            X = x;
            Y = y;
            Lootable = lootable;
            Removable = removable;
        }
    }
}
