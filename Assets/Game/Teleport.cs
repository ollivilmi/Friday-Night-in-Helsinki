using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        GameObject player;
		CameraMovement cameraMovement;

		void Start()
		{
			player = GameObject.Find("Player");
			cameraMovement = FindObjectOfType<CameraMovement> ();
		}

		public void Tele(NPC.Collision teleporter)
        {
			cameraMovement.moveRight = false;
			cameraMovement.moveLeft = false;
			string name = teleporter.GetName ();
			switch (name)
			{
			case "DoorRWS1(Clone)":
				player.transform.position = new Vector2 (-60f, -8f);
				Camera.main.transform.position = new Vector3 (-60f, 3f, -10f);
				break;
			case "DoorMainHall1(Clone)":
				player.transform.position = new Vector2 (20f, -8f);
				Camera.main.transform.position = new Vector3 (20f, 3f, -10f);
				break;
			case "DoorMainHall2(Clone)":
				player.transform.position = new Vector2 (-140f, -8f);
				Camera.main.transform.position = new Vector3 (-140f, 3f, -10f);
				break;
			case "DoorBar1(Clone)":
				player.transform.position = new Vector2 (-80f, -8f);
				Camera.main.transform.position = new Vector3 (-80f, 3f, -10f);
				break;
			}
        }

    }
}