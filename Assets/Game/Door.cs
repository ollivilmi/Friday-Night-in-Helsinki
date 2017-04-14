﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Door : MonoBehaviour
    {
        GameObject player;
        CameraFollow limits;

		void Start()
		{
			player = GameObject.Find("Player");
			limits = FindObjectOfType<CameraFollow> ();
		}

		public void Enter(NPC.Collision teleporter)
        {
			string name = teleporter.GetName ();
			switch (name)
			{
			case "DoorRWS1(Clone)":
                    EnterDoor(-60, -105, -68);
				break;
			case "DoorMainHall1(Clone)":
                    EnterDoor(20, 0, 140);
				break;
			case "DoorMainHall2(Clone)":
                    EnterDoor(-140, -182, -161);
				break;
			case "DoorBar1(Clone)":
                    EnterDoor(-80, -105, -68);
				break;
			}
        }

        private void EnterDoor(float xLocation, float xMin, float xMax)
        {
            player.transform.position = new Vector2(xLocation, -8f);
            Camera.main.transform.position = new Vector3(xLocation, 3f, -10f);
            limits.xMin = xMin;
            limits.xMax = xMax;
        }

    }
}