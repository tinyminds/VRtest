using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class CollectableItem : MonoBehaviour {
	public string theItemName;
	public Sprite itemImage;
	public int itemNumber;
	public GameObject m_Reticle;
	private Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
	private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
	private Image m_Sprite;
	private GameObject invObj;
	private bool isHeld = false;
	public Rigidbody rb;
	public GameObject m_Holder;
	[SerializeField] private VRInput m_VRInput;   
	[SerializeField] private float m_Torque = 10f;
	public static readonly Color COLOR_GRAB = new Color(1.0f, 0.5f, 0.0f, 1.0f);
    public static readonly Color COLOR_HIGHLIGHT = new Color(1.0f, 0.0f, 1.0f, 1.0f);
	private Color m_color;
	public Renderer rend;
	public Material meshMaterial;
	private Animator m_animator = null;
	private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;
	
	void Awake(){
		invObj = GameObject.Find ("Inv");
		m_CameraTransform = Camera.main.transform;
		m_InteractiveItem = GetComponent<VRInteractiveItem>();
		rend = gameObject.GetComponent<Renderer> ();
		//if (rend) {
		//	meshMaterial = rend.material;
		//}
		//m_color = meshMaterial.color;
		            // Get animator layer indices by name, for later use switching between hand visuals
	}


	private void OnEnable()
	{
		m_InteractiveItem.OnUp += HandleDown;
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_VRInput.OnSwipe += HandleSwipe;
		m_VRInput.OnDoubleClick += HandleDoubleClick;
	}


	private void OnDisable()
	{
		m_InteractiveItem.OnUp -= HandleDown;
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
		m_VRInput.OnSwipe -= HandleSwipe;
		m_VRInput.OnDoubleClick -= HandleDoubleClick; 
	}

	private void HandleOver()
	{
		SetColor(COLOR_HIGHLIGHT);
		//change the colour of the reticle for pickupable items - add another dif coloured image as [1]?
		m_Sprite = GameObject.Find("GUIReticle").GetComponentsInChildren<Image> ()[0];
		m_Sprite.color = new Color32 (227, 225, 40, 255);
		m_Sprite.enabled = true;
	}

	private void HandleOut()
	{
		SetColor(m_color);
		m_Sprite = GameObject.Find("GUIReticle").GetComponentsInChildren<Image> ()[0];
		m_Sprite.color = new Color32 (223, 30, 99, 255);
		m_Sprite.enabled = false;
	}

	private void HandleSwipe(VRInput.SwipeDirection swipeDirection )
	{
		SetColor(m_color);
		if ((isHeld)&&(swipeDirection!=VRInput.SwipeDirection.NONE)) {
			Debug.Log ("drop and swipey" + swipeDirection);
			isHeld = false;
			gameObject.transform.parent = null;
			rb = gameObject.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.None;
			//probably should add some velocity here too, not just spin
			switch (swipeDirection) {
			case VRInput.SwipeDirection.UP:
				rb.AddForce (Vector3.right * m_Torque);
				break;
			case VRInput.SwipeDirection.DOWN:
				rb.AddForce (-Vector3.right * m_Torque);
				break;
			case VRInput.SwipeDirection.LEFT:
				rb.AddForce (Vector3.up * m_Torque);
				break;
			case VRInput.SwipeDirection.RIGHT:
				rb.AddForce (-Vector3.up * m_Torque);
				break;
			}
		}
	}

	private void HandleDown()
	{
		
		//check if the hand is holding anything already and drop that first if so.
		if (!isHeld) {
			//UpdateAnimStates ();
			SetColor(COLOR_GRAB);
			Debug.Log ("button down pick up");
			isHeld = true;
			rb = gameObject.GetComponent<Rigidbody> ();
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			gameObject.transform.SetParent (m_Holder.transform);
			gameObject.transform.position = m_Holder.transform.position;
		}
	}

	private void HandleDoubleClick()
	{
		if (isHeld) {
			SetColor(m_color);
			Debug.Log ("double click if held");
			//change recticle colour back if picked up
			//m_Sprite = GameObject.Find ("GUIReticle").GetComponentsInChildren<Image> () [0];
			//m_Sprite.color = new Color32 (223, 30, 99, 255);
			//m_Sprite.enabled = false;
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
				//StartCoroutine (doPause ());
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
	
	private void Update()
	{

	}
	 private void SetColor(Color color)
     {
		//could do this if it's the outline shader, change the outline colour not main colour
		//if (rend) {
		//	meshMaterial.color = color;
		//}
      }
	  
	 private void UpdateAnimStates()
        {
     
            // Pose
            //m_animator.SetInteger(m_animParamIndexPose, (int)handPoseId);

            // Flex
            // blend between open hand and fully closed fist
			
            //m_animator.SetFloat(m_animParamIndexFlex, flex);

            // Point
           // m_animator.SetLayerWeight(m_animLayerIndexPoint, point);

            // Thumbs up
           // m_animator.SetLayerWeight(m_animLayerIndexThumb, thumbsUp);
			
			//pinch
            m_animator.SetFloat("Pinch", 10);
        } 
}
