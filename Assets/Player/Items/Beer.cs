using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Beer : Item
    {
        public Beer()
        {
            this.price = 1.1f;
            this.amount = 0;
            this.itemName = "Beer";
        }
    }
}
