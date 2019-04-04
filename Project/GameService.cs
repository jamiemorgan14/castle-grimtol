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

    public void Setup()
    {

      Room basic = new Room("Basic Training", "You're in basic training. The foundation of your career.  Go (north) to travel to your first base");
      Room firstBase = new Room("First Base", "Your first base is where most of your time will be spent");
      Room workCenter = new Room("Work Center", "Your work center is where you will be spending more time than you ever thought possible");
      Room fco = new Room("The Flight Chief's Office", "You've been reprimanded! Take better care in the choices you make, or you will get kicked out");
      Room wlb = new Room("The Room of Work/Life Balance", "You've realized that you need to maintain a quality work/life balance");
      Room roc = new Room("Room of Clarity", "You've decided it's time to get out.");
      Room separation = new Room("Separation Room!", "The room you've been waiting for.  Find the DD-214 to exit through the door to the civilian world");

      //create items
      Item morale = new Item("Morale!", "Morale reduces the effects of the punishments your flight chief has given you.");
      Item dd214 = new Item("DD-214!", "If you can find your way to the door to the civilian world, this will serve as your key to unlock it!");

      //add items to room
      wlb.AddItem(morale);
      roc.AddItem(dd214);
      //create relationships
      basic.AddExit("north", firstBase);
      firstBase.AddExit("north", workCenter);
      workCenter.AddExit("west", fco);
      workCenter.AddExit("east", wlb);
      workCenter.AddExit("north", separation);
      wlb.AddExit("west", workCenter);
      wlb.AddExit("north", roc);
      separation.AddExit("east", roc);
      separation.AddExit("south", workCenter);
      roc.AddExit("west", separation);
      roc.AddExit("south", wlb);

      //the fco can be entered from any room, but can only exit into work center.
      fco.AddExit("east", workCenter);




      CurrentRoom = basic;
      Playing = true;
    }
    public void StartGame()
    {
      Setup();
      while (Playing)
      {

        System.Console.WriteLine($"{CurrentRoom.Name}: {CurrentRoom.Description}");
        System.Console.WriteLine("Enter a direction to navigate through your career!");
        string input = Console.ReadLine().ToLower();
        string userCommand = GetUserInput(input);

      }
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
            TakeItem();
            break;
          default:
            System.Console.WriteLine("Unrecognized command");
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

    public void Go(string direction)
    {
      CurrentRoom = (Room)CurrentRoom.TravelToRoom(direction);
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      if (CurrentRoom.Item.Name.Length == 0)
      {
        System.Console.WriteLine("There's nothing much to see in this room");
      }
      else
      {
        System.Console.WriteLine($"As you look around the room, you notice that there is a rare {CurrentRoom.Item.Name}. (Take) the item?");
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
      throw new System.NotImplementedException();
    }

    public void TakeItem()
    {
      Item itemInRoom = CurrentRoom.Item;
      CurrentPlayer.Inventory.Add(itemInRoom);
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}