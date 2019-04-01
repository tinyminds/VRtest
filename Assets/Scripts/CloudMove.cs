using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {
	public float speed = 0.5f;
	private Vector3 sphere;
	public float startX;
	public float startZ;
	public float moveToX;
	public float moveToZ;
	private float t = 0f;
	public int count = 0;
	// Use this for initialization
	void Start () {
		sphere = Random.onUnitSphere * 15;
		startX = transform.position.x;
		startZ = transform.position.z;
		moveToX = sphere.x; 
		moveToZ = sphere.y; 
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Round(transform.position.x) != Mathf.Round(moveToX)) {
			transform.position = new Vector3 (Mathf.Lerp (startX, moveToX, t), transform.position.y, Mathf.Lerp (startZ, moveToZ, t));
			t += (speed/2) * Time.deltaTime;
		} else {
			count++;
			t = 0f;
			if (count == 1) {
				moveToX = startX; 
				moveToZ = startZ; 
			} else {
				sphere = Random.onUnitSphere * 15;
				moveToX = sphere.x; 
				moveToZ = sphere.y;  
				count = 0;
			}
			startX = transform.position.x;
			startZ = transform.position.z;
		}
	}
}
