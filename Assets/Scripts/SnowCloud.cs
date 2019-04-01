using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class SnowCloud : MonoBehaviour {

	public GameObject m_snowParticles; 
	public OVRInput.Button snowButton;
	public Transform pointerTransform; // Could be a tracked controller
	public LayerMask snowLayerMask;
	public float maxCollectRange = 300;
	public Transform m_CameraTransform; 
	private bool doStuff = true;
	public GameObject skyStuff;
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (pointerTransform.position, pointerTransform.forward, out hit, maxCollectRange, snowLayerMask)) {
			if (OVRInput.GetDown (snowButton) || Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) {

				if(!skyStuff.GetComponent<checkWeather>().isSnowing&&doStuff)
				{
					StartCoroutine(makeSnowStart());
					doStuff = false;
				}
				else if(skyStuff.GetComponent<checkWeather>().isSnowing&&doStuff)
				{
					StartCoroutine(makeSnowStop());
					doStuff = false;
				}
			}
		}

	}




	IEnumerator makeSnowStart()
	{
		m_snowParticles.SetActive (true);
		m_snowParticles.transform.position = new Vector3 (m_CameraTransform.transform.position.x + 2f, 15f, m_CameraTransform.transform.position.z + 2f);
		yield return new WaitForSeconds(1f);
		doStuff = true;
		skyStuff.GetComponent<checkWeather>().isSnowing = true;
	}

	IEnumerator makeSnowStop()
	{
		m_snowParticles.SetActive (false);
		yield return new WaitForSeconds(1f);
		doStuff = true;
		skyStuff.GetComponent<checkWeather>().isSnowing = false;
	}

}