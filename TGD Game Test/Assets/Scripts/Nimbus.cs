using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimbus : MonoBehaviour {

	[SerializeField]
	private float _speed = 1.5f;
	private int _numDirection;
	private float _direction;

	private GameObject _sceneManager;
	private bool _activeCloud = false;

	private void Awake(){
		_sceneManager = GameObject.FindGameObjectWithTag("SM");
	}
	// Use this for initialization
	void Start () {
		_numDirection = RandomOne();
		
	}

	// Update is called once per frame
	void Update () {
		_activeCloud = _sceneManager.GetComponent<SceneController>().CloudMovementOn();

		if(_activeCloud){
			_direction = Vector3.right.x*_numDirection*_speed*Time.deltaTime;
			transform.Translate(Vector3.right*_numDirection*_speed*Time.deltaTime);
		}

		if(transform.position.x > 13f){
			transform.position = new Vector2(-13f,transform.position.y);
		}else if(transform.position.x < -13f){
			transform.position = new Vector2(13f,transform.position.y);
		}
	}

	private int RandomOne(){
		int num = 0;
		num = Random.Range(1,10);
		if(num%2 == 0){
			return 1;
		}else{
			return -1;
		}
	}
}
