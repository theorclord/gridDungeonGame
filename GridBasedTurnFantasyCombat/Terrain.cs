using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
  class Terrain
  {
    public TerrainType Type { get; set; }
    public enum TerrainType
    {
      WALL, FLOOR, DOOR
    }
  }
}
