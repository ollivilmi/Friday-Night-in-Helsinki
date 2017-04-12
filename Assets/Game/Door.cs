using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Door : MonoBehaviour
    {
        GameObject player;
		CameraMovement cameraMovement;

		void Start()
		{
			player = GameObject.Find("Player");
			cameraMovement = FindObjectOfType<CameraMovement> ();
		}

		public void Enter(NPC.Collision teleporter)
        {
			cameraMovement.moveRight = false;
			cameraMovement.moveLeft = false;
			string name = teleporter.GetName ();
			switch (name)
			{
			case "DoorRWS1(Clone)":
                    EnterDoor(-60);
				break;
			case "DoorMainHall1(Clone)":
                    EnterDoor(20);
				break;
			case "DoorMainHall2(Clone)":
                    EnterDoor(-140);
				break;
			case "DoorBar1(Clone)":
                    EnterDoor(-80);
				break;
			}
        }

        private void EnterDoor(float locationX)
        {
            player.transform.position = new Vector2(locationX, -8f);
            Camera.main.transform.position = new Vector3(locationX, 3f, -10f);
        }

    }
}