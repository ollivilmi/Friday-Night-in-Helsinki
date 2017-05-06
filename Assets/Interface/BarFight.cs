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
    public class BarFight : MonoBehaviour
    {
        private List<Image> bfPlayer;
        private List<Image> bfNPC;
        private Sprite[] playerHPBar, npcHPBar;
        private GameObject bfOptionsPanel, bfInfo, barfight;
        private List<GameObject> bfDialogue;
        private List<Button> bfOptions;
        private List<string> insults;
        private Dictionary<Button, string> buttonToString;
        private int playerHP, npcHP;
        private System.Random random;
        public Player.Player player { get; set; }
        private Cutscene cutscene;
        private Text bfInfoText;
        private List<Text> bfInsults;
        private NPC.Collision target;
        private GameEvents events;
        private InterfaceManager iManager;

        private void Start()
        {
            random = new System.Random();
            playerHP = 0;
            npcHP = 0;
            cutscene = FindObjectOfType<Cutscene>();
            iManager = FindObjectOfType<InterfaceManager>();
            barfight = GameObject.Find("BarFight");

            bfInsults = new List<Text> { GameObject.Find("BFtextNPC").GetComponent<Text>(), GameObject.Find("BFtextPlayer").GetComponent<Text>() }; 
            bfInfo = GameObject.Find("BFinfo");
            bfInfoText = bfInfo.GetComponent<Text>();
            bfInfo.SetActive(false);

            bfPlayer = new List<Image>()
            {
                GameObject.Find("BFPlayer").GetComponent<Image>(),
                GameObject.Find("HPplayer").GetComponent<Image>(),
            };

            bfNPC = new List<Image>()
            {
                GameObject.Find("BFNPC").GetComponent<Image>(),
                GameObject.Find("HPnpc").GetComponent<Image>()
            };

            insults = new List<string>()
            {
                "Hey donkey, how is your mother? Do the zoo keepers remember to feed her?",
                "You stupidhead, how does it feel to be stupid?",
                "I had a lovely evening with your mother yesterday. Or was it a cow? Can't remember.",
                "I'm sorry your dad beat you instead of cancer.",
                "If you were a potato you'd be a stupid potato.",
                "You are so ugly that when you were born, the doctor slapped your mother.",
                "Ready to fail like your dad's condom?",
                "You look like something I drew with my left hand.",
            };

            playerHPBar = Resources.LoadAll<Sprite>("HPplayer");
            npcHPBar = Resources.LoadAll<Sprite>("HPnpc");

            bfPlayer[1].sprite = playerHPBar[0];
            bfNPC[1].sprite = npcHPBar[0];

            bfOptionsPanel = GameObject.Find("BFoptions");
            bfDialogue = new List<GameObject> { GameObject.Find("BFdialogueNPC"), GameObject.Find("BFdialoguePlayer") };
            bfDialogue[0].SetActive(false);
            bfDialogue[1].SetActive(false);

            bfOptions = new List<Button>()
            {
                GameObject.Find("BFAttack").GetComponent<Button>(),
                GameObject.Find("BFInsult").GetComponent<Button>(),
                GameObject.Find("BFDrink").GetComponent<Button>(),
            };

            buttonToString = new Dictionary<Button, string>()
            {
                {  bfOptions[0], "Attack" },
                {  bfOptions[1], "Insult" },
                {  bfOptions[2], "Drink" },
            };

            foreach (Button button in bfOptions)
            {
                button.onClick.AddListener(() => attackOptions(button));
            }
            barfight.SetActive(false);
        }
        /// <summary>
        /// Sets the sprite of the image that is used to portray the player.
        /// This is set in GameController as the player is created.
        /// </summary>
        /// <param name="playerSprite">Sprite that the Image uses.</param>
        public void SetPlayerSprite(Sprite playerSprite)
        {
            bfPlayer[0].sprite = playerSprite;
        }
        /// <summary>
        /// Sets the sprite of the image that is used to portray the NPC.
        /// This is set in the InterfaceManager after colliding with an NPC.
        /// </summary>
        /// <param name="npcSprite">Sprite that the Image uses.</param>
        public void SetNPCSprite (Sprite npcSprite)
        {
            bfNPC[0].sprite = npcSprite;
        }
        /// <summary>
        /// Target is the Collision script of the NPC.
        /// This is used to remove the NPC GameObject after beating it.
        /// </summary>
        /// <param name="target">NPC's Collision script.</param>
        public void SetTarget(NPC.Collision target)
        {
            this.target = target;
        }
        /// <summary>
        /// Resets HP counters to 0 and activates the UI overlay.
        /// </summary>
        public void newGame()
        {
            events = player.events;
            playerHP = 0;
            npcHP = 0;
            barfight.SetActive(true);
            bfOptionsPanel.SetActive(true);
            bfPlayer[1].sprite = playerHPBar[0];
            bfNPC[1].sprite = npcHPBar[0];
        }
        /// <summary>
        /// Shows result texts on the screen, used to display
        /// how much damage was dealt for 1 second.
        /// </summary>
        /// <param name="message">The message you want to show.</param>
        /// <returns></returns>
        private IEnumerator showInfo(string message)
        {
            bfInfo.SetActive(true);
            bfInfoText.text = message;
            yield return new WaitForSeconds(1f);
            bfInfo.SetActive(false);
        }
        /// <summary>
        /// Translates the button click into a command.
        /// </summary>
        /// <param name="button">Player options in BarFight.</param>
        private void attackOptions(Button button)
        {
            string selection = buttonToString[button];
            bfOptionsPanel.SetActive(false);
            switch (selection)
            {
                case "Attack":
                    StartCoroutine(useAttack());
                    break;
                case "Insult":
                    StartCoroutine(useInsult());
                    break;
                case "Drink":
                    useDrink();
                    break;
            }
        }
        /// <summary>
        /// Checks winning conditions. Randomizes damage, also if
        /// player is very drunk the player has a higher chance of missing.
        /// </summary>
        /// <param name="user">bfPlayer or bfNPC. The list contains the image of the player at [0] and hp bar at [1].</param>
        private void attackResult(List<Image> user)
        {
            int damage = random.Next(0, 3);
            if (user == bfPlayer)
            {
                if (player.drunkLevel > 60 && damage != 0)
                {
                    damage = random.Next(0, 3);
                }
                npcHP += damage;
                StartCoroutine(showInfo("Player deals " +damage +" damage!"));
                if (npcHP >= 5)
                {
                    events.ChangeTime(10);
                    events.addScore(20);
                    StartCoroutine(iManager.PopUp("You showed who's the boss."));
                    player.useMoney(random.Next(0, 30));
                    target.Remove();
                    target.StopInteracting();
                    barfight.SetActive(false);
                }
                else
                {
                    bfNPC[1].sprite = npcHPBar[npcHP];
                }
            }
            else if (user == bfNPC)
            {
                damage = random.Next(0, 3);
                playerHP += damage;
                StartCoroutine(showInfo("NPC deals " + damage + " damage!"));
                if (playerHP >= 5)
                {
                    target.StopInteracting();
                    StartCoroutine(cutscene.CutsceneBlackout());
                    player.useMoney(-(random.Next(0, 20)));
                    barfight.SetActive(false);
                }
                else
                {
                    bfPlayer[1].sprite = playerHPBar[playerHP];
                }
            }
        }
        /// <summary>
        /// Randomizes whether the NPC insults or attacks head on.
        /// </summary>
        private void npcAttack()
        {
            if (barfight.activeInHierarchy)
            {
                int randomize = random.Next(0, 2);
                switch (randomize)
                {
                    case 0:
                        StartCoroutine(insult(bfNPC));
                        break;
                    case 1:
                        StartCoroutine(attack(bfNPC));
                        break;
                }
            }
        }
        /// <summary>
        /// Starts attack animation, then waits 1,5 seconds for the NPC to act.
        /// </summary>
        /// <returns></returns>
        private IEnumerator useAttack()
        {
            StartCoroutine(attack(bfPlayer));
            yield return new WaitForSeconds(1.5f);
            npcAttack();
        }
        /// <summary>
        /// Plays an attack animation where the player moves 40 frames forward
        /// and backward.
        /// </summary>
        /// <param name="user">bfPlayer or bfNPC. The list contains the image of the player at [0] and hp bar at [1].</param>
        /// <returns></returns>
        private IEnumerator attack(List<Image> user)
        {
            for (int i = 0; i < 40; i++)
            {
                user[0].transform.Translate(2f, 0f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            attackResult(user);
            for (int i = 0; i < 40; i++)
            {
                user[0].transform.Translate(-2f, 0f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            if (user == bfNPC)
            {
                bfOptionsPanel.SetActive(true);
            }
        }
        /// <summary>
        /// Changes dialogue box location and starts the insult attack.
        /// Waits for 3 seconds and then lets the NPC randomize a response.
        /// </summary>
        /// <returns></returns>
        private IEnumerator useInsult()
        {
            StartCoroutine(insult(bfPlayer));
            yield return new WaitForSeconds(3f);
            npcAttack();
        }
        /// <summary>
        /// Randomizes an insult to the insult dialogue box from the insults list. Checks for damage.
        /// </summary>
        /// <param name="user">bfPlayer or bfNPC. The list contains the image of the player at [0] and hp bar at [1].</param>
        /// <returns></returns>
        private IEnumerator insult(List<Image> user)
        {
            if (user == bfPlayer)
            {
                bfDialogue[1].SetActive(true);
                bfInsults[1].text = insults[random.Next(0, insults.Count)];
            }
            else if (user == bfNPC)
            {
                bfDialogue[0].SetActive(true);
                bfInsults[0].text = insults[random.Next(0, insults.Count)];
            }       
            yield return new WaitForSeconds(3f);
            attackResult(user);
            if (user == bfPlayer)
            {
                bfDialogue[1].SetActive(false);
            }
            else if (user == bfNPC)
            {
                bfDialogue[0].SetActive(false);
                bfOptionsPanel.SetActive(true);
            }
        }
        /// <summary>
        /// Drink a beer if you have one. Gives you 2hp but increases drunkLevel.
        /// You have a higher chance of missing at over 60 drunkLevel and you black out 
        /// at 100.
        /// </summary>
        private void useDrink()
        {
            if (player.items[0].UseItem())
            {
                cutscene.StartCutsceneItem("Beer");
                playerHP -= 2;
                if (playerHP < 0)
                {
                    playerHP = 0;
                }
                bfPlayer[1].sprite = playerHPBar[playerHP];
            }
            if (player.drunkLevel == 100)
            {
                target.StopInteracting();
                barfight.SetActive(false);
            }
            bfOptionsPanel.SetActive(true);
        }
    }
}
