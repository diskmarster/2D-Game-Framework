using AdvSoftMandatory2DWorld.Classes.Interfaces;

/// <summary>
/// Represents a composite attack item made of multiple attack components.
/// Used to group spells or attacks together (Composite Pattern).
/// </summary>
public class AttackItemComposite : IAttackItem
{
    private readonly List<IAttackItem> _components = new();

    /// <summary>
    /// The name of the composite attack.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new composite attack with the given name.
    /// </summary>
    /// <param name="name">Name of the attack group.</param>
    public AttackItemComposite(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Adds an attack item to the composite.
    /// </summary>
    /// <param name="item">The attack item to add.</param>
    public void Add(IAttackItem item)
    {
        _components.Add(item);
    }

    /// <summary>
    /// Removes an attack item from the composite.
    /// </summary>
    /// <param name="item">The attack item to remove.</param>
    public void Remove(IAttackItem item)
    {
        _components.Remove(item);
    }

    /// <summary>
    /// Total combined damage of all contained attack items.
    /// </summary>
    public int Damage => _components.Sum(i => i.Damage);

    /// <summary>
    /// Average hit chance of all contained attack items.
    /// </summary>
    public int HitChance => _components.Any() ? (int)_components.Average(i => i.HitChance) : 0;

    /// <summary>
    /// The highest range among all contained attack items.
    /// </summary>
    public int Range => _components.Any() ? _components.Max(i => i.Range) : 0;
}
