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
        private Vector2 posMouse;
        public Vector2 posChar { get; set; }
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
        /// On left click, get the click's x world coordinate and move to it.
        /// </summary>
        public void LeftClick()
        {
            if (moveRight && !dMan.dialogueActive) 
            {
                CharacterMoveRight();
            }
            else if (moveLeft && !dMan.dialogueActive) 
            {
                CharacterMoveLeft();
            }

            if (Input.GetMouseButtonDown(0) && !dMan.dialogueActive) //Prevents moving while in dialogue
            {
                posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Register mouse position in World coordinates
                if (posMouse.x > posChar.x)
                {
                    moveRight = true;
                }
                else if (posMouse.x < posChar.x)
                {
                    moveLeft = true;
                }
            }
        }

        /// <summary>
        /// Moves character left until reaching the x world coordinate.
        /// </summary>
        private void CharacterMoveLeft()
        {
            character.transform.Translate(-player.speed * Time.deltaTime, 0f, 0f);
            if (moveLeft && posChar.x < posMouse.x)
            {
                moveLeft = false;
            }
        }

        /// <summary>
        /// Moves character right until reaching the x world coordinate.
        /// </summary>
        private void CharacterMoveRight()
        {
            character.transform.Translate(player.speed * Time.deltaTime, 0f, 0f);
            if (moveRight && posChar.x > posMouse.x)
            {
                moveRight = false;
            }
        }
    }
}
