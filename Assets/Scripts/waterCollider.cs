using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Floaters")
		{
			other.GetComponent<boyancy>().isInWater = true;
			Debug.Log ("waaater");
		}
	}


	public void OnTriggerExit (Collider other)
	{
		if (other.tag == "Floaters") {
			other.GetComponent<boyancy> ().isInWater = false;
			Debug.Log ("out water");
		}
		
	}
}
