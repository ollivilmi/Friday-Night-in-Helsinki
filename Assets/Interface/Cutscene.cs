using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;

namespace Interface
{
    public class Cutscene : MonoBehaviour
    {
        private GameObject cutscene, animationBeer, animationTobacco, animationMetro, textObject;
        public GameObject player { get; set; }
        private List<GameObject> cutsceneObjects;
        private List<Sprite> background;
        private Text cutsceneText;
        public GameEvents events { get; set; }
        private Door door;

        private void Start()
        {
            animationBeer = GameObject.Find("CutsceneBeer");
            animationTobacco = GameObject.Find("CutsceneTobacco");
            animationMetro = GameObject.Find("CutsceneMetro");
            cutscene = GameObject.Find("Cutscene");
            player = GameObject.Find("CutscenePlayer");
            textObject = GameObject.Find("CutsceneText");
            cutsceneText = textObject.GetComponent<Text>();
            door = FindObjectOfType<Door>();

            background = new List<Sprite>()
            {
                Resources.Load<Sprite>("darkalley"),
                Resources.Load<Sprite>("metro"),
                Resources.Load<Sprite>("blackout"),
            };

            cutsceneObjects = new List<GameObject> { animationBeer, animationTobacco, animationMetro,
            textObject, player, cutscene };

            foreach (GameObject element in cutsceneObjects)
            {
                element.SetActive(false);
            }
        }

        public void SetPlayer(Sprite character)
        {
            switch (character.name)
            {
                case "Jarno":
                    player.GetComponent<Image>().sprite = character;
                    break;
                case "Make":
                    player.GetComponent<Image>().sprite = character;
                    break;
                case "Teddy":
                    player.GetComponent<Image>().sprite = character;
                    player.GetComponent<RectTransform>().localPosition = new Vector3(-61f, -47f);
                    break;
            }
        }

        public void StartCutsceneItem(string item)
        {
            switch (item)
            {
                case "Beer":
                    StartCoroutine(CutsceneItem(animationBeer));
                    break;
                case "Tobacco":
                    StartCoroutine(CutsceneItem(animationTobacco));
                    break;
            }
        }

        public IEnumerator CutsceneMetro()
        {
            cutscene.GetComponent<Image>().sprite = background[1];
            animationMetro.SetActive(true);
            cutscene.SetActive(true);
            yield return new WaitForSeconds(5f);
            animationMetro.SetActive(false);
            cutscene.SetActive(false);
        }

        private IEnumerator CutsceneItem(GameObject item)
        {
            cutscene.GetComponent<Image>().sprite = background[0];
            cutscene.SetActive(true);
            item.SetActive(true);
            player.SetActive(true);
            yield return new WaitForSeconds(3f);
            item.SetActive(false);
            player.SetActive(false);
            cutscene.SetActive(false);
        }

        public IEnumerator CutsceneBlackout()
        {
            events.ChangeTime(60);
            cutscene.GetComponent<Image>().sprite = background[2];
            cutscene.SetActive(true);
            textObject.SetActive(true);
            cutsceneText.text = "You don't remember what happens in the following hour.";
            yield return new WaitForSeconds(8f);          
            door.RandomLocation();
            textObject.SetActive(false);
            cutscene.SetActive(false);
        }
    }
}
