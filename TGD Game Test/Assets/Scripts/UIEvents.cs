using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour {
	
	[SerializeField]
	private GameObject _painelScore;

	[SerializeField]
	private Toggle _uToggle;
	private bool _isPaused;
	
	private void Awake(){
		if(_painelScore == null){
			_painelScore = new GameObject();
		}
	}
	private void Start() {
		Debug.Log(GameManager.instance.musSource.mute);
		if(GameManager.instance.musSource.mute == true){
			_uToggle.isOn = false;
		}
	}
	private void Update(){
		ShowScore();
		
	}
	public void Exit(){
		Debug.Log("Saiu");
		Application.Quit();
	}
	public void Mute(){
		if (!_uToggle.isOn) {
			GameManager.instance.musSource.mute = true;
		} else {
			GameManager.instance.musSource.mute = false;
		}
	}
	public void Pause(){
		if(_isPaused){
			_isPaused = false;
			Time.timeScale = 1;
		}else{
			_isPaused = true;
			Time.timeScale = 0;
		}
		
	}
	public void PlayGame(){
		SceneManager.LoadScene("SceneGame");
	}
	public void BackMenu(){
		SceneManager.LoadScene("MenuScene");
	}
	private void ShowScore(){
		if(_painelScore.GetComponent<Text>() != null)
			_painelScore.GetComponent<Text> ().text = GameManager.instance.score.ToString ();
	}
	public void ActiveCursor(){
		GameManager.instance.optionsPainelisActive = true;
		Cursor.visible = true;
		GameManager.instance.cursorIsActive = true;
	}
	public void DesablePainel(){
		GameManager.instance.optionsPainelisActive = false;
	}
}
