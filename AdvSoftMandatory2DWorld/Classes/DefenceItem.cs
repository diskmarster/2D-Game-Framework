using System;

namespace AdvSoftMandatory2DWorld.Classes
{
    /// <summary>
    /// Represents an item that provides defense, reducing incoming damage when equipped by a creature.
    /// </summary>
    public class DefenceItem : WorldObject
    {
        /// <summary>
        /// Gets or sets the amount of hit points this item can reduce from an incoming attack.
        /// </summary>
        public int ReduceHitPoint { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefenceItem"/> class.
        /// </summary>
        /// <param name="name">The name of the defense item.</param>
        /// <param name="x">The X coordinate in the world.</param>
        /// <param name="y">The Y coordinate in the world.</param>
        /// <param name="lootable">Indicates whether the item can be looted.</param>
        /// <param name="removable">Indicates whether the item can be removed from the world.</param>
        /// <param name="reduceHitPoint">The amount of damage this item reduces when equipped.</param>
        public DefenceItem(string name, int x, int y, bool lootable, bool removable, int reduceHitPoint)
            : base(name, x, y, lootable, removable)
        {
            ReduceHitPoint = reduceHitPoint;
        }
    }
}
