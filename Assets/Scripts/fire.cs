using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {
	public GameObject fireParticles;
	public GameObject smokeParticles;
	public GameObject rainObject;
	public GameObject snowObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((rainObject.activeSelf)||(snowObject.activeSelf))
			{
				StartCoroutine(putOutFire ());
			}
	}

	IEnumerator putOutFire()
	{
		yield return new WaitForSeconds(2f);
		smokeParticles.SetActive(true);
		yield return new WaitForSeconds(1f);
		fireParticles.SetActive(false);
		GetComponent<AudioSource> ().Stop ();
	}


}
