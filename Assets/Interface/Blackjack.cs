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
    public class Blackjack : MonoBehaviour
    {
        private GameObject blackjack, objectDouble, objectHit, objectStand, objectBet, objectPlay, objectQuit, resultPanel;
        private Button buttonQuit, buttonDouble, buttonHit, buttonStand, buttonBet, buttonPlay;
        private Text cardcountPlayer, cardcountDealer, infoBlackjack, resultPanelText;
        private Image cardPlayerCurrent, cardPlayerPrevious, cardDealerPrevious, cardDealerCurrent;
        private Sprite[] cards;
        private List<int> cardInts;
        private int playerAceCount, dealerAceCount, playerCount, dealerCount, dealerCard;
        private bool playing, dealerPlaying;
        private List<int> betSize;
        private int bet, index;
        public Player.Player player { get; set; }
        public Movement playerMovement { get; set; }
        private System.Random random;
        private string resultText;
        private CollisionBlackjack col;

        private void Start()
        {
            random = new System.Random();
            cardInts = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
            betSize = new List<int> { 5, 10, 20, 50, 100, 200 };
            bet = 5;
            index = 0;
            cards = Resources.LoadAll<Sprite>("cards");
            playing = false;

            col = FindObjectOfType<CollisionBlackjack>();

            objectQuit = GameObject.Find("BlackjackQuit");
            objectBet = GameObject.Find("BlackjackBet");
            objectDouble = GameObject.Find("BlackjackDouble");
            objectPlay = GameObject.Find("BlackjackPlay");
            objectHit = GameObject.Find("BlackjackHit");
            objectStand = GameObject.Find("BlackjackStand");
            resultPanel = GameObject.Find("BlackjackResult");
            blackjack = GameObject.Find("Blackjack");

            buttonQuit = objectQuit.GetComponent<Button>();
            buttonBet = objectBet.GetComponent<Button>();
            buttonDouble = objectDouble.GetComponent<Button>();
            buttonPlay = objectPlay.GetComponent<Button>();
            buttonHit = objectHit.GetComponent<Button>();
            buttonStand = objectStand.GetComponent<Button>();

            cardPlayerCurrent = GameObject.Find("CardPlayerCurrent").GetComponent<Image>();
            cardPlayerPrevious = GameObject.Find("CardPlayerPrevious").GetComponent<Image>();
            cardDealerCurrent = GameObject.Find("CardDealerCurrent").GetComponent<Image>();
            cardDealerPrevious = GameObject.Find("CardDealerPrevious").GetComponent<Image>();

            cardcountPlayer = GameObject.Find("CardCountPlayer").GetComponent<Text>();
            cardcountDealer = GameObject.Find("CardCountDealer").GetComponent<Text>();
            infoBlackjack = GameObject.Find("BlackjackInfo").GetComponent<Text>();
            resultPanelText = GameObject.Find("ResultPanelText").GetComponent<Text>();

            buttonPlay.onClick.AddListener(() => playClicked());
            buttonQuit.onClick.AddListener(() => quitClicked());
            buttonBet.onClick.AddListener(() => betClicked());
            buttonDouble.onClick.AddListener(() => doubleClicked());
            buttonHit.onClick.AddListener(() => hitClicked());
            buttonStand.onClick.AddListener(() => standClicked());
            controlButtons(false, true);
            resultPanel.SetActive(false);
        }

        private void dealCard(string target)
        {
            int card = cardInts[random.Next(0, 13)];
            switch (target)
            {
                case "Dealer":
                    cardDealerPrevious.sprite = cardPlayerCurrent.sprite;
                    if (card == 10)
                    {
                        cardDealerCurrent.sprite = cards[random.Next(9, 13)];
                    }
                    else
                    {
                        cardDealerCurrent.sprite = cards[(card - 1)];
                    }
                    if (card == 1)
                    {
                        card = 11;
                        dealerAceCount++;
                    }
                    dealerCard = card;
                    dealerCount += card;
                    break;
                case "Player":
                    cardPlayerPrevious.sprite = cardPlayerCurrent.sprite;
                    if (card == 10)
                    {
                        cardPlayerCurrent.sprite = cards[random.Next(9, 13)];
                    }
                    else
                    {
                        cardPlayerCurrent.sprite = cards[(card - 1)];
                    }
                    if (card == 1)
                    {
                        card = 11;
                        playerAceCount++;
                    }
                    playerCount += card;
                    break;
            }
        }

        private void resetGame()
        {
            playerCount = 0;
            dealerCount = 0;
            dealerAceCount = 0;
            playerAceCount = 0;
            dealerCard = 0;
            dealerPlaying = false;
        }

        private void startNewGame()
        {
            controlButtons(true, false);
            resetGame();
            player.useMoney(-bet);
            playing = true;
            dealCard("Dealer");
            dealCard("Dealer");
            checkCount("Dealer");
            cardDealerPrevious.sprite = cards[13];
            dealCard("Player");
            dealCard("Player");
            checkCount("Player");       
        }

        private void endGame()
        {
            bet = betSize[index];
            playing = false;
            controlButtons(false, true);
            StartCoroutine(openResultPanel());
        }

        private IEnumerator openResultPanel()
        {   
            resultPanel.SetActive(true);
            resultPanelText.text = resultText;
            yield return new WaitForSeconds(2f);
            resultPanel.SetActive(false);
        }

        private void controlButtons(bool playingButtons, bool menuButtons)
        {
            objectPlay.SetActive(menuButtons);
            objectQuit.SetActive(menuButtons);
            objectBet.SetActive(menuButtons);
            objectDouble.SetActive(playingButtons);
            objectHit.SetActive(playingButtons);
            objectStand.SetActive(playingButtons);
        }

        private void checkCount (string target)
        {
            switch (target)
            {
                case "Dealer":
                    if (dealerCount > 21)
                    {
                        if (dealerAceCount > 0)
                        {
                            dealerAceCount--;
                            dealerCount -= 10;
                        }
                        else
                        {
                            player.useMoney(2 * bet);
                            resultText = "Dealer busts, you win!";
                            endGame();
                        }
                    }
                    break;
                case "Player":
                    if (playerCount == 21)
                    {
                        player.useMoney(1.5 * bet);
                        resultText = "Blackjack, you win!";
                        endGame();
                        break;
                    }
                    if (playerCount > 21)
                    {
                        if (playerAceCount > 0)
                        {
                            playerAceCount--;
                            playerCount -= 10;
                        }
                        else
                        {
                            resultText = "Busted, you lose.";
                            endGame();
                        }
                    }
                    break;
            }
        }

        private void dealerPlay()
        {
            dealerPlaying = true;
            while (playing)
            {
                if (dealerCount < playerCount || dealerCount < 17)
                {
                    dealCard("Dealer");
                }
                else if (dealerCount == playerCount)
                {
                    player.useMoney(bet);
                    resultText = "Push, your bet has been returned.";
                    endGame();
                }
                else if (dealerCount > playerCount)
                {
                    resultText = "Dealer wins.";
                    endGame();
                }
                checkCount("Dealer");
            }
        }

        private void LateUpdate()
        {
            if (dealerPlaying)
            {
                cardcountDealer.text = "" + dealerCount;
            }
            else if (!dealerPlaying)
            {
                cardcountDealer.text = "" + dealerCard;
            }
            cardcountPlayer.text = "" + playerCount;
            infoBlackjack.text = "Player money: "+player.money + " Current bet: " + bet;
        }

        private void playClicked()
        {
            if (!playing)
            {
                if (player.money >= bet)
                {
                    startNewGame();
                }
            }
        }

        private void quitClicked()
        {
            blackjack.SetActive(false);
            playerMovement.Stop = false;
            col.ShowInteraction();
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

        private void doubleClicked()
        {
            if (playing)
            {
                if (player.money >= bet)
                {
                    player.useMoney(-bet);
                    bet *= 2;
                    dealCard("Player");
                    checkCount("Player");
                    if (playing)
                    {
                        dealerPlay();
                    }
                }
            }
        }

        private void hitClicked()
        {
            if (playing)
            {
                dealCard("Player");
                checkCount("Player");
            }
        }

        private void standClicked()
        {
            dealerPlay();
        }
    }
}
