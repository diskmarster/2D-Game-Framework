using System;

namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Interface for entities that can perform attacks on other creatures within the game world.
    /// </summary>
    public interface IAttackable
    {
        /// <summary>
        /// Executes an attack on the specified target within the given world.
        /// </summary>
        /// <param name="target">The creature being attacked.</param>
        /// <param name="world">The current game world context.</param>
        void Attack(Creature target, World world);
    }
}
