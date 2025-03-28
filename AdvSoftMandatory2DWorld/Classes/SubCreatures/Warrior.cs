using AdvSoftMandatory2DWorld.Classes.Interfaces;
using System;

namespace AdvSoftMandatory2DWorld.Classes.SubCreatures

{
    public class Warrior : Creature
    {
        public Warrior(string name, int hitPoints, int x, int y, IMovementStrategy movementStrategy)
            : base(name, hitPoints, x, y, movementStrategy) { }

        // Warrior's unique attack method implementation
        public override int Hit()
        {
            if (AttackItems.Count == 0)
            {
                Console.WriteLine($"{Name} has no weapon and punches for 5 damage! because warriors are strong!");
                return 5; // Default punch damage to be changed at a later stage
            }

            int totalDamage = AttackItems.Sum(a => a.Damage);
            Console.WriteLine($"{Name} attacks for {totalDamage} damage!");
            return totalDamage;
        }
    }
}
