using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Player;
using Game;
using NPC;

namespace Interface
{
    public class GuitarGod : MonoBehaviour
    {
        private int score, count, multiplier, missclickCount, comboCount, failLimit, previousNote, randomNote;
        private float timer;
        private List<GameObject> guitarnotes;
        private System.Random random;
        private Text uiScore, uiMultiplier;
        private Button missClick;
        private Sprite[] rockmeters;
        public bool playing { get; set; }
        public Movement playerMovement { get; set; }
        private GameObject guitargod;
        private Image rockmeterImage;

        private void Start()
        {
            playing = false;
            uiScore = GameObject.Find("GuitarGodScore").GetComponent<Text>();
            uiMultiplier = GameObject.Find("GGMultiplier").GetComponent<Text>();
            random = new System.Random();
            missClick = GameObject.Find("Missclick").GetComponent<Button>();
            missClick.onClick.AddListener(() => MissClicked());
            rockmeters = Resources.LoadAll<Sprite>("rockmeter");
            rockmeterImage = GameObject.Find("GGRockMeter").GetComponent<Image>();

            guitarnotes = new List<GameObject>()
            {
                (GameObject)Resources.Load("Note1", typeof(GameObject)),
                (GameObject)Resources.Load("Note2", typeof(GameObject)),
                (GameObject)Resources.Load("Note3", typeof(GameObject)),
            };
            guitargod = GameObject.Find("GuitarGod");
            StartCoroutine(gameTimer());
        }

        private void Update()
        {
            if (playing)
            {
                scoreMultiplier();
                uiScore.text = ""+score;
                uiMultiplier.text = "x" + multiplier;
                if (count < 3)
                {
                    randomNote = random.Next(0, 3);
                    if (randomNote != previousNote)
                    {
                        Instantiate(guitarnotes[randomNote]);
                        previousNote = randomNote;
                        count++;
                    }       
                }
                if (failLimit < 0)
                {
                    playing = false;
                    guitargod.SetActive(false);
                    playerMovement.Stop = false;
                }
            }
        }

        public void NewGame()
        {
            guitargod.SetActive(true);
            score = 0;
            count = 3;
            comboCount = 0;
            timer = 0;
            failLimit = 3;
            healthMeter(0);
            playing = true;
            StartCoroutine(SpawnDelay());
            StartCoroutine(gameTimer());
        }

        private IEnumerator gameTimer()
        {
            while (playing)
            {
                yield return new WaitForSeconds(1f);
                timer++;
            }
            yield return new WaitForSeconds(0.01f);
        }

        private IEnumerator SpawnDelay()
        {
            if (playing)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(guitarnotes[random.Next(0, 3)]);
                    yield return new WaitForSeconds(1);
                }
            }
        }

        private void healthMeter(int amount)
        {
            failLimit += amount;
            if (failLimit < 7 && failLimit >= 0)
            {
                rockmeterImage.sprite = rockmeters[failLimit];
            }
        }

        public float GetSpeed()
        {
            return -150f - (timer * 2);
        }

        private void MissClicked()
        {
            missclickCount++;
            healthMeter(-1);
            comboCount = 0;
        }

        public void NoteClicked()
        {
            count--;
            comboCount++;
            if (failLimit < 6)
            {
                healthMeter(1);
            }
            score += 1 * multiplier;
        }

        public void NoteMissed()
        {
            count--;
            healthMeter(-1);
            comboCount = 0;
        }

        private void scoreMultiplier()
        {
            if (comboCount < 4)
            {
                multiplier = 1;
                return;
            }
            else if (comboCount < 8)
            {
                multiplier = 2;
                return;
            }
            else if (comboCount < 12)
            {
                multiplier = 3;
                return;
            }
            else if (comboCount < 16 || comboCount > 16)
            {
                multiplier = 4;
                return;
            }
        }
    }
}