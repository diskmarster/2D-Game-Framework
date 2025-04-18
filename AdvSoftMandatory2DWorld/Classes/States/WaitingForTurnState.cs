using AdvSoftMandatory2DWorld.Classes;

/// <summary>
/// Represents the state of a creature that is waiting for its turn.
/// Cannot perform actions until the turn starts.
/// </summary>
public class WaitingForTurnState : ICreatureState
{
    /// <summary>
    /// Starts the creature's turn and transitions it to IsTurnState.
    /// </summary>
    /// <param name="creature">The creature whose turn is starting.</param>
    public void StartTurn(Creature creature)
    {
        if (creature.HitPoints <= 0)
        {
            Logger.Log($"{creature.Name} is dead and cannot take a turn.");
            return;
        }

        Logger.Log($"{creature.Name}'s turn begins!");
        creature.SetState(new IsTurnState());
    }

    /// <summary>
    /// Logs that the creature is already waiting for their turn.
    /// </summary>
    /// <param name="creature">The creature attempting to end its turn.</param>
    public void EndTurn(Creature creature)
    {
        Logger.Log($"{creature.Name} is already waiting for their turn.");
    }

    /// <summary>
    /// Logs that the creature cannot move because it's not their turn.
    /// </summary>
    /// <param name="creature">The creature attempting to move.</param>
    /// <param name="newX">New X-coordinate.</param>
    /// <param name="newY">New Y-coordinate.</param>
    /// <param name="world">World context.</param>
    public void Move(Creature creature, int newX, int newY, World world)
    {
        Logger.Log($"{creature.Name} cannot move. It's not their turn.");
    }

    /// <summary>
    /// Logs that the creature cannot attack because it's not their turn.
    /// </summary>
    /// <param name="creature">The attacker.</param>
    /// <param name="target">The target.</param>
    /// <param name="world">World context.</param>
    public void Attack(Creature creature, Creature target, World world)
    {
        Logger.Log($"{creature.Name} cannot attack. It's not their turn.");
    }

    /// <summary>
    /// Logs that the creature cannot defend because it's not their turn.
    /// </summary>
    /// <param name="creature">The creature attempting to defend.</param>
    public void Defend(Creature creature)
    {
        Logger.Log($"{creature.Name} cannot defend. It's not their turn.");
    }
}
