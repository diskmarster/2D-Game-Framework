using AdvSoftMandatory2DWorld.Classes.Interfaces;
using AdvSoftMandatory2DWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvSoftMandatory2DWorld.Classes
{
    public class DefaultMovementStrategy : IMovementStrategy
    {
        public void Move(Creature creature, int newX, int newY, World world)
        {
            if (creature.State != Creature.CreatureState.IsTurn && creature.State != Creature.CreatureState.OutOfCombat)
            {
                Console.WriteLine($"{creature.Name} cannot move right now!");
                return;
            }

            int maxDistance = 2;
            int distance = (int)Math.Sqrt(Math.Pow(newX - creature.X, 2) + Math.Pow(newY - creature.Y, 2));

            if (distance > maxDistance)
            {
                Console.WriteLine($"{creature.Name} tried to move too far!");
                return;
            }

            if (newX < 0 || newX >= world.MaxX || newY < 0 || newY >= world.MaxY)
            {
                Console.WriteLine($"{creature.Name} tried to move out of bounds!");
                return;
            }

            if (world.IsPositionOccupied(newX, newY))
            {
                Console.WriteLine($"{creature.Name} tried to move, but the space is occupied!");
                return;
            }

            creature.X = newX;
            creature.Y = newY;

            Console.WriteLine($"{creature.Name} has moved to ({creature.X}, {creature.Y})");
            creature.ChangeState(Creature.CreatureState.HasMoved);
        }
    }
}
