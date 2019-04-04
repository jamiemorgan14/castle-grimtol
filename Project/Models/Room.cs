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
    public Dictionary<string, IRoom> Exits { get; set; }

    internal IRoom TravelToRoom(string playersChosenDirection)
    {
      if (Exits.ContainsKey(playersChosenDirection))
      {
        return Exits[playersChosenDirection];
      }
      else
      {
        return Exits["oops"];
      }
    }
    public void AddExit(string direction, IRoom destination)
    {
      Exits.Add(direction, destination);
    }
    public void AddItem(Item item)
    {
      Items.Add(item);
    }

    public Room(string name, string description, Item item = null)
    {
      Name = name;
      Description = description;
      Exits = new Dictionary<string, IRoom>();
      Items = new List<Item>();
    }

  }
}