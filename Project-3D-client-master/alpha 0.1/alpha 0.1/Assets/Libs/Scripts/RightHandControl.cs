﻿using UnityEngine;
using System.Collections;

public class RightHandControl : MonoBehaviour {
	private HandInteraction handInteraction;
	private bool isRightHandGripped;
	private bool isRightHandReleased;
	private bool childrenAttached = false;
	private bool isCaught = false;

	void Awake(){
		handInteraction = GameObject.Find("Player").GetComponent<HandInteraction>();
	}

	// Use this for initialization
	//void Start () {
	
	//}
	
	// Update is called once per frame
	void Update () {
		isRightHandGripped = handInteraction.isRightHandGripped;
		isRightHandReleased = handInteraction.isRightHandReleased;

		if(isRightHandReleased){
			this.gameObject.transform.DetachChildren();
			isCaught = false;
			childrenAttached = false;
			(gameObject.GetComponent("Halo") as Behaviour).enabled = true;
		}
	}

	void OnCollisionStay(Collision other){
		if("cube" == other.gameObject.transform.tag && isRightHandGripped){
			//this.gameObject.GetComponent("Halo").enabled = false;
			(gameObject.GetComponent("Halo") as Behaviour).enabled = false;
			isCaught = true;
		}
		if(isCaught && !childrenAttached){
			other.gameObject.transform.parent = this.gameObject.transform;
			childrenAttached = true;
		}
	
	}
}