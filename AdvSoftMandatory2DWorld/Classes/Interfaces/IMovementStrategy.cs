namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Strategy interface to define custom movement logic for creatures.
    /// </summary>
    public interface IMovementStrategy
    {
        /// <summary>
        /// Moves the creature according to the implemented strategy.
        /// </summary>
        /// <param name="creature">The creature to move.</param>
        /// <param name="newX">New X-coordinate.</param>
        /// <param name="newY">New Y-coordinate.</param>
        /// <param name="world">The game world.</param>
        void Move(Creature creature, int newX, int newY, World world);
    }
}
