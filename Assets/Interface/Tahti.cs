using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
using NPC;

namespace Interface
{
    public class Tahti : MonoBehaviour
    {
        private List<Button> buttonsLock;
        private Button buttonPlay, buttonDouble, buttonBet, buttonQuit;
        private List<GameObject> fruitObjects;
        private List<Animator> fruitAnimations;
        private Dictionary<Button, int> fruitLocks;
        private List<Image> fruitImages;
        private List<Sprite> fruitSprites;
        private List<int> results;
        private List<bool> locksClicked;
        private List<int> betSize;
        private GameObject tahti;
        private System.Random random;
        private bool playing;
        private int bet, index, lastWin;
        private Text infoBet;
        public Player.Player player { get; set; }
        public Movement playerMovement { get; set; }
        private CollisionTahti col;

        private void Start()
        {
            random = new System.Random();
            results = new List<int>() { 0, 0, 0 };
            locksClicked = new List<bool> { false, false, false };
            betSize = new List<int> { 1, 2, 3, 5, 10 };
            bet = 1;
            index = 0;
            lastWin = 0;

            col = FindObjectOfType<CollisionTahti>();

            buttonsLock = new List<Button>()
            {
                GameObject.Find("Lock1").GetComponent<Button>(),
                GameObject.Find("Lock2").GetComponent<Button>(),
                GameObject.Find("Lock3").GetComponent<Button>(),
            };

            fruitLocks = new Dictionary<Button, int>();

            for (int i = 0; i < 3; i++)
            {
                fruitLocks.Add(buttonsLock[i], i);
            }

            foreach (Button button in buttonsLock)
            {
                button.onClick.AddListener(() => lockClicked(button));
            }

            fruitObjects = new List<GameObject>()
            {
                GameObject.Find("Fruit1"),
                GameObject.Find("Fruit2"),
                GameObject.Find("Fruit3")
            };

            fruitImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                fruitImages.Add(fruitObjects[i].GetComponent<Image>());
            }

            fruitSprites = new List<Sprite>
            {
                Resources.Load<Sprite>("star"),
                Resources.Load<Sprite>("strawberry"),
                Resources.Load<Sprite>("banana")
            };

            fruitAnimations = new List<Animator>();
            for (int i = 0; i < 3; i++)
            {
                fruitAnimations.Add(fruitObjects[i].GetComponent<Animator>());
            }

            buttonPlay = GameObject.Find("TahtiPlay").GetComponent<Button>();
            buttonPlay.onClick.AddListener(() => playClicked());

            buttonBet = GameObject.Find("TahtiBet").GetComponent<Button>();
            buttonBet.onClick.AddListener(() => betClicked());

            buttonQuit = GameObject.Find("TahtiQuit").GetComponent<Button>();
            buttonQuit.onClick.AddListener(() => quitClicked());

            infoBet = GameObject.Find("BetInfo").GetComponent<Text>();
            tahti = GameObject.Find("Tahti");
        }

        private void lockClicked(Button button)
        {
            if (playing)
            {
                int selection = fruitLocks[button];
                locksClicked[selection] = true;
                fruitAnimations[selection].SetBool("Playing", false);
                fruitAnimations[selection].enabled = false;
                int result = random.Next(0, 3);
                results[selection] = result;
                fruitImages[selection].sprite = fruitSprites[result];
            }
        }

        private void LateUpdate()
        {
            try
            {
                infoBet.text = "Money: " + player.money + " Bet: " + bet + " Last win: " + lastWin;
            } catch (NullReferenceException)
            { }

            if (locksClicked[0] && locksClicked[1] && locksClicked[2] && playing)
            {
                playing = false;
                checkResults();
            }
        }

        private void checkResults()
        {
            if (results[0] == 2)
            {
                if (results[1] == 2 && results[2] == 2)
                {
                    lastWin = 2 * bet;
                    player.useMoney(lastWin);
                    return;
                }
            }

            if (results[0] == 1)
            {
                if (results[1] == 1 && results[2] == 1)
                {
                    lastWin = 3 * bet;
                    player.useMoney(lastWin);
                    return;
                }
            }

            if (results[0] == 0)
            {
                if (results[1] == 0 && results[2] == 0)
                {
                    lastWin = 4 * bet;
                    player.useMoney(lastWin);
                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (results[i] == 0)
                {
                    player.useMoney(bet);
                    break;
                }
            }
        }

        private void playClicked()
        {
            if (!playing)
            {
                if (player.money >= bet)
                {
                    player.useMoney(-bet);
                    playing = true;
                    for (int i = 0; i < 3; i++)
                    {
                        locksClicked[i] = false;
                    }
                    foreach (Animator fruit in fruitAnimations)
                    {

                        fruit.enabled = true;
                        fruit.SetBool("Playing", true);
                    }
                }
            }
        }

        private void betClicked()
        {
            if (!playing)
            {
                index++;
                if (index >= betSize.Count)
                {
                    index = 0;
                }
                bet = betSize[index];
            }
        }

        private void quitClicked()
        {
            if (!playing)
            {
                tahti.SetActive(false);
                playerMovement.Stop = false;
                col.ShowInteraction();
            }
        }
    }
}
