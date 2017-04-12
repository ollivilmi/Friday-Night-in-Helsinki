using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        GameObject player;
        Button from;
		Button to;

		void SetTP(string door1, string door2)
		{
			from = GameObject.Find(door1).GetComponent<Button>();
			to = GameObject.Find(door2).GetComponent<Button>();
			player = GameObject.Find("Player");
			from.onClick.AddListener(() => Tele());
		}

        void Start()
        {
			SetTP("Teleporter","Teleporter2");
        }

		void Update()
		{
		}

        void Tele()
        {
			player.transform.position = new Vector2(to.transform.position.x, -8f);
			Camera.main.transform.position = new Vector3(to.transform.position.x, 3f, -10f);
        }

    }
}