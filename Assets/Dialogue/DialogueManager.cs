using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {

        public bool dialogueActive;
        public Button dNPC, dAnswer1, dAnswer2, dAnswer3;
        public Button[] answerButtons;
        DialogueHolder dHolder;
        Dictionary<Button, string> selection = new Dictionary<Button, string>();

        void Start()
        {
            dNPC = GameObject.Find("DialogueBox").GetComponent<Button>();
            dNPC.onClick.AddListener(() => Click(dNPC));

            dAnswer1 = GameObject.Find("Answer1").GetComponent<Button>();
            dAnswer1.onClick.AddListener(() => Click(dAnswer1));

            dAnswer2 = GameObject.Find("Answer2").GetComponent<Button>();
            dAnswer2.onClick.AddListener(() => Click(dAnswer2));

            dAnswer3 = GameObject.Find("Answer3").GetComponent<Button>();
            dAnswer3.onClick.AddListener(() => Click(dAnswer3));

            answerButtons = new Button[3] { dAnswer1, dAnswer2, dAnswer3 };

            CloseDialogue();
        }

        public void SetHolder(DialogueHolder dHolder)
        {
            this.dHolder = dHolder;
        }

        /// <summary>
        /// Set button active, change button's text and set a dictionary for +/n/-
        /// </summary>
        /// <param name="dialogue">String that contains the answer option</param>
        /// <param name="box">Enables button and gives access to button text</param>
        /// <param name="answer">Contains if the string is +/n/-</param>
        public void ShowBox(string dialogue, Button box, string answer)
        {
            box.gameObject.SetActive(true);
            Text text = box.GetComponentInChildren<Text>();
            text.text = dialogue;
            selection.Add(box, answer);
        }
        /// <summary>
        /// When you click a button, sets DialogueHolder's selection:string to
        /// the corresponding answer "Positive", "Neutral" or "Negative"
        /// </summary>
        /// <param name="b">Which button is pressed</param>
        public void Click(Button b) //When you click a button
        {
            dHolder.level++;
            dHolder.selection = selection[b];
            selection.Clear();
            dHolder.DialogueLevel();
        }
        /// <summary>
        /// Closes buttons, cleans up
        /// </summary>
        public void CloseDialogue()
        {
            dialogueActive = false;
            dNPC.gameObject.SetActive(false);
            dAnswer1.gameObject.SetActive(false);
            dAnswer2.gameObject.SetActive(false);
            dAnswer3.gameObject.SetActive(false);
            selection.Clear();
        }
    }
}