using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using Game;

namespace NPC
{
    public class NPCType : MonoBehaviour
    {
        public Player.Player player { get; set; }

        public NPCStory GetType(Collision npc, GameEvents events)
        {
            string name = npc.GetName();
            switch (name)
            {
                case "NPCPetri(Clone)":
                    return new NPCStory(player, "Petri", events);
                case "NPCMatti(Clone)":
                    return new NPCStory(player, "Matti", events);
                case "NPCTommi(Clone)":
                    return new NPCStory(player, "Tommi", events);
                case "NPCHeikki(Clone)":
                    return new NPCStory(player, "Heikki", events);
                case "NPCAlexander(Clone)":
                    return new NPCStory(player, "Alexander", events);
                case "NPCJ-P(Clone)":
                    return new NPCStory(player, "JP", events);
                default:
                return new NPCStory(player, "Default", events);
            }
        }
    }
}
 