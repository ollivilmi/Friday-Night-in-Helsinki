using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraMovement : MonoBehaviour
    {
        // Use this for initialization
        bool moveRight, moveLeft;
        Vector2 pos;
        Vector3 camerapos;

        void Start()
        {
            moveLeft = false;
            moveRight = false;
            pos = this.gameObject.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            camerapos = Camera.main.transform.position; //Check camera position every frame

            if (moveRight) //If camera has been set to move right
            {
                if (camerapos.x < pos.x) //Move right until you reach the trigger's x coordinate
                {
                    Camera.main.transform.Translate(9f * Time.deltaTime, 0f, 0f);
                }
                else if (camerapos.x > pos.x) moveRight = false;
            }
            else if (moveLeft) //If camera has been set to move left
            {
                if (camerapos.x > pos.x) //Move left until you reach the trigger's x coordinate
                {
                    Camera.main.transform.Translate(-9f * Time.deltaTime, 0f, 0f);
                }
                else if (camerapos.x < pos.x) moveLeft = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (pos.x > camerapos.x) //Set camera to move right if the camera is to the left of the trigger
                {
                    moveRight = true;
                }
                else if (pos.x < camerapos.x) //Set camera to move left if the camera is to the right of the trigger
                {
                    moveLeft = true;
                }
            }
        }
    }
}
