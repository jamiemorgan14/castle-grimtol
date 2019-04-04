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
      Room firstBase = new Room("First Base", "Your first base is where most of your time will be spent");
      Room workCenter = new Room("Work Center", "Your work center is where you will be spending more time than you ever thought possible");
      Room fco = new Room("The Flight Chief's Office", "You've been reprimanded! Take better care in the choices you make, or you will get kicked out");
      Room wlb = new Room("The Room of Work/Life Balance", "You've realized that you need to maintain a quality work/life balance");
      Room roc = new Room("Room of Clarity", "You've decided it's time to get out.");
      Room separation = new Room("Separation Room!", "The room you've been waiting for.  Find the DD-214 to exit through the door to the civilian world");
      //create relationships
      basic.AddExit(Direction.north, firstBase);
      firstBase.AddExit(Direction.north, workCenter);
      workCenter.AddExit(Direction.west, fco);
      workCenter.AddExit(Direction.east, wlb);
      workCenter.AddExit(Direction.north, separation);
      wlb.AddExit(Direction.west, workCenter);
      wlb.AddExit(Direction.north, roc);
      separation.AddExit(Direction.east, roc);
      separation.AddExit(Direction.south, workCenter);
      roc.AddExit(Direction.west, separation);
      roc.AddExit(Direction.south, wlb);

      //the fco can be entered from any room, but can only exit into work center.
      fco.AddExit(Direction.west, workCenter);




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

        //find out how to make chosen direction the user input!!!!!!!!!!!
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