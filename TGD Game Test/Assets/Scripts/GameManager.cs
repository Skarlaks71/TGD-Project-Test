using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public AudioSource musSource;
	public float score;
	public bool optionsPainelisActive = false;
	public bool cursorIsActive = true;

	private Scene _scene;
	

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if(instance != null) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	void Update() {
		_scene = SceneManager.GetActiveScene();
		if(_scene.name != "MenuScene" && cursorIsActive == true && optionsPainelisActive == false){
			Cursor.visible = false;
			cursorIsActive = false;
		}else if(_scene.name == "MenuScene"){
			optionsPainelisActive = false;
			Cursor.visible = true;
			cursorIsActive = true;
		}
	}

	public void AddScore(){
		score += 100f;
	}


}
