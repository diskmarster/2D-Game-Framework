using System;

namespace AdvSoftMandatory2DWorld.Classes.SubWorldObjects
{
    public class Bush : WorldObject
    {
        /// <summary> I wanted a way to make the bush a defensive object, so that when a creature stands in it, it gets a bonus to dodging, aka it gets harder to hit. </summary>
        public Bush(int x, int y)
            : base("Bush", x, y, lootable: false, removable: false)
        {
        }

        /// <param name="creature">The creature standing in the bush.</param>
        public void ApplyDodgeBonus(Creature creature)
        {
            Logger.Log($"{creature.Name} is standing in a bush and gains increased dodge chance!");
            // Logic to increase dodge chance
            foreach (var defenseItem in creature.DefenceItems)
            {
                defenseItem.ReduceHitPoint += 5; // Example: Increase defense by 5 points
            }
        }
    }
}

