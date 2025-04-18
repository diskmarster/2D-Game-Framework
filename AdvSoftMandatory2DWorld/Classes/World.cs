using AdvSoftMandatory2DWorld.Classes.SubWorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvSoftMandatory2DWorld.Classes
{
    /// <summary>
    /// Represents the 2D world grid where creatures and objects exist.
    /// Holds boundaries and manages spatial logic such as collisions and placement.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Gets the maximum horizontal size of the world grid.
        /// </summary>
        public int MaxX { get; }

        /// <summary>
        /// Gets the maximum vertical size of the world grid.
        /// </summary>
        public int MaxY { get; }

        /// <summary>
        /// Gets the list of all static or interactive world objects currently placed in the world.
        /// </summary>
        public List<WorldObject> Objects { get; } = new();

        /// <summary>
        /// Gets the list of all creatures currently alive and active in the world.
        /// </summary>
        public List<Creature> Creatures { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class with defined grid boundaries.
        /// </summary>
        /// <param name="maxX">Maximum width (horizontal size) of the world grid.</param>
        /// <param name="maxY">Maximum height (vertical size) of the world grid.</param>
        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }

        /// <summary>
        /// Adds a world object (e.g., bush, item, obstacle) to the grid.
        /// </summary>
        /// <param name="obj">The object to be added to the world.</param>
        public void AddObject(WorldObject obj) => Objects.Add(obj);

        /// <summary>
        /// Adds a creature to the world (e.g., warrior, mage).
        /// </summary>
        /// <param name="creature">The creature to be added to the world.</param>
        public void AddCreature(Creature creature) => Creatures.Add(creature);

        /// <summary>
        /// Checks if a given (x, y) coordinate in the world is already occupied by a creature or non-passable object.
        /// Bushes are ignored as they are considered passable.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>True if the position is occupied; otherwise, false.</returns>
        public bool IsPositionOccupied(int x, int y)
        {
            // Check if any creature occupies the position
            if (Creatures.Any(c => c.X == x && c.Y == y))
            {
                return true;
            }

            // Check if any object (excluding Bush) occupies the position
            if (Objects.Any(o => o.X == x && o.Y == y && o is not Bush))
            {
                return true;
            }

            return false;
        }
    }
}
