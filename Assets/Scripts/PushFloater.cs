using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;


    public class PushFloater : MonoBehaviour
    {
		public GameObject m_Holder;
		public GameObject thisIs;
		public Transform m_Reticle;
        private Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
        private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
        private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
        private Collider m_Collider;                                    // Used to make sure the target doesn't interupt other shots happening.
		private Image m_Sprite;
		private bool isHeld;
		public Rigidbody rb;

	private void OnEnable()
	{
		m_InteractiveItem.OnDown += HandleDown;
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnUp += HandleOnUp;
	}


	private void OnDisable()
	{
		m_InteractiveItem.OnDown -= HandleDown;
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
		m_InteractiveItem.OnUp -= HandleOnUp;
	}


        private void Awake()
        {
            // Setup the references.
            m_CameraTransform = Camera.main.transform;
            m_InteractiveItem = GetComponent<VRInteractiveItem>();
            m_Renderer = GetComponent<Renderer>();
            m_Collider = GetComponent<Collider>();
        }
	private void Update()
	{
		if(!isHeld)
		{
			thisIs.transform.parent = null;
			rb = thisIs.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.None;
		}
	}

        private void HandleOver()
        {

			m_Sprite = GameObject.Find("GUIReticle").GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = true;
		}

		private void HandleOut()
		{

			m_Sprite =GameObject.Find("GUIReticle").GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = false;
		}
		
        private void HandleDown()
        {
			isHeld = true;
			rb = thisIs.GetComponent<Rigidbody> ();
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			thisIs.transform.parent = m_Holder.transform;
        }

		private void HandleOnUp()
		{
			isHeld = false;
			
		}
    }
