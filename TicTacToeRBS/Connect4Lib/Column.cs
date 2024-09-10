using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Lib
{
    public class Column
    {
        private int slots;

        private bool isFull = false;

        public Column(int slots)
        {
            this.slots = slots;
        }

        public int addPiece()
        {
            slots--; // grid is n-1 indexed, so the slots should be as well
            //slotList[slots].tokenPlayed(isPlayer);

            if(slots <= 0)
            {
                isFull = true;
            }
            return slots;
        }

        public bool getIsFull()
        {
            return isFull;
        }
    }
}
