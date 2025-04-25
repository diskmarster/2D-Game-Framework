using AdvSoftMandatory2DWorld.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvSoftMandatory2DWorld.Classes
{
    /// <summary>
    /// Represents a base class for all creatures in the game, providing shared properties and behaviors
    /// such as movement, attacking, defending, looting, and turn-based actions.
    /// </summary>
    public abstract class Creature : IMovable, IAttackable, IDefendable
    {
        private ICreatureState _currentState;
        public IMovementStrategy _movementStrategy;
        private readonly List<IHitObserver> _observers = new();

        /// <summary>
        /// The name of the creature.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The current hit points of the creature.
        /// </summary>
        public int HitPoints { get; protected set; }

        /// <summary>
        /// The current X-coordinate position of the creature.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The current Y-coordinate position of the creature.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// A list of attack items (weapons or spells) that the creature has.
        /// </summary>
        public List<IAttackItem> AttackItems { get; } = new();

        /// <summary>
        /// A list of defense items (shields, armor) that the creature has.
        /// </summary>
        public List<DefenceItem> DefenceItems { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">Name of the creature.</param>
        /// <param name="hitPoints">Starting health points.</param>
        /// <param name="x">X-position in the world.</param>
        /// <param name="y">Y-position in the world.</param>
        /// <param name="movementStrategy">Movement strategy to control movement logic.</param>
        protected Creature(string name, int hitPoints, int x, int y, IMovementStrategy movementStrategy)
        {
            Name = name;
            HitPoints = hitPoints;
            X = x;
            Y = y;
            _movementStrategy = movementStrategy;
            _currentState = new WaitingForTurnState();
        }

        /// <summary>
        /// Sets the state of the creature (e.g., IsTurn, HasMoved).
        /// </summary>
        /// <param name="newState">The new state to transition to.</param>
        public void SetState(ICreatureState newState)
        {
            Logger.Log($"{Name} transitions to {newState.GetType().Name}.");
            _currentState = newState;
        }

        /// <summary>
        /// Starts the creature's turn.
        /// </summary>
        public void StartTurn() => _currentState.StartTurn(this);

        /// <summary>
        /// Ends the creature's turn.
        /// </summary>
        public void EndTurn() => _currentState.EndTurn(this);

        /// <summary>
        /// Attempts to attack another creature.
        /// </summary>
        /// <param name="target">The creature to attack.</param>
        /// <param name="world">The world where the attack is happening.</param>
        public void Attack(Creature target, World world) => _currentState.Attack(this, target, world);

        /// <summary>
        /// Defends this turn, increasing damage resistance temporarily.
        /// </summary>
        public void Defend() => _currentState.Defend(this);

        /// <summary>
        /// Moves the creature to a new position if allowed by the movement strategy.
        /// </summary>
        /// <param name="newX">New X-coordinate.</param>
        /// <param name="newY">New Y-coordinate.</param>
        /// <param name="world">The game world context.</param>
        public void Move(int newX, int newY, World world) => _movementStrategy.Move(this, newX, newY, world);

        /// <summary>
        /// Performs the creature's custom attack logic and returns the calculated damage.
        /// </summary>
        /// <returns>The damage dealt by the attack.</returns>
        public abstract int Hit();

        /// <summary>
        /// Receives incoming damage and reduces hit points, applying defense modifiers.
        /// </summary>
        /// <param name="damage">The base damage before mitigation.</param>
        public virtual void ReceiveHit(int damage)
        {
            
            int reducedDamage = CalculateReducedDamage(damage);
            HitPoints -= reducedDamage;

            NotifyHitObservers(reducedDamage);
            if (HitPoints <= 0)
            {
                HitPoints = 0;
                Logger.Log($"{Name} has died!");
            }
        }

        /// <summary>
        /// Picks up an object from the world, adding it to inventory if applicable.
        /// </summary>
        /// <param name="obj">The object to pick up.</param>
        public void Loot(WorldObject obj)
        {
            if (obj is AttackItem attackItem)
            {
                AttackItems.Add(attackItem);
                Logger.Log($"{Name} has picked up {attackItem.Name}");
            }
            else if (obj is DefenceItem defenceItem)
            {
                DefenceItems.Add(defenceItem);
                Logger.Log($"{Name} has picked up {defenceItem.Name}");
            }
        }

        /// <summary>
        /// Calculates the amount of damage reduced by defense items.
        /// </summary>
        /// <param name="damage">The incoming damage.</param>
        /// <returns>The reduced damage after applying defense.</returns>
        private int CalculateReducedDamage(int damage)
        {
            int totalReduction = DefenceItems.Sum(d => d.ReduceHitPoint);
            return Math.Max(0, damage - totalReduction);
        }

        /// <summary>
        /// Logs the creature's current hit points.
        /// </summary>
        public void PrintStats()
        {
            Logger.Log($"{Name} has {HitPoints} HP");
        }

        /// <summary>
        /// Subscribes an observer to be notified when the creature is hit.
        /// </summary>
        /// <param name="observer">The observer to subscribe.</param>
        public void AddObserver(IHitObserver observer) => _observers.Add(observer);

        /// <summary>
        /// Unsubscribes an observer from hit notifications.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public void RemoveObserver(IHitObserver observer) => _observers.Remove(observer);

        /// <summary>
        /// Notifies all observers that the creature has taken damage.
        /// </summary>
        /// <param name="damage">The amount of damage received.</param>
        private void NotifyHitObservers(int damage)
        {
            foreach (var observer in _observers)
            {
                observer.OnHit(this, damage);
            }
        }
    }
}
