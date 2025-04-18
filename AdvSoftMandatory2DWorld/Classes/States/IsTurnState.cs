using AdvSoftMandatory2DWorld.Classes;
using AdvSoftMandatory2DWorld.Classes.Interfaces;
using AdvSoftMandatory2DWorld.Classes.States;
using System;
using System.Linq;

/// <summary>
/// Represents the active state of a creature during its turn.
/// Allows movement, attacking, defending, or ending the turn.
/// </summary>
public class IsTurnState : CombatStateBase
{
    /// <summary>
    /// Begins the creature's turn and logs the state transition.
    /// </summary>
    /// <param name="creature">The creature whose turn is starting.</param>
    public override void StartTurn(Creature creature)
    {
        Logger.Log($"{creature.Name}'s turn begins!");
        Logger.Log($"{creature.Name} transitions to IsTurnState.");
    }

    /// <summary>
    /// Moves the creature and transitions its state to HasMoved.
    /// </summary>
    /// <param name="creature">The creature to move.</param>
    /// <param name="newX">The new X-coordinate.</param>
    /// <param name="newY">The new Y-coordinate.</param>
    /// <param name="world">The world context for the move.</param>
    public override void Move(Creature creature, int newX, int newY, World world)
    {
        creature._movementStrategy.Move(creature, newX, newY, world);
        creature.SetState(new HasMovedState());
    }

    /// <summary>
    /// Ends the creature's turn and transitions to the waiting state.
    /// </summary>
    /// <param name="creature">The creature whose turn is ending.</param>
    public override void EndTurn(Creature creature)
    {
        Logger.Log($"{creature.Name} ends their turn.");
        creature.SetState(new WaitingForTurnState());
    }
}
