using System.Collections.Generic;

namespace AdvSoftMandatory2DWorld.Classes
{
    public class TurnManager
    {
        private Queue<Creature> turnOrder = new Queue<Creature>();

        public void AddCreature(Creature creature)
        {
            turnOrder.Enqueue(creature);
        }

        public void NextTurn()
        {
            if (turnOrder.Count == 0) return;

            Creature current = turnOrder.Dequeue();
            current.StartTurn();
            turnOrder.Enqueue(current);
        }
    }
}
