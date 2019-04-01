using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class CollectableItemTwo : MonoBehaviour {
	public string theItemName;
	public Sprite itemImage;
	public int itemNumber;
	private GameObject invObj;
	public Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
	private bool isHeld = false;
	public Rigidbody rb;
	public GameObject m_Holder;
	[SerializeField] private float m_Torque = 10f;

	
	void Awake(){
		//invObj = GameObject.Find ("Inv");
	}


	private void OnEnable()
	{
		Debug.Log ("here on enable");
	}


	private void OnDisable()
	{
	}

	private void HandleOver()
	{
		Debug.Log ("over");
	}

	private void HandleOut()
	{
		Debug.Log ("out");
	}
		

	public void HandleDown()
	{
		Debug.Log ("click");
		//check if the hand is holding anything already and drop that first if so.
		if (!isHeld) {
			Debug.Log ("button down pick up");
			isHeld = true;
			rb = gameObject.GetComponent<Rigidbody> ();
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			gameObject.transform.SetParent (m_Holder.transform);
			gameObject.transform.position = m_Holder.transform.position;
		}
	}

	public void HandleDoubleClick()
	{
		Debug.Log ("doubleclick");
		if (isHeld) {
			Debug.Log ("double click if held");
			if (invObj.GetComponent<InvDisplay> ().itemInInv) {
				//StartCoroutine (doPause ());
				Debug.Log ("already have an item in inventory so drop " + invObj.GetComponent<InvDisplay> ().collectedGO.name);
				invObj.GetComponent<InvDisplay> ().collectedGO.transform.position = m_Holder.transform.position;
				invObj.GetComponent<InvDisplay> ().collectedGO.transform.parent = null;
				Debug.Log (invObj.GetComponent<InvDisplay> ().collectedGO.transform.parent + "hello parent, be null");
				invObj.GetComponent<InvDisplay> ().collectedGO.SetActive (true);
				invObj.GetComponent<InvDisplay> ().isHeld = false;
				rb = invObj.GetComponent<InvDisplay> ().collectedGO.GetComponent<Rigidbody> ();
				rb.useGravity = true;
				rb.constraints = RigidbodyConstraints.None;
				Debug.Log ("dropped");
			} 
			Debug.Log ("pick up item and put in inventory");
			invObj.GetComponent<InvDisplay> ().itemName = theItemName;
			invObj.GetComponent<InvDisplay> ().itemImage = itemImage;
			invObj.GetComponent<InvDisplay> ().itemNumber = itemNumber;
			invObj.GetComponent<InvDisplay> ().collectedGO = gameObject;
			invObj.GetComponent<InvDisplay> ().itemInInv = true;
			invObj.GetComponent<InvDisplay> ().isHeld = false;
			invObj.GetComponent<InvDisplay> ().collectedGO.transform.parent = null;
			gameObject.SetActive (false);
			isHeld = false;
		}	
	}

	IEnumerator doPause()
	{
		Debug.Log ("pause");
		yield return new WaitForSeconds(1f);
	}
		
	void Start () {
		
	}
	
	void Update()
	{

	}
}
