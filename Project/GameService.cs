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

    private void Initialize()
    {
      //create rooms
      Room basic = new Room("Basic Training", "You're in basic training. The foundation of your career.  Go (north) to travel to your first base");
      Room firstBase = new Room("First Base", "Your first base is where most of your work will take place");
      //create relationships
      basic.AddExit(Direction.north, firstBase);



      CurrentRoom = basic;
      Playing = true;
    }
    public void StartGame()
    {
      Initialize();
      while (Playing)
      {
        System.Console.WriteLine($"{CurrentRoom.Name}: {CurrentRoom.Description}");
        System.Console.WriteLine("Enter a direction to navigate through your career!");
        string playersChoice = Console.ReadLine();
        CurrentRoom = (Room)CurrentRoom.TravelToRoom(Direction.north);
      }
    }


    public void GetUserInput()
    {
      throw new System.NotImplementedException();
    }

    public void Go(string direction)
    {
      throw new System.NotImplementedException();
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
      throw new System.NotImplementedException();
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup()
    {
      throw new System.NotImplementedException();
    }

    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}