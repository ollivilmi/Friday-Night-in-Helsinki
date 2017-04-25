using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Beer : Item
    {
        public Beer(Player player, int amount)
        {
            this.player = player;
            this.price = 1.1f;
            this.amount = amount;
            this.itemName = "Beer";
        }

        public override void UseItem()
        {
            if (amount > 0)
            {
                amount--;
                player.drink(10);
            }
        }
    }
}
