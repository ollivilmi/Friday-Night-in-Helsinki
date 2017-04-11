using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        GameObject dude;
        Button tp;
		Button tp2;
        Vector2 pos;


        void Start()
        {
            tp = GameObject.Find("Teleporter").GetComponent<Button>();
			tp2 = GameObject.Find("Teleporter2").GetComponent<Button>();
            dude = GameObject.Find("Player");
            tp.onClick.AddListener(() => Tele());
			pos = new Vector2(tp2.transform.position.x, -8f);
        }


        void Update()
        {

        }

        void Tele()
        {
            dude.transform.position = pos;
			Camera.main.transform.position = new Vector3(tp2.transform.position.x, 3f, -10f);
        }
    }
}