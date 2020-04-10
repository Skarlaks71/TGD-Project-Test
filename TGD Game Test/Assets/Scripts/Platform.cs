using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private float _speed = 1.5f;
	private Vector3 _currentPosition;
	[SerializeField]
	private Transform[] _targetPosition;
	[SerializeField]
	private bool _move = false;
	private bool _isActivatePlatform = false;
	[SerializeField]
	private Transform _inicialPosition;
	private Vector3 _posDesired;
	private Vector3[] _targetDesired = new Vector3[2];

	// Use this for initialization
	void Start () {
		Debug.Log("Ativado");
		_posDesired = transform.position - _inicialPosition.position;
		_targetDesired[0] = _posDesired + _targetPosition[0].position;
		_targetDesired[1] = transform.position - (_inicialPosition.position - _targetPosition[1].position);
		
		Debug.Log("T: "+_inicialPosition.position);
		StartCoroutine(ActivePlatform());
	}
	IEnumerator ActivePlatform(){
		float step = _speed * Time.deltaTime;
		while(transform.position.y != _posDesired.y){
			Debug.Log("current: "+transform.position);
			Debug.Log(_inicialPosition.position);
			transform.position = Vector3.MoveTowards(transform.position,_posDesired,step);
			yield return null;
		}
		
		_isActivatePlatform = true;
		// if(_move == false){
		// 	StartCoroutine(SwitchPosition());
		// 	yield break;
		// }

		
	}
	IEnumerator SwitchPosition(){
		yield return new WaitForSeconds(2);
		Debug.Log("passou 2s");
		_move = !_move;
	}
	// Update is called once per frame
	void Update () {
		float step = _speed * Time.deltaTime;
		_currentPosition = transform.position - _posDesired;
		if(_isActivatePlatform){
			
			if(transform.position.y != _targetDesired[0].y && _move == false){
				Debug.Log(_targetPosition[0].position.y);
				transform.position = Vector3.MoveTowards(transform.position,_targetDesired[0],step);
				if(transform.position.y == _targetDesired[0].y){
					StartCoroutine(SwitchPosition());
				}
				
			}
			
			if(transform.position.y != _targetDesired[1].y && _move == true){
				
				transform.position = Vector3.MoveTowards(transform.position,_targetDesired[1],step);
				if(transform.position.y == _targetDesired[1].y){
					StartCoroutine(SwitchPosition());
				}
			}
		}
			
		
		
	}
}
