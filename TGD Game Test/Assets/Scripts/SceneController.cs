using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
	
	[SerializeField]
	private GameObject _groundTilemap;
	[SerializeField]
	private GameObject _platformTilemap;
	[SerializeField]
	private GameObject _enemy;
	[SerializeField]
	private GameObject _player;
	[SerializeField]
	private GameObject _player2;
	[SerializeField]
	private GameObject _coinPfb;
	[SerializeField]
	private Button _escBtn;
	[SerializeField]
	private GameObject _painelEnd;
	[SerializeField]
	private Transform[] _targetCoinPosition;
	[SerializeField]
	private bool _isGetCoin = false;
	private bool _gerateCoin = false;
	private int _numCoin = 0;

	private bool _activeEnemy = false;
	private bool _playerDie = false;
	[SerializeField][Range(0,1)]
	private float _timeSpawn = 1f; 
	[SerializeField]
	private float _timer = 60f;
	private float _canSpawn = 0f; 
	
	void Start () {
		Time.timeScale = 1;
		GameManager.instance.optionsPainelisActive = false;
		if(Cursor.visible == true){
			GameManager.instance.cursorIsActive = true;
		}
		GameManager.instance.score = 0;
	}
	
	IEnumerator DelaySpawnEnemy(){
		yield return new WaitForSeconds(_timeSpawn);
		SpawnEnemy();
	}

	void Update () {
		if(_isGetCoin && _numCoin == 1){
			ActivePlatform();
		}
		if(_player == null){
			_painelEnd.SetActive(true);
			GameManager.instance.optionsPainelisActive = true;
			Cursor.visible = true;
			_painelEnd.transform.GetChild(0).gameObject.SetActive(true);
			Time.timeScale = 0;
		}
		if(Input.GetButtonDown("Cancel") && GameManager.instance.optionsPainelisActive == false){
			_escBtn.onClick.Invoke();
		}
	}

	public bool CloudMovementOn(){
		return _isGetCoin;
	}

	public void ActiveObject(){
		_isGetCoin = true;
		_gerateCoin = true;
		SpawnCoins(_numCoin);
	}

	private void ActiveGravity(){
		_platformTilemap.GetComponent<Platform>().enabled = false;
		_platformTilemap.GetComponent<Gravity>().enabled = true;
		_groundTilemap.GetComponent<Gravity>().enabled = true;
	}

	private void ActivePlatform(){
		_platformTilemap.GetComponent<Platform>().enabled = true;
	}

	private void SpawnEnemy(){
		ActiveClock();
		if(_activeEnemy){
			GameObject enemy = (GameObject)Instantiate(_enemy,new Vector3(15f,Random.Range(-5.37f,5.37f),0f),Quaternion.identity);
			StartCoroutine(DelaySpawnEnemy());
		}else{
			_player2.GetComponent<Tree>().SetLimiteBarrel(true);
		}
	}

	private void SpawnCoins(int num){
		if(_gerateCoin){
			
			switch(num){
				case 0:
					Instantiate(_coinPfb,_targetCoinPosition[num].position,Quaternion.identity);
					_gerateCoin = false;
					break;
				case 1:
					Instantiate(_coinPfb,_targetCoinPosition[num].position,Quaternion.identity);
					_gerateCoin = false;
					//Ative a gravidade para remover o chão
					ActiveGravity();
					//Alterne os controles do Jogador
					SwitchPlayerController(0);
					//Invoque os inimigos
					_activeEnemy = true;
					_canSpawn = Time.time + _timer;
					SpawnEnemy();

					break;
					case 2:
						//momento Vitoria
						_painelEnd.SetActive(true);
						GameManager.instance.optionsPainelisActive = true;
						Cursor.visible = true;
						_painelEnd.transform.GetChild(1).gameObject.SetActive(true);
						Time.timeScale = 0;
						break;
				
			}
			Debug.Log("Gerou");
			_numCoin++;
			Debug.Log("Gerou");
		}
		

	}

	public void ActiveClock(){
		if(Time.time > _canSpawn){
			_activeEnemy = false;
		}
	}
	
	private void SwitchPlayerController(int num){
		if(num == 0){
			_player.GetComponent<Player>().SetRgdMoviment(false);
			_player.transform.SetParent(_player2.transform);
			_player2.GetComponent<Tree>().enabled = true;
		}else{
			_player2.GetComponent<Tree>().enabled = false;
			_player.GetComponent<Player>().SetRgdMoviment(true);
		}
	}

}
