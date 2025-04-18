namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Represents an attack item that has a cooldown period between uses.
    /// </summary>
    public interface ICooldownAttackItem : IAttackItem
    {
        /// <summary>
        /// Total number of turns required for the cooldown to reset.
        /// </summary>
        int Cooldown { get; }

        /// <summary>
        /// Remaining turns until the item is usable again.
        /// </summary>
        int RemainingCooldown { get; set; }

        /// <summary>
        /// Decreases the cooldown timer by one turn.
        /// </summary>
        void TickCooldown();

        /// <summary>
        /// Resets the cooldown after the item has been used.
        /// </summary>
        void ResetCooldown();
    }
}
