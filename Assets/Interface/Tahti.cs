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
        private List<Sprite> coinSprites;
        private List<int> results;
        private List<bool> locksClicked;
        private List<int> betSize;
        private GameObject tahti;
        private Animator coinAnimation;
        private Image coinImage;
        private System.Random random;
        private bool playing;
        private int bet, index, lastWin;
        private Text info1, info2;
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
            coinImage = GameObject.Find("Coin").GetComponent<Image>();

            fruitSprites = new List<Sprite>
            {
                Resources.Load<Sprite>("star"),
                Resources.Load<Sprite>("strawberry"),
                Resources.Load<Sprite>("banana")
            };
            coinSprites = new List<Sprite>
            {
                Resources.Load<Sprite>("heads"),
                Resources.Load<Sprite>("tails")
            };

            fruitAnimations = new List<Animator>();
            for (int i = 0; i < 3; i++)
            {
                fruitAnimations.Add(fruitObjects[i].GetComponent<Animator>());
            }
            coinAnimation = GameObject.Find("Coin").GetComponent<Animator>();
            coinAnimation.enabled = false;

            buttonPlay = GameObject.Find("TahtiPlay").GetComponent<Button>();
            buttonPlay.onClick.AddListener(() => playClicked());

            buttonBet = GameObject.Find("TahtiBet").GetComponent<Button>();
            buttonBet.onClick.AddListener(() => betClicked());

            buttonQuit = GameObject.Find("TahtiQuit").GetComponent<Button>();
            buttonQuit.onClick.AddListener(() => quitClicked());

            buttonDouble = GameObject.Find("TahtiDouble").GetComponent<Button>();
            buttonDouble.onClick.AddListener(() => doubleClicked());

            info1 = GameObject.Find("BetInfo").GetComponent<Text>();
            info2 = GameObject.Find("LastWin").GetComponent<Text>();
            tahti = GameObject.Find("Tahti");
        }
        /// <summary>
        /// Clicking a lock while playing stops the animation, randomizes
        /// a result, changes sprite to result and saves it.
        /// Sets bool clicked to true so that it can't be clicked again.
        /// </summary>
        /// <param name="button">Which lock was pressed, uses dictionary to translate it into an integer.</param>
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
                info1.text = "Money: " + player.money + " Bet size: " + bet;
            } catch (NullReferenceException) { }

            // If playing and all locks have been clicked, checks the result
            if (locksClicked[0] && locksClicked[1] && locksClicked[2] && playing)
            {
                playing = false;
                checkResults();
            }
        }

        /// <summary>
        /// Checks whether the player's win conditions. Checks them in order
        /// from biggest to smallest and returns if the condition is met.
        /// </summary>
        private void checkResults()
        {
            if (results[0] == 2)
            {
                if (results[1] == 2 && results[2] == 2)
                {
                    lastWin = 2 * bet;
                    payPlayer();
                    return;
                }
            }

            if (results[0] == 1)
            {
                if (results[1] == 1 && results[2] == 1)
                {
                    lastWin = 3 * bet;
                    payPlayer();
                    return;
                }
            }

            if (results[0] == 0)
            {
                if (results[1] == 0 && results[2] == 0)
                {
                    lastWin = 4 * bet;
                    payPlayer();
                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (results[i] == 0)
                {
                    lastWin = bet;
                    payPlayer();
                    return;
                }
            }
            info2.text = "";
        }

        /// <summary>
        /// Clicking the play button starts a game with the active bet size, if the
        /// player can afford it. Starts fruit animations.
        /// </summary>
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

        /// <summary>
        /// Changes bet size to the next one on the betSize list.
        /// </summary>
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

        /// <summary>
        /// If last game resulted in a win, clicking double starts
        /// the coin animation which lasts 3 seconds and randomizes 
        /// 50/50 if you win or lose.
        /// </summary>
        private void doubleClicked()
        {
            if (!playing)
            {
                if (lastWin > 0)
                {
                    player.useMoney(-lastWin);
                    StartCoroutine(CoinAnimation());
                }
            }
        }

        /// <summary>
        /// Called when win conditions are met. The machine congratulates
        /// the player for the win and aks if the player wants to double.
        /// </summary>
        private void payPlayer()
        {
            player.useMoney(lastWin);
            info2.text = "You won " + lastWin + "!" + " Double or nothing?";
        }

        /// <summary>
        /// Exits the slot machine and enables player movement.
        /// </summary>
        private void quitClicked()
        {
            if (!playing)
            {
                tahti.SetActive(false);
                playerMovement.Stop = false;
                col.ShowInteraction();
            }
        }

        /// <summary>
        /// Animation that plays coin's swapping for 3 seconds,
        /// then randomizes the result. If the result is a win,
        /// the player may double again.
        /// </summary>
        /// <returns></returns>
        IEnumerator CoinAnimation()
        {
            playing = true;
            info2.text = "Heads wins, tails loses.";
            coinAnimation.enabled = true;
            yield return new WaitForSeconds(3f);
            coinAnimation.enabled = false;
            playing = false;
            int selection = random.Next(0, 2);
            if (selection == 0)
            {
                coinImage.sprite = coinSprites[0];
                lastWin *= 2;
                info2.text = "You doubled to " + lastWin + "!";
                player.useMoney(lastWin);
            }
            else if (selection == 1)
            {
                info2.text = "You lost.";
                coinImage.sprite = coinSprites[1];
                lastWin = 0;
            }
        }
    }
}
