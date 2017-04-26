using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Interface
{
    public class BarFight : MonoBehaviour
    {
        private List<Image> bfPlayer;
        private List<Image> bfNPC;
        private Sprite[] playerHPBar, npcHPBar;
        private GameObject bfOptionsPanel, bfDialogue, bfInfo, barfight;
        private List<Button> bfOptions;
        private List<String> insults;
        private Dictionary<Button, string> buttonToString;
        private int playerHP, npcHP;
        private System.Random random;
        public Player.Player player { get; set; }
        private Cutscene cutscene;
        private Text bfInsult, bfInfoText;
        private NPC.Collision target;

        private void Start()
        {
            random = new System.Random();
            playerHP = 0;
            npcHP = 0;
            cutscene = FindObjectOfType<Cutscene>();
            barfight = GameObject.Find("BarFight");

            bfInsult = GameObject.Find("BFtext").GetComponent<Text>();
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
                "I’m sorry your dad beat you instead of cancer.",
                "If you were a potato you’d be a stupid potato.",
                "You are so ugly that when you were born, the doctor slapped your mother.",
                "Ready to fail like your dad’s condom?",
                "You look like something I drew with my left hand.",
            };

            playerHPBar = Resources.LoadAll<Sprite>("HPplayer");
            npcHPBar = Resources.LoadAll<Sprite>("HPnpc");

            bfPlayer[1].sprite = playerHPBar[0];
            bfNPC[1].sprite = npcHPBar[0];

            bfOptionsPanel = GameObject.Find("BFoptions");
            bfDialogue = GameObject.Find("BFdialogue");
            bfDialogue.SetActive(false);

            bfOptions = new List<Button>();

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

        public void SetPlayerSprite(Sprite playerSprite)
        {
            bfPlayer[0].sprite = playerSprite;
        }

        public void SetNPCSprite (Sprite npcSprite)
        {
            bfNPC[0].sprite = npcSprite;
        }

        public void SetTarget(NPC.Collision target)
        {
            this.target = target;
        }

        public void newGame()
        {
            playerHP = 0;
            npcHP = 0;
            barfight.SetActive(true);
            bfPlayer[1].sprite = playerHPBar[0];
            bfNPC[1].sprite = npcHPBar[0];
        }

        private IEnumerator showInfo(string message)
        {
            bfInfo.SetActive(true);
            bfInfoText.text = message;
            yield return new WaitForSeconds(1f);
            bfInfo.SetActive(false);
        }

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

        private void attackResult(List<Image> user)
        {
            int damage = random.Next(0, 2);
            if (user == bfPlayer)
            {
                if (player.drunkLevel > 60 && damage == 1)
                {
                    damage = random.Next(0, 2);
                }
                npcHP += damage;
                StartCoroutine(showInfo("Player deals " +damage +" damage!"));
                if (npcHP >= 5)
                {
                    StartCoroutine(showInfo("Player wins!"));
                    print(target);
                    barfight.SetActive(false);
                }
                else
                {
                    bfNPC[1].sprite = npcHPBar[npcHP];
                }
            }
            else if (user == bfNPC)
            {
                damage = random.Next(0, 2);
                playerHP += damage;
                StartCoroutine(showInfo("NPC deals " + damage + " damage!"));
                if (playerHP >= 5)
                {
                    StartCoroutine(showInfo("NPC wins!"));
                    barfight.SetActive(false);
                }
                else
                {
                    bfPlayer[1].sprite = playerHPBar[playerHP];
                }
            }
        }

        private void npcAttack()
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

        private IEnumerator useAttack()
        {
            StartCoroutine(attack(bfPlayer));
            yield return new WaitForSeconds(1.5f);
            npcAttack();
        }

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

        private IEnumerator useInsult()
        {
            bfDialogue.transform.position = new Vector3(210f, bfDialogue.transform.position.y, bfDialogue.transform.position.z);
            StartCoroutine(insult(bfPlayer));
            yield return new WaitForSeconds(3f);
            bfDialogue.transform.position = new Vector3(300f, bfDialogue.transform.position.y, bfDialogue.transform.position.z);
            npcAttack();
        }

        private IEnumerator insult(List<Image> user)
        {
            bfDialogue.SetActive(true);
            bfInsult.text = insults[random.Next(0, insults.Count)];
            yield return new WaitForSeconds(3f);
            attackResult(user);
            bfDialogue.SetActive(false);
            if (user == bfNPC)
            {
                bfOptionsPanel.SetActive(true);
            }
        }

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
            bfOptionsPanel.SetActive(true);
        }
    }
}
