using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour {

	public bool change = false;
	private Initialize init;


	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void Quit (){
		SceneManager.SetActiveScene (SceneManager.GetSceneAt (1));
		foreach(GameObject g in SceneManager.GetSceneAt(1).GetRootGameObjects()){
			g.SetActive (true);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(2).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(3).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(4).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(5).GetRootGameObjects()){
			g.SetActive (false);
		}

		foreach (GameObject g in SceneManager.GetSceneAt(6).GetRootGameObjects()) {
			g.SetActive (false);
		}
		//init.GotoStart ();
	


	}

	public void Pause () {
		SceneManager.SetActiveScene (SceneManager.GetSceneAt (3));
		foreach(GameObject g in SceneManager.GetSceneAt(3).GetRootGameObjects()){
			g.SetActive (true);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(1).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(2).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(4).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(5).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach (GameObject g in SceneManager.GetSceneAt(6).GetRootGameObjects()) {
			g.SetActive (false);
		}
	}

	public void Change () {

		SceneManager.SetActiveScene (SceneManager.GetSceneAt (2));
		foreach(GameObject g in SceneManager.GetSceneAt(2).GetRootGameObjects()){
			g.SetActive (true);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(1).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(3).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(4).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach(GameObject g in SceneManager.GetSceneAt(5).GetRootGameObjects()){
			g.SetActive (false);
		}
		foreach (GameObject g in SceneManager.GetSceneAt(6).GetRootGameObjects()) {
			g.SetActive (false);
		}

	}
	void GotoCharacterCreation(){
	}
}
