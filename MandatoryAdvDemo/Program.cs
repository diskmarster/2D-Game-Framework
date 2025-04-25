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

        // Testing: add file logging - Used it for debugging. It is located in the root folder of the project..
        Trace.Listeners.Add(new TextWriterTraceListener("logs.txt"));

        // flush output on app exit
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Trace.Flush();

        // World setup via CONFIG
        var config = ConfigLoader.Load("gameconfig.json");
        Console.WriteLine($"Loaded world size: {config.World.MaxX} x {config.World.MaxY}");
        Console.WriteLine($"Game Level: {config.GameLevel}");

        World gameWorld = new World(config.World.MaxX, config.World.MaxY);


        static void PrintStats(Creature a, Creature b)
        {
            Console.WriteLine($"[HP] {a.Name}: {a.HitPoints} HP\t{b.Name}: {b.HitPoints} HP");
        }

        // MAGE SETUP
        var mage = new Mage("Gandalf", 50, 9, 2, new DefaultMovementStrategy());
        mage.AddObserver(new DamageLogger());
        mage.AddSpell(new CooldownSpell("Fireball", 15, 75, 5, 2)); // + Ice Bolt (always)
        gameWorld.AddCreature(mage);


        // Adding a spell combo(commented out for now) (composite pattern)
        var combo = new AttackItemComposite("Firestorm");
        combo.Add(new CooldownSpell("Fireball", 15, 75, 5, 2));
        combo.Add(new CooldownSpell("Ice Bolt", 3, 100, 3, 1));
        mage.AddSpell(combo);


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

            /// <summary>Ive made a small simulation to demonstrate the mage doing 3 thing: 1. Checking Cooldowns of spells 2. If the mage is within the range of 5 he can start attacking (casting spells) 
            /// 3. if not, he will move a step closer on X angle untill he is close enough </summary>
            if (mage.HitPoints > 0)
            {
                mage.StartTurn();

                
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

            /// <summary >Warrior's Turn - 3 parts: 
            /// 1. Looting: The warrior checks if there is any loot (oftype "AttackItem") because he is checking for a sword, if there is he will unequip the weapon he has if any, and then apply a boost.
            /// 2. Attacking: If the warrior is within 1 range of the mage he will perform the Attack() 
            /// 3. Movign: if not in range for an attack he will take a step closer to mage via the X angle</summary> 
            if (warrior.HitPoints > 0)
            {
                warrior.StartTurn();

                
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

