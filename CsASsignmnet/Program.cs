using System;

namespace ConsoleApp3;




public class Character : Entity, IInteractive
{
    // Private fields
    private string name;
    private int health;
    private int energy;

    // Public properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public int Energy
    {
        get => energy;
        set => energy = value;
    }

   //constructor
    public Character(string name, int health, int energy)
    {
        this.name = name;
        this.health = health;
        this.energy = energy;
    }

    // Methods
    public virtual void Move()
    {
        Console.WriteLine(name +" is moving.");
    }

    public virtual void Interact()
    {
        Console.WriteLine(name + "{ interacts.");
    }

    // Implementation of abstract methods from Entity
    public override void Update()
    {
        Console.WriteLine(name + "is being updated.");
    }

    public override void Render()
    {
        Console.WriteLine(name + " is being rendered.");
    }
}

public class PlayerCharacter : Character
{


    // Constructor
    public PlayerCharacter(string name, int health, int energy)
        : base(name, health, energy)
    {
        
    }

    // Methods
    public void Attack()
    {
        Console.WriteLine(Name + " attacks with no parameters.");
    }

    public void Attack(int damage)
    {
        Console.WriteLine(Name+ "attacks with {damage} damage.");
    }

    public void Heal()
    {
        Console.WriteLine(Name+" heals.");
    }

   
    public override void Interact()
    {
        Console.WriteLine(Name+ "interacts as a player.");
    }
}


public class NonPlayerCharacter : Character
{
 

    // Constructor
    public NonPlayerCharacter(string name, int health, int energy)
        : base(name, health, energy)
    {
       
    }

    
    public void Trade()
    {
        Console.WriteLine(Name+" is trading.");
    }

    // Override Interact() method
    public override void Interact()
    {
        Console.WriteLine(Name + " interacts as an NPC.");
    }
}

// Environment class
public class Environment : Entity
{
    // Private field
    private string locationName;
    private List<Character> characters = new List<Character>();

    // Public properties
    public string LocationName
    {
        get => locationName;
        set => locationName = value;
    }

    public List<Character> character => characters;

    // Constructor
    public Environment(string locationName)
    {
        this.locationName = locationName;
    }

    // Methods
    public void AddCharacter(Character character)
    {
        characters.Add(character);
        Console.WriteLine(character.Name+ "has been added to the environment.");
    }

    public void RemoveCharacter(Character character)
    {
        if (characters.Contains(character))
        {
            characters.Remove(character);
            Console.WriteLine(character.Name +" has been removed from the environment.");
        }
        else
        {
            Console.WriteLine(character.Name + " not in the environment.");
        }
    }



    // Implementation of abstract methods from Entity
    public override void Update()
    {
        Console.WriteLine($"Updating environment at {LocationName}.");
    }

    public override void Render()
    {
        Console.WriteLine($"Rendering environment at {LocationName}.");
    }
}

//abstraction
public abstract class Entity
{
    public abstract void Update();
    public abstract void Render();
}

//interface
public interface IInteractive
{
    void Interact();
    void Update();
}


// E-handling
public class InvalidMoveException : Exception
{
    public InvalidMoveException(string message) : base(message) { }
}

public class InsufficientEnergyException : Exception
{
    public InsufficientEnergyException(string message) : base(message) { }
}

// Main program
public class Program
{
    public static void Main()
    {
        try
        {
          
            // Create a new environment
            Console.Write("Enter the environment name: ");
            string environmentName = Console.ReadLine();
            Environment gameWorld = new Environment(environmentName);
              
            // Get player character details from the user
            Console.Write("Enter player name: ");
            string playerName = Console.ReadLine();

            Console.Write("Enter player health: ");
            int playerHealth = int.Parse(Console.ReadLine());

            Console.Write("Enter player energy: ");
            int playerEnergy = int.Parse(Console.ReadLine());



            // Create a new PlayerCharacter
            PlayerCharacter player = new PlayerCharacter(playerName, playerHealth, playerEnergy);

            // Get NPC details from the user
            Console.Write("Enter NPC name: ");
            string npcName = Console.ReadLine();

            Console.Write("Enter NPC health: ");
            int npcHealth = int.Parse(Console.ReadLine());

            Console.Write("Enter NPC energy: ");
            int npcEnergy = int.Parse(Console.ReadLine());



            // Create a new NonPlayerCharacter
            NonPlayerCharacter npc = new NonPlayerCharacter(npcName, npcHealth, npcEnergy);

            // Add characters to environment
            gameWorld.AddCharacter(player);
            gameWorld.AddCharacter(npc);

            Console.WriteLine("List of character");
            foreach (Character character in gameWorld.character)
            {
                Console.WriteLine($"{character.Name} ");
            }


            
            string command;
            do
            {
                Console.WriteLine("\nEnter a command (move, attack, trade, interact, quit): ");
                command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "move":
                        player.Move();
                        break;
                    case "attack":
                        Console.WriteLine("Enter attack value ");
                        int damage = int.Parse(Console.ReadLine());//change this to two type of attack
                        player.Attack(damage);
                        break;
                    case "trade":
                        npc.Trade();
                        break;
                    case "interact":
                        player.Interact();
                        npc.Interact();
                        break;
                    case "quit":
                        Console.WriteLine("Exiting the game.");
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            } while (command != "quit");
        
       }
        catch (InvalidMoveException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (InsufficientEnergyException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
        finally
        {
            Console.WriteLine("code ended succesfully");
        }
    }
}


//customization and learn about many key words