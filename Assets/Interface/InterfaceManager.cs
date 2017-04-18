﻿using System.Collections;
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
        public Button buttonInteraction { get; set; }
        public Button[] answerButtons { get; set; }
        private DialogueHolder dHolder;
        private GameObject background;
        private Image imageNPC, imagePlayer;
        private Dictionary<Button, string> selectedAnswer = new Dictionary<Button, string>();
        private NPC.Collision target;
        public Game.Movement playerMovement { get; set; }

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

            buttonInteraction = GameObject.Find("Interaction").GetComponent<Button>();
            buttonInteraction.onClick.AddListener(() => Interaction());

            background = GameObject.Find("Dialogue Background");
            imagePlayer = GameObject.Find("Player image").GetComponent<Image>();
            imageNPC = GameObject.Find("NPC image").GetComponent<Image>();
            imagePlayer.sprite = GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite;

            answerButtons = new Button[3] { dBoxAnswer1, dBoxAnswer2, dBoxAnswer3 };
            //For loop iteration

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
        }

        /// <summary>
        /// Changes NPC image in the dialogue system.
        /// </summary>
        /// <param name="imageNPC"></param>
        public void SetNPCImage(Sprite imageNPC)
        {
            this.imageNPC.sprite = imageNPC;
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
            dBoxNPC.gameObject.SetActive(false);
            dBoxAnswer1.gameObject.SetActive(false);
            dBoxAnswer2.gameObject.SetActive(false);
            dBoxAnswer3.gameObject.SetActive(false);
            buttonInteraction.gameObject.SetActive(false);
            selectedAnswer.Clear();
        }
        /// <summary>
        /// Clicking the button calls the Interaction()
        /// function of the collider script and stops player movement.
        /// </summary>
        private void Interaction()
        {
            target.Interaction();
            buttonInteraction.gameObject.SetActive(false);
        }
    }
}