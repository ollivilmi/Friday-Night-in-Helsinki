using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class QuestItem : Item
    {
        public QuestItem(Player player)
        {
            this.player = player;
            this.amount = 0;
            this.itemName = "Quest Item";
        }

        public override void UseItem()
        {
            if (amount > 0)
            {
                amount--;
                player.useMoney(10);
            }
        }
    }
}
