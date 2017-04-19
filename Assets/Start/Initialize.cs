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

	public Button Char1 { get; set; }
	public Button Char2 { get; set; }
	public Button Char3 { get; set; }
	public Button CharCreation{ get; set;}
	public Button StartGame { get; set; }
	public Text BackStory { get; set;}
	public Image CharImage { get; set;}

	public GameObject ActivePlayer;
	//public Image playerImage;
	public Sprite playerSprite;
	public  string backStory;
	//public Player.Jarno jarno;

	public DataSaver dataSaver;

	public Player.Player jarno;
	public Player.Player make;
	public Player.Player teddy;


	// Use this for initialization
	void Start () {
		jarno = new Jarno();
		make = new Make();
		teddy = new Teddy();


		//print (dataSaver);

		ActivePlayer = GameObject.Find("ActivePlayer");

		// Find UI elements
		Char1 = GameObject.Find("Character 1").GetComponent<Button>();
		Char2 = GameObject.Find("Character 2").GetComponent<Button>();
		Char3 = GameObject.Find("Character 3").GetComponent<Button>();
		CharCreation = GameObject.Find("Character Creation").GetComponent<Button>();
		StartGame = GameObject.Find("Start Game").GetComponent<Button>();
		BackStory = GameObject.Find("Back Story").GetComponent<Text>();
		CharImage = GameObject.Find("Character Image").GetComponent<Image>();

		// set UI elements on/off
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

	public void GotoCharacterCreation(){

		// set UI elements on/off
		Char1.gameObject.SetActive(true);
		Char2.gameObject.SetActive(true);
		Char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		StartGame.gameObject.SetActive(false);
		BackStory.gameObject.SetActive(true);
		CharImage.gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {

	}

	public void ChooseJarno (){

		// set UI elements on/off
		Char1.gameObject.SetActive(true);
		Char2.gameObject.SetActive(true);
		Char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		StartGame.gameObject.SetActive(true);
		BackStory.gameObject.SetActive(true);
		CharImage.gameObject.SetActive(true);

		// insert Jarno's back story and picture (sprite) to corresponding elements
		DataSaver dataSaver = FindObjectOfType<DataSaver> ();

		backStory = jarno.GetBackStory();
		Text text = BackStory.GetComponentInChildren<Text>();
		text.text = backStory;

		playerSprite = jarno.GetPlayerSprite();
		Image playerImage = CharImage.GetComponentInChildren<Image> ();
		playerImage.sprite = playerSprite;

		dataSaver.character = "Jarno";
		print (dataSaver);

	}

	public void ChooseMake (){

		// set UI elements on/off
		Char1.gameObject.SetActive(true);
		Char2.gameObject.SetActive(true);
		Char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		StartGame.gameObject.SetActive(true);
		BackStory.gameObject.SetActive(true);
		CharImage.gameObject.SetActive(true);

		// insert Make's back story and picture (sprite) to corresponding elements

		DataSaver dataSaver = FindObjectOfType<DataSaver> ();

		backStory = make.GetBackStory();
		Text text = BackStory.GetComponentInChildren<Text>();
		text.text = backStory;

		playerSprite = make.GetPlayerSprite();
		Image playerImage = CharImage.GetComponentInChildren<Image> ();
		playerImage.sprite = playerSprite;

		dataSaver.character = "Make";



	}
	public void ChooseTeddy (){

		// set UI elements on/off
		Char1.gameObject.SetActive(true);
		Char2.gameObject.SetActive(true);
		Char3.gameObject.SetActive(true);
		CharCreation.gameObject.SetActive(false);
		StartGame.gameObject.SetActive(true);
		BackStory.gameObject.SetActive(true);
		CharImage.gameObject.SetActive(true);

		// insert Teddy's back story and picture (sprite) to corresponding elements
		DataSaver dataSaver = FindObjectOfType<DataSaver> ();

		backStory = teddy.GetBackStory();
		Text text = BackStory.GetComponentInChildren<Text>();
		text.text = backStory;

		playerSprite = teddy.GetPlayerSprite();
		Image playerImage = CharImage.GetComponentInChildren<Image> ();
		playerImage.sprite = playerSprite;

		dataSaver.character = "Teddy";

	}
}