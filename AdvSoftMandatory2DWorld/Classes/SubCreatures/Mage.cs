using AdvSoftMandatory2DWorld.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvSoftMandatory2DWorld.Classes.SubCreatures
{
    /// <summary>
    /// Represents a magical creature that can cast and combine spells, including cooldown-based attacks.
    /// </summary>
    public class Mage : Creature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mage"/> class with a default Ice Bolt spell.
        /// </summary>
        /// <param name="name">The name of the mage.</param>
        /// <param name="hitPoints">The mage's health points.</param>
        /// <param name="x">Starting X position in the world.</param>
        /// <param name="y">Starting Y position in the world.</param>
        /// <param name="movementStrategy">The strategy used to move this mage.</param>
        public Mage(string name, int hitPoints, int x, int y, IMovementStrategy movementStrategy)
           : base(name, hitPoints, x, y, movementStrategy)
        {
            // Always knows Ice Bolt, cannot be removed
            var iceBolt = new AttackItem("Ice Bolt", x, y, false, false, 3, 100, 3);
            AttackItems.Add(iceBolt);
        }

        /// <summary>
        /// Adds a new spell to the mage if it isn't already known.
        /// </summary>
        /// <param name="spell">The spell to add.</param>
        public void AddSpell(IAttackItem spell)
        {
            if (AttackItems.Any(s => s.Name == spell.Name))
            {
                Logger.Log($"{Name} already knows the spell {spell.Name}.");
                return;
            }

            AttackItems.Add(spell);
            Logger.Log($"{Name} has learned {spell.Name}.");
        }

        /// <summary>
        /// Removes a spell from the mage's spell list if it exists and is not on cooldown.
        /// </summary>
        /// <param name="spellName">The name of the spell to remove.</param>
        public void RemoveSpell(string spellName)
        {
            var spell = AttackItems.FirstOrDefault(s => s.Name == spellName && s.Name != "Ice Bolt");

            if (spell == null)
            {
                Logger.Log($"{Name} doesn't know the spell {spellName} or cannot remove it.");
                return;
            }

            if (spell is ICooldownAttackItem cd && cd.RemainingCooldown > 0)
            {
                Logger.Log($"{spellName} is still on cooldown and can't be removed.");
                return;
            }

            AttackItems.Remove(spell);
            Logger.Log($"{Name} has forgotten the spell {spellName}.");
        }

        /// <summary>
        /// Executes the mage's spell combo, casting up to 2 available spells and applying cooldowns.
        /// </summary>
        /// <returns>The total damage dealt by the combined spells.</returns>
        public override int Hit()
        {
            var availableSpells = AttackItems
                .OfType<IAttackItem>()
                .Where(spell => spell is not ICooldownAttackItem cd || cd.RemainingCooldown == 0)
                .DistinctBy(s => s.Name)
                .Take(2)
                .ToList();

            if (!availableSpells.Any())
            {
                Logger.Log($"{Name} casts Ice Bolt for 3 damage!");
                return 3;
            }

            string comboNames = string.Join(" + ", availableSpells.Select(s => s.Name));
            int totalDamage = availableSpells.Sum(s => s.Damage);

            Logger.Log($"{Name} casts {comboNames} for {totalDamage} damage!");

            foreach (var spell in availableSpells)
            {
                if (spell is ICooldownAttackItem cd)
                    cd.ResetCooldown();
            }

            return totalDamage;
        }
    }
}
