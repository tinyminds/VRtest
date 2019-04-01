using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class RainCloud : MonoBehaviour {
	
	public GameObject m_rainParticles; 
	public OVRInput.Button rainButton;
	public Transform pointerTransform; // Could be a tracked controller
	public LayerMask rainLayerMask;
	public float maxCollectRange = 1000;
	public Transform m_CameraTransform;
	private bool doStuff = true;
	public GameObject skyStuff;

	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (pointerTransform.position, pointerTransform.forward, out hit, maxCollectRange, rainLayerMask)) {
			if (OVRInput.GetDown (rainButton) || Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) {
				if(!skyStuff.GetComponent<checkWeather> ().isRaining&&doStuff)
				{
					StartCoroutine(makeRainStart());
					doStuff = false;
				}
				if(skyStuff.GetComponent<checkWeather> ().isRaining&&doStuff)
				{
					StartCoroutine(makeRainStop());
					doStuff = false;
				}
			}
		}

	}
		
	IEnumerator makeRainStart()
	{
		Debug.Log ("start rain");
		m_rainParticles.SetActive (true);
		m_rainParticles.transform.position = new Vector3 (m_CameraTransform.transform.position.x + 2f, 15f, m_CameraTransform.transform.position.z + 2f);
		yield return new WaitForSeconds(1f);
		doStuff = true;
		skyStuff.GetComponent<checkWeather>().isRaining = true;
	}

	IEnumerator makeRainStop()
	{
		Debug.Log ("stop rain");
		m_rainParticles.SetActive (false);
		yield return new WaitForSeconds(1f);
		doStuff = true;
		skyStuff.GetComponent<checkWeather>().isRaining = false;
	}

}