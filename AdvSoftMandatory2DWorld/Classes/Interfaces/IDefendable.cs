namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    /// <summary>
    /// Interface for entities that can enter a defensive state during their turn.
    /// </summary>
    public interface IDefendable
    {
        /// <summary>
        /// Triggers the entity's defend behavior.
        /// </summary>
        void Defend();
    }
}
