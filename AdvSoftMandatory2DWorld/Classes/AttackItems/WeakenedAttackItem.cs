using AdvSoftMandatory2DWorld.Classes.Interfaces;

/// <summary>
/// A decorator that reduces damage and hit chance of an attack item,
/// simulating weakening effects (like low health or debuffs).
/// </summary>
public class WeakenedAttackItem : AttackItemDecorator
{
    /// <summary>
    /// Wraps an existing attack item with weakening effects.
    /// </summary>
    /// <param name="baseItem">The base item to weaken.</param>
    public WeakenedAttackItem(IAttackItem baseItem) : base(baseItem) { }

    public override int Damage => Math.Max(0, _inner.Damage - 3);
    public override int HitChance => Math.Max(10, _inner.HitChance - 15);
}
