using AdvSoftMandatory2DWorld.Classes.Interfaces;
using System;

namespace AdvSoftMandatory2DWorld.Classes.SubCreatures
{
    /// <summary>
    /// Represents a warrior class creature that relies on melee attacks and physical weapons.
    /// </summary>
    public class Warrior : Creature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Warrior"/> class.
        /// </summary>
        /// <param name="name">The name of the warrior.</param>
        /// <param name="hitPoints">The warrior's health points.</param>
        /// <param name="x">Starting X position in the world.</param>
        /// <param name="y">Starting Y position in the world.</param>
        /// <param name="movementStrategy">The strategy used to move this warrior.</param>
        public Warrior(string name, int hitPoints, int x, int y, IMovementStrategy movementStrategy)
            : base(name, hitPoints, x, y, movementStrategy) { }

        /// <summary>
        /// Equips a weapon for the warrior, replacing any currently equipped weapon.
        /// </summary>
        /// <param name="weapon">The weapon to equip.</param>
        public void AddWeapon(IAttackItem weapon)
        {
            AttackItems.Clear(); // Only one weapon allowed for now - can be changed later
            AttackItems.Add(weapon);
            Logger.Log($"{Name} equips {weapon.Name}.");
        }

        /// <summary>
        /// Removes the currently equipped weapon from the warrior.
        /// </summary>
        public void RemoveWeapon()
        {
            if (AttackItems.Count == 0)
            {
                Logger.Log($"{Name} has no weapon to remove.");
                return;
            }
            var weapon = AttackItems[0];
            AttackItems.Clear();
            Logger.Log($"{Name} unequips {weapon.Name}.");
        }

        /// <summary>
        /// Attempts to loot a nearby object in the world. If the object is an attack item, it replaces the current weapon.
        /// </summary>
        /// <param name="world"></param>
        
        public void TryLootNearby(World world)
        {
            var nearbyLoot = world.Objects.FirstOrDefault(obj =>
                obj.Lootable &&
                Math.Abs(obj.X - X) + Math.Abs(obj.Y - Y) <= 1);

            if (nearbyLoot == null)
            {
                return;
            }

            if (nearbyLoot is AttackItem)
            {
                RemoveWeapon(); // Unequip old
                Logger.Log($"{Name} has picked up {nearbyLoot.Name} and unequipped the old one.");
            }

            Loot(nearbyLoot);              
            world.Objects.Remove(nearbyLoot); // Remove from world
            
        }


        /// <summary>
        /// Executes the warrior's attack. If no weapon is equipped, a basic punch is used.
        /// </summary>
        /// <returns>The total damage dealt by the attack.</returns>
        // Uint 32 bit altid positiv 0 -> 4294967295
        public override int Hit()
        {
            if (AttackItems.Count == 0)
            {
                Console.WriteLine($"{Name} has no weapon and punches for 5 damage! because warriors are strong!");
                return 5;
            }

            int totalDamage = AttackItems.Sum(a => a.Damage);
            Console.WriteLine($"{Name} attacks for {totalDamage} damage!");
            return totalDamage;
        }
    }
}
