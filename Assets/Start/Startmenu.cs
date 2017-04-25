using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Startmenu : MonoBehaviour {

	public Button HiScoreButton { get; set; }
	public Button StartButton { get; set; }
	// Use this for initialization
	void Start () {
		HiScoreButton = GameObject.Find("Character 1").GetComponent<Button>();
		StartButton = GameObject.Find("Character 2").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
