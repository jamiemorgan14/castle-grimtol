using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public Item Item { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    internal IRoom TravelToRoom(string playersChosenDirection)
    {
      if (Exits.ContainsKey(playersChosenDirection))
      {
        return Exits[playersChosenDirection];
      }
      System.Console.WriteLine("You can't go that way!");
      return (IRoom)this;
    }

    internal void AddItem(Item item)
    {
      Item = item;
    }
    internal void AddExit(string direction, IRoom destination)
    {
      Exits.Add(direction, destination);
    }

    public Room(string name, string description, Item item = null)
    {
      Name = name;
      Description = description;
      Exits = new Dictionary<string, IRoom>();
    }

  }
}