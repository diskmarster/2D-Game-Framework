using System;

namespace AdvSoftMandatory2DWorld.Classes
{
    /// <summary>
    /// Represents a base class for all objects placed within the world grid.
    /// Objects may include items like weapons, shields, or obstacles such as bushes.
    /// </summary>
    public abstract class WorldObject
    {
        /// <summary>
        /// Gets or sets the name of the object (e.g., "Sword", "Shield", "Bush").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the object's position in the world.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the object's position in the world.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets whether this object can be looted (e.g., picked up by a creature).
        /// </summary>
        public bool Lootable { get; set; }

        /// <summary>
        /// Gets or sets whether this object can be removed from the world.
        /// </summary>
        public bool Removable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldObject"/> class.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="x">The X coordinate in the world.</param>
        /// <param name="y">The Y coordinate in the world.</param>
        /// <param name="lootable">Whether the object is lootable.</param>
        /// <param name="removable">Whether the object is removable.</param>
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
