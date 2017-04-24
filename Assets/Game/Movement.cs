using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogue;
using Player;
using Interface;

namespace Game
{
    public class Movement
    {
        public bool moveRight, moveLeft;
        public bool Stop { get; set; }
        private Vector2 posMouse;
        public Vector2 posChar { get; set; }
        private Player.Player player;
        private GameObject character;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Uses player's speed</param>
        /// <param name="iManager">Doesn't move if dialogue is active</param>
        /// <param name="character">Character is used to get player sprite's position</param>
        public Movement(Player.Player player, GameObject character)
        {
            this.player = player;
            this.character = character;
            moveRight = false;
            moveLeft = false;
            Stop = false;
        }

        /// <summary>
        /// On left click, get the click's x world coordinate and move to it.
        /// </summary>
        public void LeftClick()
        {
            if (moveRight && !Stop) 
            {
                CharacterMoveRight();
            }
            else if (moveLeft && !Stop) 
            {
                CharacterMoveLeft();
            }

            if (Input.GetMouseButtonDown(0) && !Stop)
            {
                GetClick();
            }
        }

        /// <summary>
        /// Moves character left until reaching the x world coordinate.
        /// </summary>
        private void CharacterMoveLeft()
        {
            character.transform.Translate(player.speed * Time.deltaTime, 0f, 0f);
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

        /// <summary>
        /// Get the x world coordinate from your mouse position and set character to move towards it.
        /// </summary>
        private void GetClick()
        {
            StopMovement();
            posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (posMouse.x > posChar.x)
            {
                character.transform.eulerAngles = new Vector2(0, 0);
                moveRight = true;
            }
            else if (posMouse.x < posChar.x)
            {
                character.transform.eulerAngles = new Vector2(0, 180);
                moveLeft = true;
            }
        }

        /// <summary>
        /// Stops current movement.
        /// </summary>
        public void StopMovement()
        {
            moveRight = false;
            moveLeft = false;
        }
    }
}
