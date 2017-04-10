using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {

        public bool dialogueActive { get; set; }
        public Button dBoxNPC, dBoxAnswer1, dBoxAnswer2, dBoxAnswer3;
        public Button[] answerButtons;
        private DialogueHolder dHolder;
        private Dictionary<Button, string> selectedAnswer = new Dictionary<Button, string>();

        private void Start()
        {
            dBoxNPC = GameObject.Find("DialogueBox").GetComponent<Button>();
            dBoxNPC.onClick.AddListener(() => Click(dBoxNPC));

            dBoxAnswer1 = GameObject.Find("Answer1").GetComponent<Button>();
            dBoxAnswer1.onClick.AddListener(() => Click(dBoxAnswer1));

            dBoxAnswer2 = GameObject.Find("Answer2").GetComponent<Button>();
            dBoxAnswer2.onClick.AddListener(() => Click(dBoxAnswer2));

            dBoxAnswer3 = GameObject.Find("Answer3").GetComponent<Button>();
            dBoxAnswer3.onClick.AddListener(() => Click(dBoxAnswer3));

            answerButtons = new Button[3] { dBoxAnswer1, dBoxAnswer2, dBoxAnswer3 }; //The buttons are also accessible via this array if you need to iterate
                                                                                    // in a for loop for example
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
        /// Returns which button was clicked to the DialogueHolder.
        /// </summary>
        /// <param name="answer">Which button is pressed</param>
        public void Click(Button answer)
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
            dBoxNPC.gameObject.SetActive(false);
            dBoxAnswer1.gameObject.SetActive(false);
            dBoxAnswer2.gameObject.SetActive(false);
            dBoxAnswer3.gameObject.SetActive(false);
            selectedAnswer.Clear();
        }
    }
}