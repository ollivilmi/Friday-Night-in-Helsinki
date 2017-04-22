using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Game;
using System.Collections;

namespace Interface
{
    public class Inventory : MonoBehaviour
    {
        private Button buttonOpenInventory, inventoryBeer, inventoryTobacco, inventoryQuest, inventoryInfo;
        private GameObject panelInventory, panelInformation;
        private Text amountBeer, amountTobacco, amountQuestItem, info;
        public Movement playerMovement { get; set; }
        public Player.Player player { get; set; }
        private Cutscene cutscene;
        private string active;
        private Image imageQuestItem;
        private int index;

        private void Start()
        {
            buttonOpenInventory = GameObject.Find("Inventory Button").GetComponent<Button>();
            buttonOpenInventory.onClick.AddListener(() => openInventory());

            inventoryBeer = GameObject.Find("Beer").GetComponent<Button>();
            inventoryBeer.onClick.AddListener(() => infoPanel("Beer"));

            inventoryTobacco = GameObject.Find("Tobacco").GetComponent<Button>();
            inventoryTobacco.onClick.AddListener(() => infoPanel("Tobacco"));

            inventoryQuest = GameObject.Find("QuestItem").GetComponent<Button>();
            inventoryQuest.onClick.AddListener(() => infoPanel("Quest Item"));

            inventoryInfo = GameObject.Find("Item info").GetComponent<Button>();
            inventoryInfo.onClick.AddListener(() => closeInfo());

            imageQuestItem = GameObject.Find("QuestItem").GetComponent<Image>();

            amountBeer = GameObject.Find("Beer amount").GetComponent<Text>();
            amountTobacco = GameObject.Find("Tobacco amount").GetComponent<Text>();
            amountQuestItem = GameObject.Find("QuestItem amount").GetComponent<Text>();
            info = GameObject.Find("Info text").GetComponent<Text>();
            cutscene = FindObjectOfType<Cutscene>();

            panelInventory = GameObject.Find("Inventory panel");
            panelInformation = GameObject.Find("Info panel");

            panelInventory.SetActive(false);
            panelInformation.SetActive(false);
        }

        /// <summary>
        /// Opens or closes Inventory. Stops player movement.
        /// </summary>
        private void openInventory()
        {
            if (panelInventory.activeInHierarchy == false)
            {
                playerMovement.Stop = true;
                panelInventory.SetActive(true);
                updateInventory();
            }
            else
            {
                playerMovement.Stop = false;
                playerMovement.StopMovement();
                panelInformation.SetActive(false);
                index = 0;
                active = null;
                panelInventory.SetActive(false);
            }
        }

        /// <summary>
        /// Updates the amount of items and active Quest item image.
        /// </summary>
        private void updateInventory()
        {
            amountBeer.text = ""+player.items[0].amount;
            amountTobacco.text = "" + player.items[1].amount;
            amountQuestItem.text = ""+player.itemsQuest.Count;
            imageQuestItem.sprite = player.itemsQuest[index].image;
        }

        /// <summary>
        /// Closes item information panel.
        /// </summary>
        private void closeInfo()
        {
            panelInformation.SetActive(false);
            active = null;
        }

        /// <summary>
        /// Uses item or changes Quest item.
        /// </summary>
        /// <param name="item"></param>
        private void useItem(string item)
        {
            switch (item) {
                case "Beer":
                    if (player.items[0].UseItem())
                    {
                        print("Lol");
                        cutscene.StartCutsceneItem("Beer");
                    }
                    updateInventory();
                break;
                case "Tobacco":
                    if (player.items[1].UseItem())
                    {
                        print("Lol");
                        cutscene.StartCutsceneItem("Tobacco");
                    }
                    updateInventory();
                    break;
                case "Quest Item":
                    index++;
                    if (index >= player.itemsQuest.Count)
                    {
                        index = 0;
                    }
                    info.text = player.itemsQuest[index].description;
                    updateInventory();
                    break;
            }
        }

        /// <summary>
        /// Opens the item's description. If the item is already active, uses it.
        /// </summary>
        /// <param name="item"></param>
        private void infoPanel(string item)
        {
            panelInformation.SetActive(true);
            switch (item)
            {
                case "Beer":
                    if (active == "Beer")
                    {
                        useItem(item);
                        break;
                    }
                    info.text = "Good ol' warm pocket beer. Drinking one increases your drunk level by 10"
                        + " but also decreases your likability. \n\nClick the icon again to drink.";
                    active = item;
                    break;

                case "Tobacco":
                    if (active == "Tobacco")
                    {
                        useItem(item);
                        break;
                    }
                    info.text = "The package has a picture of a black lung. The text reads: 'Smoking causes cancer'."
                        + "\n\nClick the icon again to smoke.";
                    active = item;
                    break;

                case "Quest Item":
                    if (active == "Quest Item")
                    {
                        useItem(item);
                        break;
                    }
                    info.text = player.itemsQuest[index].description;
                    active = item;
                    break;
            }
        }
    }
}
