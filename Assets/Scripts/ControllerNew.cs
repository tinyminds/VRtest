using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerNew : MonoBehaviour {

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
	public Transform pointerTransform;
	public GameObject hintText;

	void Awake () {
		m_CameraTransform = Camera.main.transform;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


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
