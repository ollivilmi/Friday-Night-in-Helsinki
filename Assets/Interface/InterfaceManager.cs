using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Dialogue;

namespace Interface
{
    public class InterfaceManager : MonoBehaviour
    {
        public bool dialogueActive { get; set; }
        public Button dBoxNPC { get; set; }
        public Button dBoxAnswer1 { get; set; }
        public Button dBoxAnswer2 { get; set; }
        public Button dBoxAnswer3 { get; set; }
        public Button dBoxAnswerStory { get; set; }
        public Button buttonInteraction { get; set; }
        public List<Button> answerButtons { get; set; }
        private Button buttonBeer, buttonTobacco, buttonQuit;
        private DialogueHolder dHolder;
        private GameObject background, panelShop, panelPopUp;
        private Image imageNPC;
        public Image imagePlayer { get; set; }
        private Dictionary<Button, string> selectedAnswer = new Dictionary<Button, string>();
        private List<GameObject> dialogueElements;
        private BarFight barfight;
        public NPC.Collision target { get; set; }
        public Game.Movement playerMovement { get; set; }
        private Text textPopUp;

        private void Start()
        {
            barfight = FindObjectOfType<BarFight>();
            panelPopUp = GameObject.Find("Information PopUp");
            textPopUp = GameObject.Find("PopUpText").GetComponent<Text>();
            panelPopUp.SetActive(false);

            dialogueElements = new List<GameObject>();
            dBoxNPC = GameObject.Find("DialogueBox").GetComponent<Button>();
            dBoxAnswer1 = GameObject.Find("Answer1").GetComponent<Button>();
            dBoxAnswer2 = GameObject.Find("Answer2").GetComponent<Button>();
            dBoxAnswer3 = GameObject.Find("Answer3").GetComponent<Button>();
            dBoxAnswerStory = GameObject.Find("AnswerStory").GetComponent<Button>();

            answerButtons = new List<Button> { dBoxAnswer1, dBoxAnswer2, dBoxAnswer3, dBoxAnswerStory, dBoxNPC };
            foreach (Button button in answerButtons)
            {
                button.onClick.AddListener(() => Click(button));
                dialogueElements.Add(button.gameObject);
            }

            buttonInteraction = GameObject.Find("Interaction").GetComponent<Button>();
            buttonInteraction.onClick.AddListener(() => Interaction());
            dialogueElements.Add(buttonInteraction.gameObject);

            buttonBeer = GameObject.Find("BeerBuy").GetComponent<Button>();
            buttonBeer.onClick.AddListener(() => BuyItem("Beer"));
            buttonTobacco = GameObject.Find("TobaccoBuy").GetComponent<Button>();
            buttonTobacco.onClick.AddListener(() => BuyItem("Tobacco"));
            buttonQuit = GameObject.Find("QuitDialogue").GetComponent<Button>();
            buttonQuit.onClick.AddListener(() => SetDialogueActive(false));

            panelShop = GameObject.Find("Shop panel");
            dialogueElements.Add(panelShop);

            background = GameObject.Find("Dialogue Background");
            imagePlayer = GameObject.Find("Player image").GetComponent<Image>();
            imageNPC = GameObject.Find("NPC image").GetComponent<Image>();

            dialogueActive = false;
            background.SetActive(false);
            CloseDialogue();
        }

        /// <summary>
        /// After colliding with an npc, use the manager with the the dialogue it holds.
        /// </summary>
        /// <param name="dHolder">The NPC's dialogue.</param>
        public void SetHolder(DialogueHolder dHolder)
        {
            this.dHolder = dHolder;
        }

        /// <summary>
        /// Sets collision target so that Interaction() can be used.
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(NPC.Collision target)
        {
            this.target = target;
            barfight.SetTarget(target);
        }

        /// <summary>
        /// Changes NPC image in the dialogue system.
        /// </summary>
        /// <param name="imageNPC"></param>
        public void SetNPCImage(Sprite imageNPC)
        {
            this.imageNPC.sprite = imageNPC;
            barfight.SetNPCSprite(imageNPC);
        }

        /// <summary>
        /// Enables/disables dialogue UI and player movement.
        /// </summary>
        /// <param name="set"></param>
        public void SetDialogueActive(bool set)
        {
            dialogueActive = set;
            playerMovement.Stop = set;
            playerMovement.StopMovement();
            background.SetActive(set);
        }

        /// <summary>
        /// Set button active, change it's text and what clicking it does.
        /// </summary>
        /// <param name="dialogue">String that contains the text that will be put on the button.</param>
        /// <param name="box">Enables button and changes it's text.</param>
        /// <param name="answer">Contains a string which is returned to DialogueHolder.</param>
        public void ShowBox(string dialogue, Button box, string answer)
        {
            box.gameObject.SetActive(true);
            Text text = box.GetComponentInChildren<Text>();
            text.text = dialogue;
            selectedAnswer.Add(box, answer); 
        }
        /// <summary>
        /// Changes button's dialogue without changing what clicking it does
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="box"></param>
        public void ShowBox(string dialogue, Button box)
        {
            box.gameObject.SetActive(true);
            Text text = box.GetComponentInChildren<Text>();
            text.text = dialogue;
        }

        /// <summary>
        /// Returns which button was clicked to the DialogueHolder.
        /// </summary>
        /// <param name="answer">Which button is pressed</param>
        private void Click(Button answer)
        {
            string selection = selectedAnswer[answer];
            CloseDialogue();
            dHolder.DialogueLevel(selection);
        }
        /// <summary>
        /// Closes buttons, cleans up dictionary.
        /// </summary>
        public void CloseDialogue()
        {
            foreach (GameObject element in dialogueElements)
            {
                element.SetActive(false);
            }
            selectedAnswer.Clear();
        }
        /// <summary>
        /// Clicking the button calls the Interaction()
        /// function of the collider script and stops player movement.
        /// </summary>
        private void Interaction()
        {
            playerMovement.StopMovement();
            target.Interaction();
            buttonInteraction.gameObject.SetActive(false);
        }
        
        public void OpenShop()
        {
            if (panelShop.activeInHierarchy == false)
            {
                panelShop.SetActive(true);
            }
            else
            {
                panelShop.SetActive(false);
            }
        }

        private void BuyItem(string item)
        {
            dHolder.BuyItem(item);
        }

        public void OpenPopUp(string message)
        {
            StartCoroutine(PopUp(message));
        }

        public IEnumerator PopUp(string message)
        {
            panelPopUp.SetActive(true);
            textPopUp.text = message;
            yield return new WaitForSeconds(3f);
            panelPopUp.SetActive(false);
        }
    }
}