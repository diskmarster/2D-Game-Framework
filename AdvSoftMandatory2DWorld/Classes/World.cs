using System;
using System.Collections.Generic;

namespace AdvSoftMandatory2DWorld.Classes
{
    public class World
    {
        public int MaxX { get; }
        public int MaxY { get; }
        public List<WorldObject> Objects { get; } = new();
        public List<Creature> Creatures { get; } = new();

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }

        public void AddObject(WorldObject obj) => Objects.Add(obj);
        public void AddCreature(Creature creature) => Creatures.Add(creature);

        public bool IsPositionOccupied(int x, int y)
        {
            return Creatures.Any(c => c.X == x && c.Y == y) || Objects.Any(o => o.X == x && o.Y == y);
        }
    }
}