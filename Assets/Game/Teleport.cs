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
        Vector2 pos;


        void Start()
        {
            tp = GameObject.Find("Teleporter").GetComponent<Button>();
            dude = GameObject.Find("Player");
            tp.onClick.AddListener(() => Tele());
            pos = new Vector2(70f, 0f);
        }


        void Update()
        {

        }

        void Tele()
        {
            dude.transform.position = pos;
            Camera.main.transform.position = new Vector3(70f, 3f, -10f);
        }
    }
}