using AdvSoftMandatory2DWorld.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AdvSoftMandatory2DWorld.Classes
{
    public abstract class Creature : IMovable, IAttackable, IDefendable
    {
        private IMovementStrategy _movementStrategy;
        public string Name { get; protected set; }
        public int HitPoints { get; protected set; }
        public int X { get; set; }
        public int Y { get; set; }
        protected List<AttackItem> AttackItems { get; } = new();
        protected List<DefenceItem> DefenceItems { get; } = new();
        public CreatureState State { get; internal set; } = CreatureState.WaitingForTurn;

        // States Work in process
        public enum CreatureState
        {
            OutOfCombat,
            WaitingForTurn,
            IsTurn,
            StunnedOrSleep,
            HasMoved,
            Dead
        }

        protected Creature(string name, int hitPoints, int x, int y, IMovementStrategy movementStrategy)
        {
            Name = name;
            HitPoints = hitPoints;
            X = x;
            Y = y;
            _movementStrategy = movementStrategy;
        }

        public abstract int Hit();

        public virtual void ReceiveHit(int damage)
        {
            int reducedDamage = CalculateReducedDamage(damage);
            HitPoints -= reducedDamage;
            if (HitPoints <= 0)
            {
                HitPoints = 0;
                Console.WriteLine($"{Name} has died!");
            }
        }

        public void Loot(WorldObject obj)
        {
            if (obj is AttackItem attackItem)
            {
                AttackItems.Add(attackItem);
                Console.WriteLine($"{Name} has picked up {attackItem.Name}");
            }
            else if (obj is DefenceItem defenceItem)
            {
                DefenceItems.Add(defenceItem);
                Console.WriteLine($"{Name} has picked up {defenceItem.Name}");
            }
        }

        private int CalculateReducedDamage(int damage)
        {
            int totalReduction = DefenceItems.Sum(d => d.ReduceHitPoint);
            return Math.Max(0, damage - totalReduction);
        }

        public void PrintStats()
        {
            Console.WriteLine($"{Name} has {HitPoints} HP");
        }

        public void StartTurn()
        {
            if (HitPoints <= 0)
            {
                Console.WriteLine($"{Name} is dead and cannot take their turn!");
                return;
            }

            State = CreatureState.IsTurn;
            Console.WriteLine($"{Name} is taking their turn!");
        }

        public void EndTurn()
        {
            State = CreatureState.WaitingForTurn;
            Console.WriteLine($"{Name} has ended their turn!");
        }

        public void Move(int newX, int newY, World world)
        {
            _movementStrategy.Move(this, newX, newY, world);
        }

        public void ChangeState(CreatureState newState)
        {
            State = newState;
        }

        public void Defend()
        {
            if (State != CreatureState.IsTurn && State != CreatureState.HasMoved)
            {
                Console.WriteLine($"{Name} cannot defend right now!");
                return;
            }
            Console.WriteLine($"{Name} takes a defensive stance! Damage reduction increased by 50% this turn.");
            foreach (var defenseItem in DefenceItems)
            {
                defenseItem.ReduceHitPoint = (int)(defenseItem.ReduceHitPoint * 1.5);
            }
            EndTurn();
        }

        public void Attack(Creature target)
        {
            if (State != CreatureState.IsTurn && State != CreatureState.HasMoved)
            {
                Console.WriteLine($"{Name} cannot attack right now!");
                return;
            }
            int damage = Hit();
            target.ReceiveHit(damage);
            Console.WriteLine($"{Name} attacked {target.Name} for {damage} damage!");
            Console.WriteLine($"{target.Name} has {target.HitPoints} HP left!");
            EndTurn();
        }


        /* Gammel kode der er blevet refaktoreret
        // Jeg har tilføjet en check for om newX og newY er udenfor world.MaxX og world.MaxY for at undgå at creature kan bevæge sig udenfor worldet.
        // Jeg bruger IsPositionOccupied() fra min world class, for at undgå at to creatures kan stå på samme position.
        // Jeg har også tilføjet en Console.WriteLine for at give besked om at creature ikke kan bevæge sig til den nye position.

        // Jeg laver en variabel distance, som er lig med forskellen mellem newX og X plus forskellen mellem newY og Y.
        // Jeg bruger Math.Abs for at sikre at distance er positiv.
        // Jeg laver en if statement, som tjekker om distance er større end 2. Dette er bare en vilkårlig værdi, som jeg har valgt.
        // Hvis distance er større end 2, så skriver den en besked til consolen om at creature ikke kan bevæge sig så langt.
        // Skiftet til Euclidean distance, da det er den mest almindelige formel for distance mellem to punkter i et koordinatsystem.
        //public void Move(int newX, int newY, World world)
        //{

        //if (State != CreatureState.IsTurn && State != CreatureState.OutOfCombat)
        //{
        //    Console.WriteLine($"{Name} cannot move right now!");
        //    return;
        //}


        //    int maxDistance = 2; 
        //    int distance = (int)Math.Sqrt(Math.Pow(newX - X, 2) + Math.Pow(newY - Y, 2));

        //    if (distance > maxDistance)
        //    {
        //        Console.WriteLine($"{Name} tried to move too far!");
        //        return;
        //    }

        //    if (newX < 0 || newX >= world.MaxX || newY < 0 || newY >= world.MaxY)
        //    {
        //        Console.WriteLine($"{Name} tried to move out of bounds!");
        //        return;
        //    }

        //    if (world.IsPositionOccupied(newX, newY))
        //    {
        //        Console.WriteLine($"{Name} tried to move, but the space is occupied!");
        //        return;
        //    }

        //    X = newX;
        //    Y = newY;

        //    Console.WriteLine($"{Name} has moved to ({X}, {Y})");
        //    State = CreatureState.HasMoved;
        //}



        // Made a weapon checker to see if the creature has any weapons in their inventory
        // If the creature has no weapons, it will default to a punch attack
        // The attack method will calculate the distance to the target using Euclidean distance
        // If the target is out of range, it will print a message to the console
        // The hit chance is calculated using a random number between 1 and 100
        // If the hit chance is greater than the weapon's hit chance, it will print a message to the console
        // The damage is calculated by subtracting the target's defence items from the weapon's damage
        // If the damage is less than 0, it will default to 0
        // The target will receive the damage and the console will print a message with the damage dealt and the target's remaining hit points
        public void Attack(Creature target)
        {
            if (State != CreatureState.IsTurn && State != CreatureState.HasMoved)
            {
                Console.WriteLine($"{Name} cannot attack right now!");
                return;
            }

            // weapon checker logic
            AttackItem weapon;
           
            if (AttackItems.Count > 0)
            {
                weapon = AttackItems[0];
            }
            else
            {
                weapon = new AttackItem("Punch", X, Y, false, false, 1, 40, 1);
            }

          
            int distance = (int)Math.Sqrt(Math.Pow(target.X - X, 2) + Math.Pow(target.Y - Y, 2));

            if (distance > weapon.Range)
            {
                Console.WriteLine($"{Name} tried to attack {target.Name}, but they are out of range!");
                return;
            }

            // Hit Chance logic
            Random random = new Random();
            int hitChance = random.Next(1, 100); 
            if (hitChance > weapon.Hit)
            {
                Console.WriteLine($"{Name} tried to attack {target.Name}, but missed!");
                return;
            }

            // Damage calculation
            int damage = weapon.Damage - target.DefenceItems.Sum(d => d.ReduceHitPoint);
            damage = Math.Max(0, damage);
            
            target.ReceiveHit(damage);
            
            Console.WriteLine($"{Name} attacked {target.Name} for {damage} damage!");
            Console.WriteLine($"{target.Name} has {target.HitPoints} HP left!");

            EndTurn();
        }


        public void Defend()
        {
            if (State != CreatureState.IsTurn && State != CreatureState.HasMoved)
            {
                Console.WriteLine($"{Name} cannot defend right now!");
                return;
            }

            Console.WriteLine($"{Name} takes a defensive stance! Damage reduction increased by 50% this turn.");

            foreach (var defenseItem in DefenceItems)
            {
                defenseItem.ReduceHitPoint = (int)(defenseItem.ReduceHitPoint * 1.5);
            }

            EndTurn();
        }

        */

    }
}
