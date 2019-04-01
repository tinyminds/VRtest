using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherFollows : MonoBehaviour {
	public Transform m_CameraTransform; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf) {
			if(gameObject.transform.position!=new Vector3 (m_CameraTransform.transform.position.x + 2f, 20f, m_CameraTransform.transform.position.z + 2f))
			{
			gameObject.transform.position = new Vector3 (m_CameraTransform.transform.position.x + 2f, 20f, m_CameraTransform.transform.position.z + 2f);
			}
		}
	}
}