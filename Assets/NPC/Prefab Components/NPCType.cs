using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;

namespace NPC
{
    public class NPCType : MonoBehaviour
    {
        public Player.Player player { get; set; }

        public NPCStory GetType(Collision npc)
        {
            string name = npc.GetName();
            switch (name)
            {
                case "NPCPetri(Clone)":
                    return new NPCStory(player, "Petri");
                case "NPCMatti(Clone)":
                    return new NPCStory(player, "Matti");
            default:
                return new NPCStory(player, "Default");
            }
        }
    }
}
 