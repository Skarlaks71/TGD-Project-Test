using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVerify : MonoBehaviour {

	
	void Start () {
		if(GameManager.instance.musSource.mute == false){
			GetComponent<Toggle>().isOn = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
