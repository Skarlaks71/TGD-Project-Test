using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	[SerializeField]
	private float _speed = 3.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.down*_speed*Time.deltaTime);	

	}
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Ground"){
			Debug.Log("pisei");
			Destroy(gameObject);
		}
	}
}
