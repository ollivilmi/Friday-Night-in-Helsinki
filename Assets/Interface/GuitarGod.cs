﻿using System;
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
        private int score, count, missclickCount, comboCount, failLimit;
        private float timer;
        private List<GameObject> guitarnotes;
        private System.Random random;
        private Text uiScore;
        private Button missClick;
        public bool playing { get; set; }
        public Movement playerMovement { get; set; }
        private GameObject guitargod;

        private void Start()
        {
            playing = false;
            uiScore = GameObject.Find("GuitarGodScore").GetComponent<Text>();
            random = new System.Random();
            missClick = GameObject.Find("Missclick").GetComponent<Button>();
            missClick.onClick.AddListener(() => MissClicked());

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
                uiScore.text = "Score: " + score + "\nMiss clicks: " + missclickCount + "\nCombo: " + comboCount;
                if (count < 3)
                {
                    Instantiate(guitarnotes[random.Next(0, 3)]);
                    count++;
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
            failLimit = 5;
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

        public float GetSpeed()
        {
            return -150f - (timer * 2);
        }

        private void MissClicked()
        {
            missclickCount++;
            score--;
            failLimit--;
            comboCount = 0;
        }

        public void NoteClicked()
        {
            count--;
            comboCount++;
            if (failLimit < 5)
            {
                failLimit++;
            }
            score += scoreMultiplier();
        }

        public void NoteMissed()
        {
            count--;
            score--;
            failLimit--;
            comboCount = 0;
        }

        private int scoreMultiplier()
        {
            if (comboCount < 4)
            {
                return 1;
            }
            else if (comboCount < 8)
            {
                return 2;
            }
            else if (comboCount < 12)
            {
                return 3;
            }
            else if (comboCount < 16 || comboCount > 16)
            {
                return 4;
            }
            return 0;
        }
    }
}