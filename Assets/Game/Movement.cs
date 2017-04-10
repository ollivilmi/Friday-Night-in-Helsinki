using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogue;
using Player;

namespace Game
{
    public class Movement
    {
        private bool moveRight, moveLeft;
        private DialogueManager dMan;
        public Vector2 mousePos, charPos;
        private Player.Player player;
        private GameObject character;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Uses player's speed</param>
        /// <param name="dMan">Doesn't move if dialogue is active</param>
        /// <param name="character">Character is used to get player sprite's position</param>
        public Movement(Player.Player player, DialogueManager dMan, GameObject character)
        {
            this.player = player;
            this.dMan = dMan;
            this.character = character;
            moveRight = false;
            moveLeft = false;
        }

        /// <summary>
        /// On left click, move to the click's x-coordinate. However, if you are busy (in dialogue)
        /// don't move. Uses player variable float speed.
        /// </summary>
        public void LeftClick()
        {
            if (moveRight && !dMan.dialogueActive) //Move right if clicked
            {                                                                 //Don't move if in dialogue
                character.transform.Translate(player.speed * Time.deltaTime, 0f, 0f);
                if (moveRight && charPos.x > mousePos.x) //Stop moving after reaching destination
                {
                    moveRight = false;
                }
            }
            else if (moveLeft && !dMan.dialogueActive) //Move left if clicked
            {                                                                     //Don't move if in dialogue
                character.transform.Translate(-player.speed * Time.deltaTime, 0f, 0f);
                if (moveLeft && charPos.x < mousePos.x) //Stop moving after reaching destination
                {
                    moveLeft = false;
                }
            }

            if (Input.GetMouseButtonDown(0) && !dMan.dialogueActive) //If player uses left click and is not in dialogue
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Register mouse position in World coordinates
                if (mousePos.x > charPos.x)
                {
                    moveRight = true;
                }
                else if (mousePos.x < charPos.x)
                {
                    moveLeft = true;
                }
            }
        }
    }
}
