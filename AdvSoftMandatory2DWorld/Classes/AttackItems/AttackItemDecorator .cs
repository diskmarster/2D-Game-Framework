using AdvSoftMandatory2DWorld.Classes.Interfaces;

/// <summary>
/// Abstract base class for attack item decorators, allowing modification of behavior.
/// Implements the Decorator Pattern.
/// </summary>
public abstract class AttackItemDecorator : IAttackItem
{
    /// <summary>
    /// The inner attack item being decorated.
    /// </summary>
    protected readonly IAttackItem _inner;

    /// <summary>
    /// Initializes a new decorator for the specified attack item.
    /// </summary>
    /// <param name="inner">The base attack item to wrap.</param>
    protected AttackItemDecorator(IAttackItem inner)
    {
        _inner = inner;
    }

    public virtual string Name => _inner.Name;
    public virtual int Damage => _inner.Damage;
    public virtual int HitChance => _inner.HitChance;
    public virtual int Range => _inner.Range;
}
