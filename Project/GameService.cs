using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; }
    public int Reprimands { get; set; }
    public bool CanWin { get; set; }

    public void Setup()
    {
      CurrentPlayer = new Player(EstablishPlayer());
      //create items
      Item dd214 = new Item("DD-214!", "If you can find your way to the door to the civilian world, this will serve as your key to unlock it!");
      Item morale = new Item("Morale!", "Morale reduces the effects of the punishments your flight chief has given you.");

      Room basic = new Room("Basic Training", $"Welcome to basic training, {CurrentPlayer.Name}. The foundation of your career.  (Go north) to travel to your first base");
      Room firstBase = new Room("First Base", "Your first base is where most of your time will be spent");
      Room workCenter = new Room("Work Center", "Your work center is where you will be spending more time than you ever thought possible");
      Room fco = new Room("The Flight Chief's Office", "You've been reprimanded! Take better care in the choices you make, or you will get kicked out");
      Room wlb = new Room("The Room of Work/Life Balance", "You've realized that you need to maintain a quality work/life balance");
      Room roc = new Room("Room of Clarity", "You've decided it's time to get out.");

      //create locked room
      Room separation = new Room("Separation Room!", "The room you've been waiting for.  Find the DD-214 to exit through the door to the civilian world");

      //add items to room
      wlb.AddItem(morale);
      roc.AddItem(dd214);

      //create relationships
      basic.AddExit("north", firstBase);
      firstBase.AddExit("north", workCenter);
      workCenter.AddExit("west", fco);
      workCenter.AddExit("east", wlb);
      workCenter.AddExit("north", separation);
      workCenter.AddExit("south", firstBase);
      wlb.AddExit("west", workCenter);
      wlb.AddExit("north", roc);
      separation.AddExit("east", roc);
      separation.AddExit("south", workCenter);
      roc.AddExit("west", separation);
      roc.AddExit("south", wlb);

      //the fco can be entered from any room, but can only exit into work center.
      fco.AddExit("east", workCenter);
      fco.AddExit("oops", fco);
      basic.AddExit("oops", fco);
      firstBase.AddExit("oops", fco);
      workCenter.AddExit("oops", fco);
      wlb.AddExit("oops", fco);
      roc.AddExit("oops", fco);
      separation.AddExit("oops", fco);


      CurrentRoom = basic;
      Playing = true;
      Reprimands = 0;
      CanWin = false;
    }
    public void StartGame()
    {
      Setup();
      Help();
      System.Console.WriteLine(@"
Enter a direction to navigate through your career!");
      while (Playing)
      {
        if (Reprimands < 3)
        {
          string input = Console.ReadLine().ToLower();
          string userCommand = GetUserInput(input);
        }
        else
        {
          System.Console.WriteLine(@"
You've gotten too many reprimands. You've been court martialed. Go start from the beginning.");
          Console.ReadLine();
          Reset();
        }
      }
    }

    public string EstablishPlayer()
    {
      Console.Clear();
      System.Console.WriteLine("What is your name?");
      string name = Console.ReadLine();
      return name;
    }


    public string GetUserInput(string input)
    {
      string[] playersChoice = input.Split(" ");
      if (playersChoice.Length < 2)
      {
        switch (playersChoice[0])
        {
          case "quit":
            Quit();
            break;
          case "help":
            Help();
            break;
          case "inventory":
            Inventory();
            break;
          case "look":
            Look();
            break;
          case "take":
            System.Console.WriteLine(@"
You must see an item before you can take it.");
            break;
          case "reset":
            System.Console.WriteLine(@"
Are you sure you want to reset? You'll lose your progress! Y/N?");
            string resetChoice = Console.ReadLine().ToLower();
            if (resetChoice == "y")
            {
              Reset();
            }
            else
            {
              Console.Clear();
              System.Console.WriteLine($@"
{CurrentRoom.Name}: {CurrentRoom.Description}");
            }
            break;
          default:
            System.Console.WriteLine(@"
Unrecognized command");
            break;
        }
      }
      else
      {
        string direction = playersChoice[1];
        Go(direction);
        return playersChoice[1];
      }
      return "Good move";
    }

    public void getReprimanded()
    {
      Reprimands++;
      System.Console.WriteLine($@"
You've been reprimanded!
You now have {Reprimands} reprimands. If you can find some morale, you can elimate the effects of a punishment!");
      System.Console.WriteLine(@"
Press any key to continue");
      Console.ReadLine();

    }
    public void Go(string direction)
    {
      Console.Clear();
      if (CurrentRoom.Name == "Separation Room!" && !CanWin && direction == "north")
      {
        System.Console.WriteLine(@"
You must use the DD-214 to unlock the door to exit the separation room");
      }
      if (CurrentRoom.Name == "Separation Room!" && CanWin && direction == "north")
      {
        WinGame();
      }
      CurrentRoom = (Room)CurrentRoom.TravelToRoom(direction);
      if (CurrentRoom.Name == "The Flight Chief's Office")
      {
        getReprimanded();
      }
      System.Console.WriteLine($@"
{CurrentRoom.Name}: {CurrentRoom.Description}");

    }

    private void WinGame()
    {
      System.Console.WriteLine(@"Congratulations! You've entered the civilian world and have left the Air Force!
Press enter to play again or (q) to quit");
      string userChoice = Console.ReadLine();
      if (userChoice == "q")
      {
        Playing = !Playing;
      }
      else
      {
        Reset();
      }
    }

    public void Help()
    {
      Console.WriteLine(@"
      +----------------------------+       +-----------+
      |         Separation         |       |  room     |
      |                            |       |   of      |
      +----------------------------+       |    clarity|
                                           +-----------+

+---------+   +----------+        +------------+
|         |   |  work    |        |  work      |
| flight  |   |  center  |        |   life     |
|  chief  |   |          |        |    balance |
|   office|   |          |        |            |
+---------+   +----------+        +------------+


           +--------------------+
           |      first         |
           |       base         |
           |                    |
           +--------------------+


            +-----------------+
            |   basic         |
            |    training     |
            |                 |
            +-----------------+

use 'go (direction)' to travel from room to room.
Find the Morale! in order to negate the effects of one reprimand.
Exit the air force by leaving (north) through the separation room, but make sure you find your DD-214 first!
Commands:
Go north
   south
   east
   west
inventory - shows current items around room
look - look around the current room for items to use
quit - exits the game
restart - restarts the game from the very beginning!
help - use at any time to show map and these instructions

Careful not to take a wrong turn, you may end up in your flight chief's office!!!!

Good luck!");

      System.Console.WriteLine($"{CurrentRoom.Name}: {CurrentRoom.Description}");
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count > 0)
      {
        System.Console.WriteLine("Select and item's number to use it.");
        int i = 1;
        foreach (Item item in CurrentPlayer.Inventory)
        {
          System.Console.WriteLine($"{i}: {item.Name}");
          i++;
        }
        string itemToUseIndex = Console.ReadLine();
        UseItem(CurrentPlayer.Inventory[Int32.Parse(itemToUseIndex) - 1]);
      }
      else
      {
        System.Console.WriteLine("You have no items. (Look) around rooms to find items you can add to your inventory!");
      }
    }

    public void Look()
    {
      if (CurrentRoom.Items.Count <= 0)
      {
        System.Console.WriteLine("There's nothing much to see in this room");
      }
      else if (CurrentRoom.Items.Count == 1)
      {
        System.Console.WriteLine($"As you look around the room, you notice that there is a rare {CurrentRoom.Items[0].Name}. Would you like to take this item (Y/N)?");
        string takeItemSelection = Console.ReadLine().ToLower();
        if (takeItemSelection == "y")
        {
          TakeItem(CurrentRoom.Items[0]);
        }
      }
      else
      {
        System.Console.WriteLine("As you look around the room, you see several items: ");
        foreach (Item item in CurrentRoom.Items)
        {
          System.Console.WriteLine($"{item.Name}");
        }
      }
    }

    public void Quit()
    {
      System.Console.WriteLine("Are you sure you want to quit? (Y/N)");
      string selection = Console.ReadLine();
      if (selection.ToLower().StartsWith("y"))
      {
        System.Console.WriteLine("Thanks for playing!");
      }
      Playing = !Playing;
    }

    public void Reset()
    {
      StartGame();
    }

    public void TakeItem(Item item)
    {
      CurrentRoom.Items.Remove(item);
      CurrentPlayer.Inventory.Add(item);
      System.Console.WriteLine($"You've taken the {item.Name}");
    }

    public void UseItem(Item item)
    {
      CurrentPlayer.Inventory.Remove(item);
      if (item.Name == "Morale!" && Reprimands > 0)
      {
        Reprimands--;
        System.Console.WriteLine($"You've used morale! You now have {Reprimands} reprimands");
      }
      else if (item.Name == "Morale!" && Reprimands == 0)
      {
        System.Console.WriteLine("Your supervisor caught you trying to have morale! Go to his office.");
        getReprimanded();
      }
      else if (item.Name == "DD-214!" && CurrentRoom.Name == "Separation Room!")
      {
        CanWin = true;
        System.Console.WriteLine($"You've used the power of the {item.Name} You can exit the Air Force by travelling north!");
      }
      else if (item.Name == "DD-214!" && CurrentRoom.Name != "Separation Room!")
      {
        System.Console.WriteLine("You must be in the separation room to use the powers of the DD-214.");
      }
    }
  }
}