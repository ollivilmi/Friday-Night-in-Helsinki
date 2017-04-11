using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueHolderDoor : DialogueHolder
    {
        public DialogueHolderDoor(Player.Player player, DialogueManager dManager)
        {
            this.player = player;
            this.dManager = dManager;
            dManager.SetHolder(this);
        }
        public override void DialogueLevel(string selection)
        {
            Debug.Log("Teleport");
        }
    }
}
