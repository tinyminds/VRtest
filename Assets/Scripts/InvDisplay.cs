using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;


public class InvDisplay : MonoBehaviour
{
	public GameObject m_Holder;
	public GameObject m_Reticle;
	private Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
	private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
	private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
	private Collider m_Collider;                                    // Used to make sure the target doesn't interupt other shots happening.
	private Image m_Sprite;
	public string itemName = "Empty";
	public Sprite itemImage;
	public string itemDefaultName = "Empty";
	public Sprite itemDefaultImage;
	public int itemNumber = 0;
	public GameObject collectedGO;
	public bool isHeld = false;
	public bool itemInInv = false;
	public Rigidbody rb;

	private void OnEnable()
	{
		m_InteractiveItem.OnDown += HandleDown;
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
	}


	private void OnDisable()
	{
		m_InteractiveItem.OnDown -= HandleDown;
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
	}


	private void Awake()
	{
		// Setup the references.
		m_CameraTransform = Camera.main.transform;
		m_InteractiveItem = GetComponent<VRInteractiveItem>();
		m_Renderer = GetComponent<Renderer>();
		m_Collider = GetComponent<Collider>();
	}

	private void HandleOver()
	{

		m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
		m_Sprite.enabled = true;
		//yellow for collectable items
		transform.GetComponent<Image> ().color = new Color32 (47, 191, 238, 117);
	}

	private void HandleOut()
	{

		m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
		m_Sprite.enabled = false;
		transform.GetComponent<Image> ().color = new Color32 (238, 193, 47, 117);
	}

	public void HandleDown()
	{
		if (itemNumber != 0) {
			Debug.Log ("drop item");
			isHeld = true;
			itemName = itemDefaultName;
			itemImage = itemDefaultImage;
			itemNumber = 0;
			itemInInv = false;
		}
		//attach to gaze
		//double click to drop
	}

	void Update()
	{
		GetComponentInChildren<Text> ().text = itemName;
		GetComponentInChildren<Image> ().sprite = itemImage;
		//if there is an item in the inventory and it is clicked on drop it
		if(isHeld)
			{
				collectedGO.SetActive (true);
				collectedGO.transform.position = m_Holder.transform.position;
				collectedGO.transform.parent = null;
				rb = collectedGO.GetComponent<Rigidbody> ();
				rb.useGravity = true;
				rb.constraints = RigidbodyConstraints.None;
				isHeld = false;
			}
		}
}
