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
        private GameEvents events;

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

            cardPlayerCurrent.sprite = cards[random.Next(0, 14)];
            cardPlayerPrevious.sprite = cards[random.Next(0, 14)];
            cardDealerCurrent.sprite = cards[random.Next(0, 14)];
            cardDealerPrevious.sprite = cards[random.Next(0, 14)];

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

        /// <summary>
        /// Adds a random card to the target's current card count.
        /// Swaps card sprites and moves current card sprite to previous card sprite.
        /// </summary>
        /// <param name="target">"Player" or "Dealer", otherwise does nothing.</param>
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

        /// <summary>
        /// Resets game state by setting all counters to 0 and
        /// dealerPlaying to false to hide dealer's true card count.
        /// </summary>
        private void resetGame()
        {
            playerCount = 0;
            dealerCount = 0;
            dealerAceCount = 0;
            playerAceCount = 0;
            dealerCard = 0;
            dealerPlaying = false;
        }

        /// <summary>
        /// Hides menu buttons, resets game and deals cards.
        /// Dealer's first card image or count is not shown.
        /// </summary>
        private void startNewGame()
        {
            events = player.events;
            events.ChangeTime(3);
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

        /// <summary>
        /// Sets playing to false, resets bet size in case that it was doubled, disabled play buttons
        /// and enables menu buttons. Opens result panel for 2 seconds.
        /// </summary>
        private void endGame()
        {
            bet = betSize[index];
            playing = false;
            controlButtons(false, true);
            StartCoroutine(openResultPanel());
        }

        /// <summary>
        /// Opens result panel for 1 second.
        /// </summary>
        /// <returns></returns>
        private IEnumerator openResultPanel()
        {   
            resultPanel.SetActive(true);
            resultPanelText.text = resultText;
            yield return new WaitForSeconds(1f);
            resultPanel.SetActive(false);
        }

        /// <summary>
        /// Can be used to toggle between playing buttons and menu buttons.
        /// </summary>
        /// <param name="playingButtons">Double, hit, stand</param>
        /// <param name="menuButtons">Quit, play, bet</param>
        private void controlButtons(bool playingButtons, bool menuButtons)
        {
            objectPlay.SetActive(menuButtons);
            objectQuit.SetActive(menuButtons);
            objectBet.SetActive(menuButtons);
            objectDouble.SetActive(playingButtons);
            objectHit.SetActive(playingButtons);
            objectStand.SetActive(playingButtons);
        }

        /// <summary>
        /// Checks target's card count to check winning
        /// and losing conditions.
        /// </summary>
        /// <param name="target">"Player" or "Dealer", otherwise does nothing.</param>
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

        /// <summary>
        /// Dealer uses dealCard("Dealer") until a winning or losing
        /// condition is met and then ends the game.
        /// </summary>
        private void dealerPlay()
        {
            dealerPlaying = true;
            while (playing)
            {
                if (dealerCount >= 17)
                {
                    if (playerCount > dealerCount)
                    {
                        player.useMoney(2 * bet);
                        resultText = "Player wins!";
                        endGame();

                    }
                    else if (playerCount < dealerCount)
                    {
                        resultText = "Dealer wins.";
                        endGame();
                    }
                    else if (dealerCount == playerCount)
                    {
                        player.useMoney(bet);
                        resultText = "Push, your bet has been returned.";
                        endGame();
                    }
                }
                else if (dealerCount < 17)
                {
                    dealCard("Dealer");
                }
                checkCount("Dealer");
            }
        }

        /// <summary>
        /// Information, card counters for player and dealer are updated,
        /// however while playing only updates the card which is face
        /// up for the dealer.
        /// </summary>
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
            infoBlackjack.text = "Money: "+player.money + " Bet: " + bet;
        }

        /// <summary>
        /// Starts the game with current bet sizing if player has enough money.
        /// </summary>
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

        /// <summary>
        /// Clicking quit button lets you move again and closes the Blackjack UI.
        /// </summary>
        private void quitClicked()
        {
            blackjack.SetActive(false);
            playerMovement.Stop = false;
            col.ShowInteraction();
        }

        /// <summary>
        /// Changes current bet size from a list of possible bet sizes.
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
        /// If player can afford it while playing,
        /// double the bet and draw one more card.
        /// </summary>
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

        /// <summary>
        /// Deal a card to the player and check for results.
        /// </summary>
        private void hitClicked()
        {
            if (playing)
            {
                dealCard("Player");
                checkCount("Player");
            }
        }

        /// <summary>
        /// Deal cards to the dealer until a result is reached.
        /// </summary>
        private void standClicked()
        {
                dealerPlay();
        }
    }
}
