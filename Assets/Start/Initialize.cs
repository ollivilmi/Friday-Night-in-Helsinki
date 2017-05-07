using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player;

using System;

using System.Linq;
using System.Text;
using Game;
using Dialogue;


public class Initialize : MonoBehaviour {

    private Button char1, char2, char3, startSelection, startGame;
    private GameObject CharCreation;
    private Text backStory;
    private Image charImage;

    private Sprite playerSprite;
    private string charBackStory;
    private SwitchScene switchScene;
	private DataSaver dataSaver;

	public Player.Player jarno;
	public Player.Player make;
	public Player.Player teddy;


	// Use this for initialization
	void Start () {
		jarno = new Jarno();
		make = new Make();
		teddy = new Teddy();

        switchScene = FindObjectOfType<SwitchScene>();
        dataSaver = FindObjectOfType<DataSaver>();

        // Find UI elements
        char1 = GameObject.Find("Character 1").GetComponent<Button>();
        char1.onClick.AddListener(() => ChooseJarno());
        char2 = GameObject.Find("Character 2").GetComponent<Button>();
        char2.onClick.AddListener(() => ChooseMake());
        char3 = GameObject.Find("Character 3").GetComponent<Button>();
        char3.onClick.AddListener(() => ChooseTeddy());
        startSelection = GameObject.Find("Startbutton").GetComponent<Button>();
        startSelection.onClick.AddListener(() => GotoCharacterCreation());
        startGame = GameObject.Find("Start Game").GetComponent<Button>();
        startGame.onClick.AddListener(() => switchScene.Change());

        CharCreation = GameObject.Find("Character Creation");
		startGame = GameObject.Find("Start Game").GetComponent<Button>();
		backStory = GameObject.Find("Back Story").GetComponent<Text>();
		charImage = GameObject.Find("Character Image").GetComponent<Image>();

		// set UI elements on/off
		char1.gameObject.SetActive(false);
		char2.gameObject.SetActive(false);
		char3.gameObject.SetActive(false);
		startGame.gameObject.SetActive(false);
		backStory.gameObject.SetActive(false);
		charImage.gameObject.SetActive(false);

		// set other scenes inactive
		foreach(GameObject g in SceneManager.GetSceneAt(2).GetRootGameObjects()){
			g.SetActive (false);
		}

		foreach (GameObject g in SceneManager.GetSceneAt(3).GetRootGameObjects()) {
			g.SetActive (false);
		}
		foreach (GameObject g in SceneManager.GetSceneAt(4).GetRootGameObjects()) {
			g.SetActive (false);
		}
		foreach (GameObject g in SceneManager.GetSceneAt(5).GetRootGameObjects()) {
			g.SetActive (false);
		}
		foreach (GameObject g in SceneManager.GetSceneAt(6).GetRootGameObjects()) {
			g.SetActive (false);
		}
	}

	public void GotoCharacterCreation(){

		// set UI elements on/off
		char1.gameObject.SetActive(true);
		char2.gameObject.SetActive(true);
		char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		startGame.gameObject.SetActive(false);
		backStory.gameObject.SetActive(true);
		charImage.gameObject.SetActive(true);
	}

	public void GotoStart(){

		char1 = GameObject.Find("Character 1").GetComponent<Button>();
		char2 = GameObject.Find("Character 2").GetComponent<Button>();
		char3 = GameObject.Find("Character 3").GetComponent<Button>();
        CharCreation = GameObject.Find("Character Creation");
		startGame = GameObject.Find("Start Game").GetComponent<Button>();
		backStory = GameObject.Find("Back Story").GetComponent<Text>();
		charImage = GameObject.Find("Character Image").GetComponent<Image>();

		// set UI elements on/off
		char1.gameObject.SetActive(false);
		char2.gameObject.SetActive(false);
		char3.gameObject.SetActive(false);
		CharCreation.gameObject.SetActive(true);
		startGame.gameObject.SetActive(false);
		backStory.gameObject.SetActive(false);
		charImage.gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update () {

	}

	public void ChooseJarno (){

		// set UI elements on/off
		char1.gameObject.SetActive(true);
		char2.gameObject.SetActive(true);
		char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		startGame.gameObject.SetActive(true);
		backStory.gameObject.SetActive(true);
		charImage.gameObject.SetActive(true);

		// insert Jarno's back story and picture (sprite) to corresponding elements
		charBackStory = jarno.GetBackStory();
		Text text = backStory.GetComponentInChildren<Text>();
		text.text = charBackStory;

		playerSprite = jarno.GetPlayerSprite();
		Image playerImage = charImage.GetComponentInChildren<Image> ();
		playerImage.sprite = playerSprite;

		dataSaver.character = "Jarno";

	}

	public void ChooseMake (){

		// set UI elements on/off
		char1.gameObject.SetActive(true);
		char2.gameObject.SetActive(true);
		char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		startGame.gameObject.SetActive(true);
		backStory.gameObject.SetActive(true);
		charImage.gameObject.SetActive(true);

		// insert Make's back story and picture (sprite) to corresponding elements
		charBackStory = make.GetBackStory();
		Text text = backStory.GetComponentInChildren<Text>();
		text.text = charBackStory;

		playerSprite = make.GetPlayerSprite();
		Image playerImage = charImage.GetComponentInChildren<Image> ();
		playerImage.sprite = playerSprite;

		dataSaver.character = "Make";



	}
	public void ChooseTeddy (){

		// set UI elements on/off
		char1.gameObject.SetActive(true);
		char2.gameObject.SetActive(true);
		char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		startGame.gameObject.SetActive(true);
		backStory.gameObject.SetActive(true);
		charImage.gameObject.SetActive(true);

		// insert Teddy's back story and picture (sprite) to corresponding elements
		charBackStory = teddy.GetBackStory();
		Text text = backStory.GetComponentInChildren<Text>();
		text.text = charBackStory;

		playerSprite = teddy.GetPlayerSprite();
		Image playerImage = charImage.GetComponentInChildren<Image> ();
		playerImage.sprite = playerSprite;

		dataSaver.character = "Teddy";

	}
}