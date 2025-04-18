using System;
using System.Diagnostics;
using System.Threading;
using AdvSoftMandatory2DWorld.Classes;
using AdvSoftMandatory2DWorld.Classes.Interfaces;
using AdvSoftMandatory2DWorld.Classes.SubCreatures;
using AdvSoftMandatory2DWorld.Classes.SubWorldObjects;
using MandatoryAdvDemo;

class Program
{
    
    static void Main()
    {
        /// <summary> Demonstration of the game world and its components. And also my testing grounds for new features etc. </summary>

        // Set up console logging via Trace
        Trace.Listeners.Clear();
        Trace.Listeners.Add(new ConsoleTraceListener()); 

        // Optional: add file logging
        Trace.Listeners.Add(new TextWriterTraceListener("logs.txt"));
        // flush output on app exit
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Trace.Flush();

        // World setup via CONFIG
        var config = ConfigLoader.Load("gameconfig.json");
        Console.WriteLine($"Loaded world size: {config.World.MaxX} x {config.World.MaxY}");
        Console.WriteLine($"Game Level: {config.GameLevel}");

        World gameWorld = new World(10, 5); // fixed size for this battle


        static void PrintStats(Creature a, Creature b)
        {
            Console.WriteLine($"[HP] {a.Name}: {a.HitPoints} HP\t{b.Name}: {b.HitPoints} HP");
        }

        // MAGE SETUP
        var mage = new Mage("Gandalf", 50, 9, 2, new DefaultMovementStrategy());
        mage.AddObserver(new DamageLogger());
        mage.AddSpell(new CooldownSpell("Fireball", 15, 75, 5, 2)); // + Ice Bolt (always)
        gameWorld.AddCreature(mage);

        // WARRIOR SETUP
        AttackItem lootableSword = new AttackItem("Sword", 1, 2, lootable: true, removable: true, damage: 17, hitChance: 70, range: 1);
        gameWorld.AddObject(lootableSword);
        var warrior = new Warrior("Conan", 80, 0, 2, new DefaultMovementStrategy());

        warrior.AddObserver(new DamageLogger());
        gameWorld.AddCreature(warrior);

        Console.WriteLine("\n--- BATTLE BEGINS ---");

        int turn = 1;
        while (mage.HitPoints > 0 && warrior.HitPoints > 0)
        {
            Console.WriteLine($"\n--- Turn {turn} ---");

            // Mage's Turn
            if (mage.HitPoints > 0)
            {
                mage.StartTurn();

                // Show cooldowns
                foreach (var spell in mage.AttackItems.OfType<ICooldownAttackItem>())
                {
                    Logger.Log($"{mage.Name}'s {spell.Name} cooldown: {spell.RemainingCooldown} turn(s)");
                }

                if (GetDistance(mage, warrior) <= 5)
                {
                    mage.Attack(warrior, gameWorld);
                }
                else
                {
                    int newX = Math.Max(mage.X - 1, warrior.X + 1);
                    mage.Move(newX, mage.Y, gameWorld);
                    mage.EndTurn();
                }

               
                foreach (var spell in mage.AttackItems.OfType<ICooldownAttackItem>())
                {
                    spell.TickCooldown();
                }
            }

            Thread.Sleep(500);

            // Warrior's Turn
            if (warrior.HitPoints > 0)
            {
                warrior.StartTurn();

                // Try looting if there's a weapon nearby
                var nearbyWeapon = gameWorld.Objects
                    .OfType<AttackItem>()
                    .FirstOrDefault(o => o.Lootable &&
                                         Math.Abs(o.X - warrior.X) <= 1 &&
                                         Math.Abs(o.Y - warrior.Y) <= 1);

                if (nearbyWeapon != null)
                {
                    gameWorld.Objects.Remove(nearbyWeapon);

                    var boosted = new BoostedAttackItem(nearbyWeapon);
                    warrior.AddWeapon(boosted);
                }

                // If he's now armed and in range, attack!
                if (warrior.AttackItems.Any() && GetDistance(warrior, mage) <= 1)
                {
                    warrior.Attack(mage, gameWorld);
                }
                else
                {
                    int newX = Math.Min(warrior.X + 1, mage.X - 1);
                    warrior.Move(newX, warrior.Y, gameWorld);
                    warrior.EndTurn();
                }
            }
            PrintStats(mage, warrior);

            Thread.Sleep(1000);
            turn++;
        }

        Console.WriteLine("\n--- BATTLE OVER ---");
        if (mage.HitPoints <= 0 && warrior.HitPoints <= 0)
            Console.WriteLine("It's a draw! Both have fallen.");
        else if (mage.HitPoints <= 0)
            Console.WriteLine("🏆 Warrior wins!");
        else
            Console.WriteLine("🏆 Mage wins!");
        
    }

    static int GetDistance(Creature a, Creature b)
    {
        return (int)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }

    

}

