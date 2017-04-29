using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Player;
using Game;

namespace Interface
{
    public class GuitarNote : MonoBehaviour
    {
        RectTransform note;
        GuitarGod guitargod;

        private void Start()
        {
            note = GetComponent<RectTransform>();
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => Clicked());
            guitargod = FindObjectOfType<GuitarGod>();
            transform.SetParent(GameObject.Find("UI").transform, true);
            transform.localPosition = GetComponent<RectTransform>().position;
        }

        private void Update()
        {
            note.transform.Translate(new Vector3(0f, guitargod.GetSpeed() * Time.deltaTime, 0f));
            if (transform.localPosition.y < -300)
            {
                guitargod.NoteMissed();
                Destroy(gameObject);
            }
            if (!guitargod.playing)
            {
                Destroy(gameObject);
            }
        }

        private void Clicked()
        {
            if (transform.localPosition.y > -200 && transform.localPosition.y < -90)
            {
                guitargod.NoteClicked();
                Destroy(gameObject);
            }
            else
            {
                guitargod.NoteMissed();
                Destroy(gameObject);
            }
        }
    }
}