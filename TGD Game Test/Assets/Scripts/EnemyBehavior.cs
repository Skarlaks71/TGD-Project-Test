﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	[SerializeField]
	private float _speed = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left*_speed*Time.deltaTime);
		if(transform.position.x < - 14f)
			Destroy(gameObject);
	}
	
}
