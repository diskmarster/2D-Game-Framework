using System;
using AdvSoftMandatory2DWorld.Classes.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvSoftMandatory2DWorld.Classes.States
{
    /// <summary>
    /// Represents the state of a creature that has moved this turn.
    /// Can still attack, defend, or end its turn.
    /// </summary>
    public class HasMovedState : CombatStateBase
    {
        /// <summary>
        /// Indicates that the creature is already mid-turn and cannot start a new one.
        /// </summary>
        /// <param name="creature">The creature attempting to start a turn.</param>
        public override void StartTurn(Creature creature)
        {
            Logger.Log($"{creature.Name} cannot start a new turn because they are already mid-turn.");
        }

        /// <summary>
        /// Prevents the creature from moving again after already moving.
        /// </summary>
        /// <param name="creature">The creature attempting to move.</param>
        /// <param name="newX">The new X-coordinate.</param>
        /// <param name="newY">The new Y-coordinate.</param>
        /// <param name="world">The world context for movement.</param>
        public override void Move(Creature creature, int newX, int newY, World world)
        {
            Logger.Log($"{creature.Name} cannot move again this turn.");
        }

        /// <summary>
        /// Ends the creature's turn and transitions to the waiting state.
        /// </summary>
        /// <param name="creature">The creature whose turn is ending.</param>
        public override void EndTurn(Creature creature)
        {
            Logger.Log($"{creature.Name} ends their turn after moving.");
            creature.SetState(new WaitingForTurnState());
        }
    }
}
