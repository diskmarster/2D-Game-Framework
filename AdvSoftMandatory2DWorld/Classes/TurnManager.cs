using AdvSoftMandatory2DWorld.Classes;

/// <summary>
/// Manages turn order using a simple round-robin queue.
/// </summary>
public class TurnManager
{
    private Queue<Creature> turnOrder = new Queue<Creature>();

    /// <summary>
    /// Adds a creature to the turn rotation.
    /// </summary>
    /// <param name="creature">Creature to enqueue.</param>
    public void AddCreature(Creature creature)
    {
        turnOrder.Enqueue(creature);
    }

    /// <summary>
    /// Advances to the next creature's turn.
    /// Automatically wraps around to the front of the queue.
    /// </summary>
    public void NextTurn()
    {
        if (turnOrder.Count == 0) return;

        Creature current = turnOrder.Dequeue();
        Logger.Log($"It's {current.Name}'s turn!");
        current.StartTurn();
        turnOrder.Enqueue(current);
    }
}
