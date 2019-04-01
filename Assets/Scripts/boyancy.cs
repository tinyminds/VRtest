using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boyancy : MonoBehaviour {
	public float waterLevel;
	public float floatHeight;
	public Vector3 buoyancyCentreOffset;
	public float bounceDamp;
	public bool isInWater = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate () {
		if(isInWater)
		{
			//Debug.Log ("in water");
			var actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
			var forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

			if (forceFactor > 0f) {
				var uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
				GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
			}
		}
	}
}
