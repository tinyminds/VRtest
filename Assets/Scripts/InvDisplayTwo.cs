using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class InvDisplayTwo : MonoBehaviour
{
	public GameObject m_Holder;
	public GameObject m_Reticle;
	private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
	private Collider m_Collider;                                    // Used to make sure the target doesn't interupt other shots happening.
	public GameObject m_Player;
	private Image m_Sprite;
	public Text objectNameText;
	public Image objectImage;
	public string itemName = "Empty";
	public Sprite itemImage;
	public string itemDefaultName = "Empty";
	public Sprite itemDefaultImage;
	public int itemNumber = 0;
	public GameObject collectedGO;
	public bool isHeld = false;
	public bool itemInInv = false;
	public Rigidbody rb;
	public float floorPos = 0;
	public Button useButt;
	public Button dropButt;
	public string TaginUse;

	private void Awake()
	{
		// Setup the references.
		m_Renderer = GetComponent<Renderer>();
		m_Collider = GetComponent<Collider>();
	}

	void Update()
	{
		if (!itemInInv) {
			dropButt.interactable=false;
			useButt.interactable=false;
		} else {
			dropButt.interactable=true;
			//check for tagged object, if there enable use button.

			//if already used use TagsToUseUsedWith
			int a = 0;
			for(a = 0; a<collectedGO.GetComponent<CollectableObject> ().TagsToUseWith.Length; a++)
			{
				if(findUseTag(collectedGO.GetComponent<CollectableObject> ().TagsToUseWith[a])!=null)
				{
					TaginUse = collectedGO.GetComponent<CollectableObject> ().TagsToUseWith [a];
					//change use button text to useButtonText or usedUseButtontext
					useButt.interactable=true;
				}
			}
		}
		//if already used use usedItem ect
		objectNameText.text = itemName;
		objectImage.sprite = itemImage;

	}

	public void HandleDrop()
	{
		//drop if clicked
		if (itemNumber != 0) {
			Debug.Log ("drop item");
			itemName = itemDefaultName;
			itemImage = itemDefaultImage;
			itemNumber = 0;
			itemInInv = false;
			collectedGO.SetActive (true);
			collectedGO.GetComponent<CollectableObject> ().isHeld = false;
			collectedGO.GetComponent<CollectableObject> ().isInInv = false;
			Vector3 handPos = m_Holder.transform.position;
			//stop you dropping through the floor - fiddle
			if(handPos.y<floorPos)
			{
				handPos.y = floorPos+1f;
			}
			collectedGO.transform.position = handPos;
			collectedGO.transform.parent = null;
			rb = collectedGO.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.None;
			isHeld = false;
		}
	}

	public void HandleUse()
	{
		//if canBeConsumed use ConsumeTriggerAction
		//isused already use the UsedTriggerAction
		//and theItemName and itemImage istead and isusedused
		Debug.Log ("use" + TaginUse);
		//collectedGO.GetComponent<CollectableObject> ().TriggerAction
		//massive switch statement - get 
		itemName = collectedGO.GetComponent<CollectableObject> ().theItemUsedName;
		itemImage = collectedGO.GetComponent<CollectableObject> ().itemUsedImage;
		collectedGO.GetComponent<CollectableObject> ().isUsed = true;
	}

	public Transform findUseTag(string theTag)
	{
		// The minimum distance we are looking at lateron
		float minDistance = float.MaxValue;
	 
		// Get the collisions
		Collider[] hitColliders = Physics.OverlapSphere(m_Player.transform.position, 2.5f);
		int i = 0;
		while (i < hitColliders.Length)
		{
			//Debug.Log ("colliders: " + hitColliders[i].tag + " tag: " + theTag);
			// Check, if the collision object has the correct tag
			if (hitColliders[i].tag.Equals(theTag))
			{
	 
				// Get the position of the collider we are looking at
				Vector3 possiblePosition = hitColliders[i].transform.position;
	 
				// Get the distance between us and the collider
				float currDistance = Vector3.Distance(m_Reticle.transform.position, possiblePosition);
	 
				// If the distance is smaller than the one before...
				if (currDistance < minDistance)
				{
					return hitColliders[i].transform;
					break;
					minDistance = currDistance;
				}
			}
			i++;
		}
		return null;
	}	
}
