namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Interface for observing when a creature receives damage.
    /// </summary>
    public interface IHitObserver
    {
        /// <summary>
        /// Called when a creature takes damage.
        /// </summary>
        /// <param name="creature">The creature that was hit.</param>
        /// <param name="damageTaken">The amount of damage taken.</param>
        void OnHit(Creature creature, int damageTaken);
    }
}
