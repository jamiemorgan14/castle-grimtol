using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<Direction, IRoom> Exits { get; set; }

    internal IRoom TravelToRoom(Direction playersChosenDirection)
    {
      if (Exits.ContainsKey(playersChosenDirection))
      {
        return Exits[playersChosenDirection];
      }
      System.Console.WriteLine("You can't go that way!");
      return (IRoom)this;
    }
    internal void AddExit(Direction direction, IRoom destination)
    {
      Exits.Add(direction, destination);
    }
    public Room(string name, string description, Item item = null)
    {
      Name = name;
      Description = description;
      Exits = new Dictionary<Direction, IRoom>();
    }

  }
  public enum Direction
  {
    north,
    south,
    east,
    west
  }
}