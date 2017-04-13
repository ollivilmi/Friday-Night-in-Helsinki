using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour
    {
        public float xMax { get; set; }
        public float xMin { get; set; }
        private Transform player;
        private Vector3 targetPos;

        private void Start()
        {
            player = GameObject.Find("Player").transform;
            xMax = 140;
            xMin = 0;
        }

        private void LateUpdate()
        {
            targetPos = new Vector3(Mathf.Clamp(player.position.x, xMin, xMax), transform.position.y, transform.position.z);

            if (targetPos.x > transform.position.x)
            {
                MoveCameraRight(11f);
            }
            else if (targetPos.x < transform.position.x)
            {
                MoveCameraLeft(-11f);
            }
        }

        /// <summary>
        /// Moves the camera right until reaching the x position
        /// </summary>
        private void MoveCameraRight(float speed)
        {
                Camera.main.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

        /// <summary>
        /// Moves the camera left until reaching the x position
        /// </summary>
        private void MoveCameraLeft(float speed)
        {
                Camera.main.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }
    }
}
