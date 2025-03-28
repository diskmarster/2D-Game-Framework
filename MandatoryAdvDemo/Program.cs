using System;
using System.Threading;
using AdvSoftMandatory2DWorld.Classes;
using AdvSoftMandatory2DWorld.Classes.SubCreatures;

class Program
{
    static void Main()
    {
        World gameWorld = new World(10, 10);
        TurnManager turnManager = new TurnManager();

        /* Old test code
        //Creature player = new Warrior("Hero", 100, 2, 2);
        //gameWorld.AddCreature(player);

        // Tests for moving creature and their limits:
        // Move within boundaries
        // player.Move(3, 3, gameWorld); // ✅ Should work



        //player.Move(6, 7, gameWorld); // ❌ Should fail, moving to far

        // Add another creature and try moving into its space
        //Creature enemy = new Warrior("Enemy", 50, 4, 4);
        //gameWorld.AddCreature(enemy);

        //player.Move(4, 4, gameWorld); // ❌ Should fail (occupied space)

        // Move outside boundaries
        //player.Move(5, 5, gameWorld); // ✅ Should work
        //player.Move(7, 7, gameWorld);
        //player.Move(6, 6, gameWorld);
        //player.Move(8, 8, gameWorld);
        //player.Move(9, 9, gameWorld);
        //player.Move(11, 11, gameWorld); // ❌ Should fail (out of bounds)

        // Tests for attacking:
        //player.Attack(enemy); // ✅ Should work
        //enemy.Attack(player); // ✅ Should work
        //player.Move(5, 5, gameWorld);
        //enemy.Move(3, 3, gameWorld);
        //player.Attack(enemy); // ❌ Should fail (out of range)

        */



        // Test if turns work:


        // Create creatures
        Creature player = new Warrior("Hero", 100, 2, 2, new DefaultMovementStrategy());
        Creature enemy = new Warrior("Enemy", 50, 4, 4, new DefaultMovementStrategy());

        // Add creatures to the game world
        gameWorld.AddCreature(player);
        gameWorld.AddCreature(enemy);

        // Add creatures to the turn manager
        turnManager.AddCreature(player);
        turnManager.AddCreature(enemy);

        // Test moving within boundaries
        turnManager.NextTurn();
        player.Move(3, 3, gameWorld);
        player.Attack(enemy);  // ✅ Should be allowed after moving

        // Test defending
        turnManager.NextTurn();
        enemy.Defend();  // ✅ Should be allowed directly

        // Test moving and defending
        turnManager.NextTurn();
        player.Move(5, 5, gameWorld);
        player.Defend();  // ✅ Should be allowed after moving

        // Test attacking
        turnManager.NextTurn();
        enemy.Attack(player);  // ✅ Should be allowed directly

        // Test attacking directly
        turnManager.NextTurn();
        player.Attack(enemy);  // ✅ Should work directly and end turn

        // Test moving out of bounds
        turnManager.NextTurn();
        player.Move(11, 11, gameWorld);  // ❌ Should fail (out of bounds)

        // Test moving to an occupied space
        turnManager.NextTurn();
        player.Move(4, 4, gameWorld);  // ❌ Should fail (occupied space)

        // Test moving too far
        turnManager.NextTurn();
        player.Move(6, 7, gameWorld);  // ❌ Should fail, moving too far

        // Print final stats
        player.PrintStats();
        enemy.PrintStats();
    }
}
