using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
  class Game
  {
    private Terrain[,] terrain;
    private List<Monster> monsters = new List<Monster>();
    private Creature[,] gameBoard;
    private Random ran = new Random();
    private Player player;

    private int playerMovement = 0;

    public Game()
    {
      gameBoard = new Creature[20, 20];
      terrain = new Terrain[20, 20];
      initializeBoard();
    }

    public Game(int width, int height)
    {
      gameBoard = new Creature[width, height];
      terrain = new Terrain[width, height];
      initializeBoard();
    }

    private void initializeBoard()
    {
      for(int i=0; i < terrain.GetLength(0); i++)
      {
        for(int j=0; j < terrain.GetLength(1); j++)
        {
          Terrain tempT = new Terrain();
          int typeSelect = ran.Next(10);
          if (typeSelect < 6)
          {
            tempT.Type = Terrain.TerrainType.FLOOR;
            terrain[i, j] = tempT;
          } else if(typeSelect < 8)
          {
            tempT.Type = Terrain.TerrainType.WALL;
            terrain[i, j] = tempT;
          } else
          {
            tempT.Type = Terrain.TerrainType.DOOR;
            terrain[i, j] = tempT;
          }
        }
      }
    }

    public bool AddMonster()
    {
      // later should add random monster
      // Spawn "orcs"
      // should load from file
      Monster orc = new Monster("O");
      orc.MovementSpeed = 4;
      orc.HitPoints = 4;
      orc.Damage = 1;

      // place random
      int[] pos = new int[2];
      pos[0] = ran.Next(gameBoard.GetLength(0));
      pos[1] = ran.Next(gameBoard.GetLength(1));
      orc.Coord = pos;
      if(gameBoard[pos[0],pos[1]] == null)
      {
        gameBoard[pos[0], pos[1]] = orc;
        monsters.Add(orc);
        return true;
      }
      return false;
    }

    public void AddPlayer(Player play)
    {
      player = play;
    }
    public void Run()
    {
      while (true)
      {
        Console.Clear();
        // Monster section
        if (playerMovement == player.MovementSpeed)
        {
          // run all monsters
          foreach (Monster mon in monsters)
          {
            randomMonsterWalk(mon);
          }
          if (player.Isdead())
          {
            break;
          }
          playerMovement = 0;
        }

        printBoard();
        ConsoleKeyInfo key = Console.ReadKey();
        // player section
        if (key.Key == ConsoleKey.Escape)
        {
          break;
        }
        else if(key.Key == ConsoleKey.Spacebar)
        {
          AddMonster();
        }
        else if (key.Key == ConsoleKey.W)
        {
          int xcor = player.Coord[0] - 1;
          int ycor = player.Coord[1] ;
          playerAction( xcor, ycor);
          playerMovement++;
        }
        else if (key.Key == ConsoleKey.S)
        {
          int xcor = player.Coord[0] + 1;
          int ycor = player.Coord[1] ;
          playerAction( xcor, ycor);
          playerMovement++;
        }
        else if (key.Key == ConsoleKey.A)
        {
          int xcor = player.Coord[0] ;
          int ycor = player.Coord[1] - 1;
          playerAction( xcor, ycor);
          playerMovement++;
        }
        else if (key.Key == ConsoleKey.D)
        {
          int xcor = player.Coord[0] ;
          int ycor = player.Coord[1] + 1;
          playerAction( xcor, ycor);
          playerMovement++;
        }
      }
    }
    private void printBoard()
    {
      // print player stats
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Player name: " + player.Name);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Player hitpoints: " + player.HitPoints);
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Player level: " + player.Level);
      Console.ForegroundColor = ConsoleColor.White;
      for (int i = 0; i < gameBoard.GetLength(0); i++)
      {
        for (int j = 0; j < gameBoard.GetLength(1); j++)
        {
          Creature cre = gameBoard[i, j];
          if (cre == null)
          {
            if (terrain[i, j].Type == Terrain.TerrainType.WALL)
            {
              Console.ForegroundColor = ConsoleColor.Gray;
              Console.Write("#");
            } else if(terrain[i,j].Type == Terrain.TerrainType.DOOR)
            {
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.Write("D");
            } else
            {
              Console.Write("¤");
            }
          }
          else if (cre is Monster)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(((Monster)cre).Type);
          }
          else
          {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(((Player)cre).Name.Substring(0, 1));
          }
          Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
      }
    }
    private void randomMonsterWalk(Monster cre)
    {
      Random ran = new Random();
      int xcor = 0;
      int ycor = 0;
      for (int i = 0; i < cre.MovementSpeed; i++)
      {
        int dir = ran.Next(4);
        switch (dir)
        {
          case 0:
            // move up
            xcor = cre.Coord[0];
            ycor = cre.Coord[1] + 1;
            if (checkCoords(xcor, ycor))
            {
              monsterAction(cre, xcor, ycor);
            }
            break;
          case 1:
            // move down
            xcor = cre.Coord[0];
            ycor = cre.Coord[1] - 1;
            if (checkCoords(xcor, ycor))
            {
              monsterAction(cre, xcor, ycor);
            }
            break;
          case 2:
            // move left
            xcor = cre.Coord[0] + 1;
            ycor = cre.Coord[1];
            if (checkCoords(xcor, ycor))
            {
              monsterAction(cre, xcor, ycor);
            }
            break;
          case 3:
            // move right
            xcor = cre.Coord[0] - 1;
            ycor = cre.Coord[1];
            if (checkCoords(xcor, ycor))
            {
              monsterAction(cre, xcor, ycor);
            }
            break;
        }
      }
    }
    private void monsterAction(Monster mon, int xcor, int ycor)
    {
      Creature upCre = gameBoard[xcor, ycor];
      if (upCre == null)
      {
        gameBoard[mon.Coord[0], mon.Coord[1]] = null;
        mon.Coord[0] = xcor;
        mon.Coord[1] = ycor;
        gameBoard[mon.Coord[0], mon.Coord[1]] = mon;
      }
      else if (upCre is Player)
      {
        mon.AttackCreature(upCre);
        if (upCre.Isdead())
        {
          gameBoard[xcor, ycor] = null;
        }
      }
    }
    private bool checkCoords(int xcor, int ycor)
    {
      if (xcor >= gameBoard.GetLength(0) || xcor < 0 || ycor >= gameBoard.GetLength(1) || ycor < 0)
      {
        return false;
      }
      return true;
    }
    private void playerAction(int xcor, int ycor)
    {
      if (checkCoords(xcor, ycor))
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
            monsters.Remove((Monster)upCre);
            gameBoard[xcor, ycor] = null;
            player.Level++;
          }
        }
      }
    }

    private void closestPathAStar(int[] start, int[] goal)
    {

    }
  }
}
