using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Tobacco : Item
    {
        public Tobacco(Player player)
        {
            this.price = 0.2f;
            this.amount = 0;
            this.itemName = "Tobacco";
            this.player = player;
        }

        public override void UseItem()
        {
            if (amount > 0)
            {
                amount--;
                player.haveFun(10);
            }
        }
    }
}
