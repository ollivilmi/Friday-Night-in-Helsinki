using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
		GameObject player, rws1, mh1, mh2, bar1;
		CameraMovement camera;

		void Start()
		{
			player = GameObject.Find("Player");
			camera = FindObjectOfType<CameraMovement> ();
			rws1 = (GameObject)Resources.Load ("DoorRWS1", typeof(GameObject));
			mh1 = (GameObject)Resources.Load ("DoorMainHall1", typeof(GameObject));
			mh2 = (GameObject)Resources.Load ("DoorMainHall2", typeof(GameObject));
			bar1 = (GameObject)Resources.Load ("DoorMainHall3", typeof(GameObject));
		}

		public void Tele(NPC.Collision teleporter)
        {
			camera.moveRight = false;
			camera.moveLeft = false;
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