namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Represents an item that can be used to attack, such as a weapon or spell.
    /// </summary>
    public interface IAttackItem
    {
        /// <summary>
        /// The name of the attack item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The amount of damage this item deals.
        /// </summary>
        int Damage { get; }

        /// <summary>
        /// The percentage chance to hit the target (0–100).
        /// </summary>
        int HitChance { get; }

        /// <summary>
        /// The maximum range of the attack.
        /// </summary>
        int Range { get; }
    }
}
