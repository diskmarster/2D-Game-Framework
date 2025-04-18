using AdvSoftMandatory2DWorld.Classes;

/// <summary>
/// Interface for defining behavior of a creature during various phases of its turn.
/// </summary>
public interface ICreatureState
{
    /// <summary>
    /// Called at the start of the creature's turn.
    /// </summary>
    /// <param name="creature">The creature whose turn is starting.</param>
    void StartTurn(Creature creature);

    /// <summary>
    /// Called at the end of the creature's turn.
    /// </summary>
    /// <param name="creature">The creature whose turn is ending.</param>
    void EndTurn(Creature creature);

    /// <summary>
    /// Moves the creature to a new location within the world.
    /// </summary>
    /// <param name="creature">The creature to move.</param>
    /// <param name="newX">New X-coordinate.</param>
    /// <param name="newY">New Y-coordinate.</param>
    /// <param name="world">The game world.</param>
    void Move(Creature creature, int newX, int newY, World world);

    /// <summary>
    /// Executes an attack against another creature.
    /// </summary>
    /// <param name="creature">The attacking creature.</param>
    /// <param name="target">The target being attacked.</param>
    /// <param name="world">The game world context.</param>
    void Attack(Creature creature, Creature target, World world);

    /// <summary>
    /// Puts the creature into a defensive stance for the turn.
    /// </summary>
    /// <param name="creature">The defending creature.</param>
    void Defend(Creature creature);
}
