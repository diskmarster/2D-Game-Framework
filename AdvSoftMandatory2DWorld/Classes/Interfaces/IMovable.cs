namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Interface for entities that can move within the game world.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Moves the entity to a new location in the world.
        /// </summary>
        /// <param name="newX">New X-coordinate.</param>
        /// <param name="newY">New Y-coordinate.</param>
        /// <param name="world">The world in which the entity exists.</param>
        void Move(int newX, int newY, World world);
    }
}
