using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public List<Item> Inventory { get; set; }


    public Player()
    {
      Inventory = new List<Item>();
    }

  }
}