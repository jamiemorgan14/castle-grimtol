using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IPlayer
  {
    List<Item> Inventory { get; set; }
  }
}
