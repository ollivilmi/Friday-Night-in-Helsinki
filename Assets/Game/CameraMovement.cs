using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraMovement : MonoBehaviour
    {
 
		public bool moveRight { get; set; }
		public bool moveLeft { get; set; }
        private Vector2 pos;
        private Vector3 posCamera;

        private void Start()
        {
            moveLeft = false;
            moveRight = false;
            pos = this.gameObject.transform.position;
        }


        private void Update()
        {
            posCamera = Camera.main.transform.position;

            if (moveRight)
            {
                MoveCameraRight();   
            }
            else if (moveLeft)
            {
                MoveCameraLeft();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (pos.x > posCamera.x)
                {
                    moveRight = true;
                }
                else if (pos.x < posCamera.x)
                {
                    moveLeft = true;
                }
            }
        }

        /// <summary>
        /// Moves the camera right until reaching the x position of the trigger.
        /// </summary>
        private void MoveCameraRight()
        {
            if (posCamera.x < pos.x)
            {
                Camera.main.transform.Translate(20f * Time.deltaTime, 0f, 0f);
            }
            else if (posCamera.x > pos.x) moveRight = false;
        }

        /// <summary>
        /// Moves the camera left until reaching the x position of the trigger.
        /// </summary>
        private void MoveCameraLeft()
        {
            if (posCamera.x > pos.x)
            {
                Camera.main.transform.Translate(-20f * Time.deltaTime, 0f, 0f);
            }
            else if (posCamera.x < pos.x) moveLeft = false;
        }
    }
}
