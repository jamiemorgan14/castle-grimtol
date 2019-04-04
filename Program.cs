using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {

      System.Console.WriteLine("Welcome to the Air Force!");
      GameService gameService = new GameService();
      gameService.StartGame();
    }
  }
}
