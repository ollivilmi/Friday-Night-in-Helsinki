using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Game;

namespace Interface
{
    public class Inventory : MonoBehaviour
    {
        private Button buttonOpenInventory, inventoryBeer, inventoryTobacco, inventoryQuest;
        private GameObject panelInventory;
        private Text amountBeer, amountTobacco, amountQuestItem;
        public Movement playerMovement { get; set; }
        public Player.Player player { get; set; }

        private void Start()
        {
            buttonOpenInventory = GameObject.Find("Inventory Button").GetComponent<Button>();
            buttonOpenInventory.onClick.AddListener(() => openInventory());

            inventoryBeer = GameObject.Find("Beer").GetComponent<Button>();
            inventoryBeer.onClick.AddListener(() => openInventory());

            inventoryTobacco = GameObject.Find("Tobacco").GetComponent<Button>();
            inventoryTobacco.onClick.AddListener(() => openInventory());

            inventoryQuest = GameObject.Find("QuestItem").GetComponent<Button>();
            inventoryQuest.onClick.AddListener(() => openInventory());

            amountBeer = GameObject.Find("Beer amount").GetComponent<Text>();
            amountTobacco = GameObject.Find("Tobacco amount").GetComponent<Text>();
            amountQuestItem = GameObject.Find("QuestItem amount").GetComponent<Text>();

            panelInventory = GameObject.Find("Inventory panel");
            panelInventory.SetActive(false);
        }

        private void openInventory()
        {
            if (panelInventory.activeInHierarchy == false)
            {
                playerMovement.StopMovement();
                panelInventory.SetActive(true);
                updateInventory();
            }
            else
            {
                playerMovement.StopMovement();
                panelInventory.SetActive(false);
            }
        }

        private void updateInventory()
        {
            amountBeer.text = ""+player.items[0].amount;
            amountTobacco.text = "" + player.items[1].amount;
            //amountQuestItem.text = "" + player.items[2].amount;
        }
    }
}
