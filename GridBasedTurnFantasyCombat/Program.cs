using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
  class Program
  {
    static void Main(string[] args)
    {
      // Game board
      Creature[,] gameBoard = new Creature[20, 20];
      Random ran = new Random();

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
        // Spawn 6 "orcs"
        Monster orc = new Monster("O");
        orc.MovementSpeed = 4;
        orc.HitPoints = 4;
        orc.Damage = 1;

        // place random
        int[] pos = new int[2];
        pos[0] = ran.Next(gameBoard.GetLength(0));
        pos[1] = ran.Next(gameBoard.GetLength(1));

        gameBoard[pos[0], pos[1]] = orc;
      }

      // Add the player 
      gameBoard[player.Coord[0], player.Coord[0]] = player;

      //print board
      //Main game loop
      bool run = true;
      int movement = 0;
      while (run)
      {
        Console.Clear();
        printBoard(gameBoard);
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Escape)
        {
          run = false;
        }
        else if (key.Key == ConsoleKey.W)
        {
          int xcor = player.Coord[0];
          int ycor = player.Coord[1] + 1;
          if (checkCoords(xcor, ycor, gameBoard))
          {
            playerAction(player, xcor, ycor, gameBoard);
            movement++;
          }
        }
        else if (key.Key == ConsoleKey.S)
        {
          int xcor = player.Coord[0];
          int ycor = player.Coord[1] - 1;
          if (checkCoords(xcor, ycor, gameBoard))
          {
            playerAction(player, xcor, ycor, gameBoard);
            movement++;
          }
        }
        else if (key.Key == ConsoleKey.A)
        {
          int xcor = player.Coord[0] - 1;
          int ycor = player.Coord[1];
          if (checkCoords(xcor, ycor, gameBoard))
          {
            playerAction(player, xcor, ycor, gameBoard);
            movement++;
          }
        }
        else if (key.Key == ConsoleKey.D)
        {
          int xcor = player.Coord[0] + 1;
          int ycor = player.Coord[1];
          if (checkCoords(xcor, ycor, gameBoard))
          {
            playerAction(player, xcor, ycor, gameBoard);
          }
          movement++;
        }
      }
      Console.ReadKey();
    }

    private static void playerAction(Player player, int xcor, int ycor, Creature[,] gameBoard)
    {
      Creature upCre = gameBoard[xcor, ycor];
      if (upCre == null)
      {
        gameBoard[player.Coord[0], player.Coord[1]] = null;
        player.Coord[0] = xcor;
        player.Coord[1] = ycor;
        gameBoard[player.Coord[0], player.Coord[1]] = player;
      }
      else if (upCre is Monster)
      {
        player.AttackCreature(upCre);
        if (upCre.Isdead())
        {
          gameBoard[xcor, ycor] = null;
        }
      }
    }

    private static bool checkCoords(int xcor, int ycor, Creature[,] gameBoard)
    {
      if (xcor >= gameBoard.GetLength(0) || xcor < 0 || ycor >= gameBoard.GetLength(1) || ycor < 0)
      {
        return false;
      }
      return true;
    }

    private static void printBoard(Creature[,] board)
    {
      for (int i = 0; i < board.GetLength(0); i++)
      {
        for (int j = 0; j < board.GetLength(1); j++)
        {
          Creature cre = board[i, j];
          if (cre == null)
          {
            Console.Write("#");
          }
          else if (cre is Monster)
          {
            Console.Write(((Monster)cre).Type);
          }
          else
          {
            Console.Write(((Player)cre).Name.Substring(0, 1));
          }
        }
        Console.WriteLine();
      }
    }
  }
}
