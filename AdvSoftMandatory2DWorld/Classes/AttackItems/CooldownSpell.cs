using AdvSoftMandatory2DWorld.Classes.Interfaces;

using AdvSoftMandatory2DWorld.Classes;

/// <summary>
/// Represents a spell with a cooldown timer.
/// Implements the ICooldownAttackItem interface.
/// </summary>
public class CooldownSpell : ICooldownAttackItem
{
    public string Name { get; }
    public int Damage { get; }
    public int HitChance { get; }
    public int Range { get; }

    public int Cooldown { get; }

    /// <summary>
    /// Turns remaining before the spell becomes usable again.
    /// </summary>
    public int RemainingCooldown { get; set; }

    /// <summary>
    /// Initializes a new cooldown spell with specified properties.
    /// </summary>
    /// <param name="name">Spell name.</param>
    /// <param name="damage">Spell damage.</param>
    /// <param name="hitChance">Hit chance (0–100).</param>
    /// <param name="range">Attack range.</param>
    /// <param name="cooldown">Cooldown in turns.</param>
    public CooldownSpell(string name, int damage, int hitChance, int range, int cooldown)
    {
        Name = name;
        Damage = damage;
        HitChance = hitChance;
        Range = range;
        Cooldown = cooldown;
        RemainingCooldown = 0;
    }

    /// <summary>
    /// Ticks the cooldown timer by 1 turn.
    /// </summary>
    public void TickCooldown()
    {
        if (RemainingCooldown > 0)
        {
            RemainingCooldown--;
            Logger.Log($"{Name} cooldown: {RemainingCooldown} turn(s) remaining.");
        }
    }

    /// <summary>
    /// Resets the cooldown to the full duration.
    /// Called after the spell is used.
    /// </summary>
    public void ResetCooldown()
    {
        RemainingCooldown = Cooldown;
    }
}
