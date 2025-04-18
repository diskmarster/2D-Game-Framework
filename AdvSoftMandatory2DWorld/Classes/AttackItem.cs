using AdvSoftMandatory2DWorld.Classes.Interfaces;

namespace AdvSoftMandatory2DWorld.Classes
{
    /// <summary>
    /// Represents an item that can be used to attack other creatures, such as a weapon or spell.
    /// </summary>
    public class AttackItem : WorldObject, IAttackItem
    {
        /// <summary>
        /// Gets the amount of damage this attack item deals.
        /// </summary>
        public int Damage { get; }

        /// <summary>
        /// Gets the chance (percentage) that this attack will successfully hit.
        /// </summary>
        public int HitChance { get; }

        /// <summary>
        /// Gets the range at which this attack item can be used (in grid units).
        /// </summary>
        public int Range { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackItem"/> class.
        /// </summary>
        /// <param name="name">The name of the attack item.</param>
        /// <param name="x">The X coordinate in the world.</param>
        /// <param name="y">The Y coordinate in the world.</param>
        /// <param name="lootable">Indicates whether the item can be looted.</param>
        /// <param name="removable">Indicates whether the item can be removed from the world.</param>
        /// <param name="damage">The amount of damage the item does.</param>
        /// <param name="hitChance">The percentage chance the item has to hit its target.</param>
        /// <param name="range">The maximum range this item can be used from.</param>
        public AttackItem(string name, int x, int y, bool lootable, bool removable, int damage, int hitChance, int range)
            : base(name, x, y, lootable, removable)
        {
            Damage = damage;
            HitChance = hitChance;
            Range = range;
        }
    }
}
