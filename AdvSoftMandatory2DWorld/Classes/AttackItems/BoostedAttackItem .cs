using AdvSoftMandatory2DWorld.Classes.Interfaces;

/// <summary>
/// A decorator that increases the damage and hit chance of an attack item.
/// </summary>
public class BoostedAttackItem : AttackItemDecorator
{
    /// <summary>
    /// Wraps an existing attack item with a boost.
    /// </summary>
    /// <param name="baseItem">The base item to boost.</param>
    public BoostedAttackItem(IAttackItem baseItem) : base(baseItem) { }

    public override int Damage => _inner.Damage + 5;
    public override int HitChance => _inner.HitChance + 10;
}
