using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Lib
{
    public class Slot
    {
        private bool isPlayer;
        private int column { get; set; }
        private int row { get; set; }

        public Slot(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public void tokenPlayed(bool isPlayer)
        {
            this.isPlayer = isPlayer;
        }

        public bool getIsPlayer()
        {
            return isPlayer;
        }
    }
}
