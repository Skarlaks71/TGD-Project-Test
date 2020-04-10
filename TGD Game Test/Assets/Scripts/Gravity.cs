using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
	
	[SerializeField]
	private float _speed = 3f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down*_speed*Time.deltaTime);

		if(transform.position.y < -15f){
			Destroy(this);
		}
	}
}
