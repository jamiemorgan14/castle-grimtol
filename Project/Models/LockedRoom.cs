using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class LockedRoom : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public bool Locked { get; set; } = true;

    public void AddExit(string direction, IRoom destination)
    {
      Exits.Add(direction, destination);
    }
    public void AddItem(Item item)
    {
      Items.Add(item);
    }

    public LockedRoom(string name, string description, bool locked, Item item = null)
    {
      Name = name;
      Description = description;
      Exits = new Dictionary<string, IRoom>();
      Locked = locked;
    }
  }
}