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
        private RectTransform note;
        private GuitarGod guitargod;
        private bool start;

        private void Start()
        {
            start = false;
            note = GetComponent<RectTransform>();
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => Clicked());
            guitargod = FindObjectOfType<GuitarGod>();
            transform.SetParent(GameObject.Find("UI").transform, true);
            transform.localPosition = GetComponent<RectTransform>().position;
            StartCoroutine(spawnDelay(UnityEngine.Random.Range(0.1f,0.3f)));
        }
        /// <summary>
        /// Delay which is used to make sure that the prefab has been instantiated properly.
        /// </summary>
        /// <param name="delay">Delay before moving.</param>
        /// <returns></returns>
        private IEnumerator spawnDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            start = true;
        }

        private void Update()
        {
            if (start)
            {
                note.transform.Translate(new Vector3(0f, guitargod.GetSpeed() * Time.deltaTime, 0f));
                if (transform.localPosition.y < -300)
                {
                    guitargod.NoteMissed();
                    Destroy(gameObject);
                }
            }
            if (!guitargod.playing)
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Destroys the note and adds to score.
        /// </summary>
        private void Clicked()
        {
                guitargod.NoteClicked();
                Destroy(gameObject);
        }
    }
}