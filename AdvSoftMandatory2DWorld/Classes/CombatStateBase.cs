using AdvSoftMandatory2DWorld.Classes;

/// <summary>
/// Abstract base class for combat states that handles shared logic like attacking and defending.
/// Child classes must define turn control flow (start/end/move).
/// </summary>
public abstract class CombatStateBase : ICreatureState
{
    /// <summary>
    /// Begins a turn. Must be implemented by subclasses.
    /// </summary>
    /// <param name="creature">The creature whose turn it is.</param>
    public abstract void StartTurn(Creature creature);

    /// <summary>
    /// Attempts to move a creature to a new location. Must be implemented by subclasses.
    /// </summary>
    /// <param name="creature">The moving creature.</param>
    /// <param name="newX">Target X coordinate.</param>
    /// <param name="newY">Target Y coordinate.</param>
    /// <param name="world">The current game world.</param>
    public abstract void Move(Creature creature, int newX, int newY, World world);

    /// <summary>
    /// Handles attack behavior including range, hit, and damage resolution.
    /// </summary>
    /// <param name="creature">Attacker.</param>
    /// <param name="target">Target to attack.</param>
    /// <param name="world">Game world context (unused, but passed for consistency).</param>
    public virtual void Attack(Creature creature, Creature target, World world)
    {
        int distance = (int)Math.Sqrt(Math.Pow(target.X - creature.X, 2) + Math.Pow(target.Y - creature.Y, 2));
        int maxRange = creature.AttackItems.Any() ? creature.AttackItems.Max(i => i.Range) : 1;

        if (distance > maxRange)
        {
            Logger.Log($"{creature.Name} tried to attack {target.Name}, but they are out of range!");
            return;
        }

        int damage = creature.Hit();
        if (damage <= 0)
        {
            Logger.Log($"{creature.Name} tried to attack {target.Name}, but missed or failed.");
            return;
        }

        target.ReceiveHit(damage);
        Logger.Log($"{creature.Name} dealt {damage} damage to {target.Name}!");
        creature.EndTurn();
    }

    /// <summary>
    /// Applies a defensive stance to increase protection for the current turn.
    /// </summary>
    /// <param name="creature">The defending creature.</param>
    public virtual void Defend(Creature creature)
    {
        Logger.Log($"{creature.Name} chooses to defend this turn.");
        Logger.Log($"{creature.Name} braces themselves, increasing defense by 50%!");

        foreach (var item in creature.DefenceItems)
        {
            item.ReduceHitPoint = (int)(item.ReduceHitPoint * 1.5);
        }

        creature.EndTurn();
    }

    /// <summary>
    /// Ends the current creature's turn. Must be implemented by subclasses.
    /// </summary>
    /// <param name="creature">The acting creature.</param>
    public abstract void EndTurn(Creature creature);
}
