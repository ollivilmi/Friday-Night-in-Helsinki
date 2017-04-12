using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initialize : MonoBehaviour {

	public Button Char1 { get; set; }
	public Button Char2 { get; set; }
	public Button Char3 { get; set; }
	public Button CharCreation{ get; set;}
	public Button StartGame { get; set; }
	public Text BackStory { get; set;}
	public Image CharImage{ get; set;}


	// Use this for initialization
	void Start () {
		//SceneManager.LoadScene(1, LoadSceneMode.Single);

		Char1 = GameObject.Find("Character 1").GetComponent<Button>();
		Char2 = GameObject.Find("Character 2").GetComponent<Button>();
		Char3 = GameObject.Find("Character 3").GetComponent<Button>();
		CharCreation = GameObject.Find("Character Creation").GetComponent<Button>();
		StartGame = GameObject.Find("Start Game").GetComponent<Button>();
		BackStory = GameObject.Find("Back Story").GetComponent<Text>();
		CharImage = GameObject.Find("Character Image").GetComponent<Image>();

		Char1.gameObject.SetActive(false);
		Char2.gameObject.SetActive(false);
		Char3.gameObject.SetActive(false);
		CharCreation.gameObject.SetActive(true);
		StartGame.gameObject.SetActive(false);
		BackStory.gameObject.SetActive(false);
		CharImage.gameObject.SetActive(false);


		foreach(GameObject g in SceneManager.GetSceneAt(2).GetRootGameObjects()){
			g.SetActive (false);
		}





		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
