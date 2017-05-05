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
        private GameObject cutscene, animationBeer, animationTobacco, animationGuitar, animationMetro, textObject, animationElevatorDown, animationElevatorUp;
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
            animationGuitar = GameObject.Find("CutsceneGuitar");
            animationMetro = GameObject.Find("CutsceneMetro");
            cutscene = GameObject.Find("Cutscene");
            player = GameObject.Find("CutscenePlayer");
            textObject = GameObject.Find("CutsceneText");
            cutsceneText = textObject.GetComponent<Text>();
            door = FindObjectOfType<Door>();
			animationElevatorDown = GameObject.Find ("CutsceneElevatorDown");
			animationElevatorUp = GameObject.Find ("CutsceneElevatorUp");

            background = new List<Sprite>()
            {
                Resources.Load<Sprite>("darkalley"),
                Resources.Load<Sprite>("metro"),
                Resources.Load<Sprite>("blackout"),
				Resources.Load<Sprite>("hissikuilu"),
            };

            cutsceneObjects = new List<GameObject> { animationBeer, animationTobacco, animationMetro, animationGuitar,
				textObject, player, cutscene, animationElevatorDown, animationElevatorUp };

            foreach (GameObject element in cutsceneObjects)
            {
                element.SetActive(false);
            }
        }
        /// <summary>
        /// Sets the Player Image object's sprite to correspond the character you are playing.
        /// </summary>
        /// <param name="character">Jarno, Make or Teddy</param>
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
        /// <summary>
        /// Starts a Cutscene which involves the player with some animated item. Can be used from
        /// normal C# classes.
        /// </summary>
        /// <param name="item">Which item to start a cutscene for.</param>
        public void StartCutsceneItem(string item)
        {
            switch (item)
            {
                case "Beer":
                    StartCoroutine(CutsceneItem(animationBeer, 3f));
                    break;
                case "Tobacco":
                    StartCoroutine(CutsceneItem(animationTobacco, 3f));
                    break;
                case "Guitar":
                    StartCoroutine(CutsceneItem(animationGuitar, 9f));
                    break;
            }
        }
        /// <summary>
        /// Starts BlackOut cutscene, can be accessed from normal C# classes.
        /// </summary>
        public void BlackOut()
        {
            StartCoroutine(CutsceneBlackout());
        }
        /// <summary>
        /// Starts Metro Cutscene by changing the background and enabling the Metro object.
        /// </summary>
        /// <returns></returns>
        public IEnumerator CutsceneMetro()
        {
            cutscene.GetComponent<Image>().sprite = background[1];
            animationMetro.SetActive(true);
            cutscene.SetActive(true);
            yield return new WaitForSeconds(5f);
            animationMetro.SetActive(false);
            cutscene.SetActive(false);
        }
        /// <summary>
        /// Starts Elevator Cutscene by changing the background and enabling the Elevator object.
        /// </summary>
        /// <returns></returns>
        public IEnumerator CutsceneElevatorDown()
		{
			cutscene.GetComponent<Image>().sprite = background[3];
			animationElevatorDown.SetActive(true);
			cutscene.SetActive(true);
			yield return new WaitForSeconds(5f);
			animationElevatorDown.SetActive(false);
			cutscene.SetActive(false);
		}
        /// <summary>
        /// Starts Elevator Cutscene by changing the background and enabling the Elevator object.
        /// </summary>
        /// <returns></returns>
        public IEnumerator CutsceneElevatorUp()
		{
			cutscene.GetComponent<Image>().sprite = background[3];
			animationElevatorUp.SetActive(true);
			cutscene.SetActive(true);
			yield return new WaitForSeconds(5f);
			animationElevatorUp.SetActive(false);
			cutscene.SetActive(false);
		}
        /// <summary>
        /// Starts item cutscene with the specified item and time. The item is indefinitely animated.
        /// </summary>
        /// <param name="item">Item object, which is a child of the Cutscene panel</param>
        /// <param name="time">How long to display the cutscene</param>
        /// <returns></returns>
        private IEnumerator CutsceneItem(GameObject item, float time)
        {
            cutscene.GetComponent<Image>().sprite = background[0];
            cutscene.SetActive(true);
            item.SetActive(true);
            player.SetActive(true);
            yield return new WaitForSeconds(time);
            item.SetActive(false);
            player.SetActive(false);
            cutscene.SetActive(false);
        }
        /// <summary>
        /// Changes time by 60 minutes, randomizes a new location for the player
        /// opens up the panel for 8 seconds, telling you that you lost your memory.
        /// </summary>
        /// <returns></returns>
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
