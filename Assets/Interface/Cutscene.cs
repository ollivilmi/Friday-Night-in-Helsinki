using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Interface
{
    public class Cutscene : MonoBehaviour
    {
        private GameObject cutscene, animationBeer, animationTobacco, animationMetro, spritePlayer;
        private List<GameObject> cutsceneObjects;
        private Sprite alley, metro;

        private void Start()
        {          
            animationBeer = GameObject.Find("CutsceneBeer");
            animationTobacco = GameObject.Find("CutsceneTobacco");
            animationMetro = GameObject.Find("CutsceneMetro");
            cutscene = GameObject.Find("Cutscene");
            spritePlayer = GameObject.Find("CutscenePlayer");
            alley = Resources.Load<Sprite>("darkalley");
            metro = Resources.Load<Sprite>("metro");

            cutsceneObjects = new List<GameObject> { animationBeer, animationTobacco, animationMetro,
                spritePlayer, cutscene };

            foreach (GameObject element in cutsceneObjects)
            {
                element.SetActive(false);
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
            cutscene.GetComponent<Image>().sprite = metro;
            animationMetro.SetActive(true);
            cutscene.SetActive(true);
            yield return new WaitForSeconds(5f);
            animationMetro.SetActive(false);
            cutscene.SetActive(false);
        }

        private IEnumerator CutsceneItem(GameObject item)
        {
            cutscene.GetComponent<Image>().sprite = alley;
            cutscene.SetActive(true);
            item.SetActive(true);
            spritePlayer.SetActive(true);
            yield return new WaitForSeconds(3f);
            item.SetActive(false);
            spritePlayer.SetActive(false);
            cutscene.SetActive(false);
        }
    }
}
