using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	public bool change = false;
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}


	public void ChangeScene () {

		SceneManager.SetActiveScene (SceneManager.GetSceneAt (2));
		foreach(GameObject g in SceneManager.GetSceneAt(2).GetRootGameObjects()){
			g.SetActive (true);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(1).GetRootGameObjects()){
			g.SetActive (false);
		}

	}
	void GotoCharacterCreation(){
	}

	public void GotoScene(string Scenename){
		
		if (Input.GetMouseButtonUp(0)) { Application.LoadLevel (Scenename); }
	}
}
