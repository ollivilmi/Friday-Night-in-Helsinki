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
        private GameObject cutscene, animationBeer, animationTobacco;

        private void Start()
        {
            animationBeer = GameObject.Find("CutsceneBeer");
            animationTobacco = GameObject.Find("CutsceneTobacco");
            cutscene = GameObject.Find("Cutscene");

            animationBeer.SetActive(false);
            animationTobacco.SetActive(false);
            cutscene.SetActive(false);
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

        private IEnumerator CutsceneItem(GameObject item)
        {
            cutscene.SetActive(true);
            item.SetActive(true);
            yield return new WaitForSeconds(3f);
            item.SetActive(false);
            cutscene.SetActive(false);
        }
    }
}
