
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableController : MonoBehaviour {
	
    public OVRInput.Button collectButton;
	public GameObject invObj;
    public Transform pointerTransform; // Could be a tracked controller
    public LayerMask collectLayerMask;
	public float maxCollectRange = 500;
    private CollectableObject objectToCollect;
	public Rigidbody rb;
	public GameObject m_Holder;
	[SerializeField] private float m_Torque = 10f;
	CollectableObject tp;
	private GameObject theObject;
	private bool isHolding = false;
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
			if (Physics.Raycast (pointerTransform.position, pointerTransform.forward, out hit, maxCollectRange, collectLayerMask)) {
				Debug.Log ("hit");
				tp = hit.collider.gameObject.GetComponent<CollectableObject> ();
				//tp.OnLookAt();
				if (OVRInput.GetDown (collectButton) || Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) {
					putInInv (tp);
				}
		}
		//drop from inv instead?
		/*if((tp)&&(isHolding))
		{
			Debug.Log (tp.name);
			if (tp.isHeld&&(OVRInput.GetUp(collectButton) || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)))
			{
				drop(tp);
				isHolding = false;
			}

		}*/

	}

  /*  void pickUp(CollectableObject tp)
    {
       //lets put in inv here instead
		Debug.Log("pick up" + tp.name);
		tp.isHeld = true;
		rb = tp.gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		tp.gameObject.transform.SetParent (m_Holder.transform);
		tp.gameObject.transform.position = m_Holder.transform.position;
    }

	void drop(CollectableObject tp)
	{
		Debug.Log("drop" + tp.name);
		tp.isHeld = false;
		tp.gameObject.transform.parent = null;
		rb = tp.gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = true;
		rb.constraints = RigidbodyConstraints.None;
		tp = null;
	}
*/
	void putInInv(CollectableObject tp)
	{
		//Debug.Log("put in inv" + tp.theItemName);
		//if there is already something in the inventory drop it
		if (invObj.GetComponent<InvDisplayTwo> ().itemInInv) {
			//StartCoroutine (doPause ());
			Debug.Log ("already have an item in inventory so drop " + invObj.GetComponent<InvDisplayTwo> ().collectedGO.name);
			invObj.GetComponent<InvDisplayTwo> ().collectedGO.transform.position = m_Holder.transform.position;
			invObj.GetComponent<InvDisplayTwo> ().collectedGO.transform.parent = null;
			Debug.Log (invObj.GetComponent<InvDisplayTwo> ().collectedGO.transform.parent + "hello parent, be null");
			invObj.GetComponent<InvDisplayTwo> ().collectedGO.SetActive (true);
			invObj.GetComponent<InvDisplayTwo> ().isHeld = false;
			rb = invObj.GetComponent<InvDisplayTwo> ().collectedGO.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.None;
			Debug.Log ("dropped");
		} 
		Debug.Log ("pick up item and put in inventory");
		if (tp.isUsed) {
			invObj.GetComponent<InvDisplayTwo> ().itemName = tp.theItemUsedName;
			invObj.GetComponent<InvDisplayTwo> ().itemImage = tp.itemUsedImage;
		} else {
			invObj.GetComponent<InvDisplayTwo> ().itemName = tp.theItemName;
			invObj.GetComponent<InvDisplayTwo> ().itemImage = tp.itemImage;
		}
		invObj.GetComponent<InvDisplayTwo> ().itemNumber = tp.itemNumber;
		invObj.GetComponent<InvDisplayTwo> ().collectedGO = tp.gameObject;
		invObj.GetComponent<InvDisplayTwo> ().itemInInv = true;
		invObj.GetComponent<InvDisplayTwo> ().isHeld = false;
		tp.transform.parent = m_Holder.transform;
		tp.gameObject.SetActive (false);
		tp.isHeld = false;
		tp.isInInv = true;
	}
   
}
