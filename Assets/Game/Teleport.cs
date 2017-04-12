using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
		GameObject player, rws1;     

		void Start()
		{
			player = GameObject.Find("Player");
			rws1 = (GameObject)Resources.Load ("DoorRWS1", typeof(GameObject));
		}

		public void Tele(NPC.Collision teleporter)
        {
			string name = teleporter.GetName ();
			Debug.Log (name);
			switch (name)
			{
			case "DoorRWS1(Clone)":
				player.transform.position = new Vector2 (-60f, -8f);
				Camera.main.transform.position = new Vector3 (-60f, 3f, -10f);
				break;
			}
        }

    }
}