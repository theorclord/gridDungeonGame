using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
  class Program
  {
    public static List<Monster> monsters = new List<Monster>();
    static void Main(string[] args)
    {
      // Game board
      Game board = new Game(20, 20);

      // Player
      Player player = new Player("Erast");
      player.HitPoints = 10;
      player.Level = 1;
      player.Damage = 6;
      player.MovementSpeed = 6;
      player.Coord = new int[] { 0, 0 };

      // Creatures
      for (int i = 0; i < 6; i++)
      {
        board.AddMonster();
      }

      // Add the player 
      board.AddPlayer(player);
      
      //Main game loop
      board.Run();
      Console.ReadKey();
    }
  }
}
