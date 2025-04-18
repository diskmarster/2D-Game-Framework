using AdvSoftMandatory2DWorld.Classes.Interfaces;

using AdvSoftMandatory2DWorld.Classes;

/// <summary>
/// Default movement strategy allowing limited range movement and basic world boundary checks.
/// </summary>
public class DefaultMovementStrategy : IMovementStrategy
{
    /// <summary>
    /// Moves the creature to a new position if valid (in range, in bounds, unoccupied).
    /// </summary>
    /// <param name="creature">Creature to move.</param>
    /// <param name="newX">Target X coordinate.</param>
    /// <param name="newY">Target Y coordinate.</param>
    /// <param name="world">Current world containing bounds and occupied spaces.</param>
    public void Move(Creature creature, int newX, int newY, World world)
    {
        int maxDistance = 2;
        int distance = (int)Math.Sqrt(Math.Pow(newX - creature.X, 2) + Math.Pow(newY - creature.Y, 2));

        if (distance > maxDistance)
        {
            Logger.Log($"{creature.Name} tried to move too far!");
            return;
        }

        if (newX < 0 || newX >= world.MaxX || newY < 0 || newY >= world.MaxY)
        {
            Logger.Log($"{creature.Name} tried to move out of bounds!");
            return;
        }

        if (world.IsPositionOccupied(newX, newY))
        {
            Logger.Log($"{creature.Name} tried to move, but the space is occupied!");
            return;
        }

        creature.X = newX;
        creature.Y = newY;

        Logger.Log($"{creature.Name} has moved to ({creature.X}, {creature.Y})");
    }
}
