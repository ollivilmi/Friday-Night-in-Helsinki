using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPC
{
    public class NPCBouncer : NPCStory
    {
        public NPCBouncer(Player.Player player) : base(player)
        {
            Functionality = "Can I buy a ticket?";
        }
    }
}
