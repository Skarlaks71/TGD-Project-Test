using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	[SerializeField]
	private float _speed = 1.5f;
	private bool _isPlusBarrel = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		//box movement
		if(transform.position.x < -9.5f ){
			transform.position = new Vector3(-9.5f,transform.position.y,0f);
		}

		if(_isPlusBarrel){
			if(transform.position.x > 9.5f){
				transform.position = new Vector3(-1.2f,transform.position.y,0f);
			}
		}else{
			if(transform.position.x > -1.2f){
				transform.position = new Vector3(-1.2f,transform.position.y,0f);
			}
		}
		
		if(transform.position.y > 1.5f ){
			transform.position = new Vector3(transform.position.x,1.5f,0f);
		}
		if(transform.position.y < -8.5f){
			transform.position = new Vector3(transform.position.x,-8.5f,0f);
		}
	}
	private void Movement(){

		transform.Translate(Vector3.up*Input.GetAxisRaw("Vertical")*_speed*Time.deltaTime);
		
		transform.Translate(Vector3.right*Input.GetAxisRaw("Horizontal")*_speed*Time.deltaTime);
		
	}
	public void SetLimiteBarrel(bool value){
		_isPlusBarrel = value;
	}
}
