using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {
	
	[Header("Offeset Speed")]
	[SerializeField][Range(-1,1)] private float _speedX;
	[SerializeField][Range(-1,1)] private float _speedY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(Time.time*_speedX,Time.time*_speedY);

		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
