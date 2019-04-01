using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	public Material GlowVisted;
	public Material GlowUnVisted;
	public Material GlowCompleted;
	public GameObject[] Teleports;
	public GameObject[] RainClouds;
	public GameObject[] SnowClouds;
	public GameObject rainParticles;
	public GameObject snowParticles;
	public GameObject fireParticles;
	public GameObject smokeParticles;
	public GameObject objBlob;
	public GameObject objTarget;
	public GameObject objboids;
	public GameObject[] objPrimatives;
	public GameObject SwipeCube;
	public string[] TeleportTxts;
	public GameObject objFloorMenu;
	private Transform m_CameraTransform;
	public GameObject menuParent;
	public int teleportNumber = 1;
	private bool doneOnce = false;
	public GameObject hintText;
	void Awake () {
		m_CameraTransform = Camera.main.transform;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (m_CameraTransform.transform.eulerAngles.x > 30 && m_CameraTransform.transform.eulerAngles.x < 180) 
		{
			if (!doneOnce) {
				Vector3 camPos = menuParent.transform.localPosition;
				Vector3 camAng = m_CameraTransform.transform.eulerAngles;
				objFloorMenu.transform.parent =	null;
				objFloorMenu.transform.position = camPos;
				objFloorMenu.transform.eulerAngles = new Vector3 (0, camAng.y, 0);
				objFloorMenu.SetActive (true);
				doneOnce = true;
			}
		}
		else
		{
			objFloorMenu.SetActive(false);
			doneOnce = false;
		}

	}

	public void teleportControll(int teleportNum) {

		objFloorMenu.transform.SetParent(menuParent.transform);
		hintText.GetComponent<Text> ().text = TeleportTxts [teleportNum-1];
		objFloorMenu.SetActive(false);
		turnOffAllClouds ();
		fireParticles.SetActive(true);
		smokeParticles.SetActive(false);
	}
	public void turnOffAllClouds()
	{
		turnOffRainClouds ();
		turnOffSnowClouds ();
	}

	public void turnOffRainClouds()
	{
		//turn off the rain particles
		//foreach (GameObject rainCloud in RainClouds) {
			rainParticles.SetActive (false);
		//}
	}

	public void turnOffSnowClouds()
	{
		//turn off the snow particles
		//foreach (GameObject snowCloud in SnowClouds) {
			snowParticles.SetActive (false);
		//}
	}

	IEnumerator fadeAway(GameObject objText)
	{
		yield return new WaitForSeconds(10f);	
	}
}
