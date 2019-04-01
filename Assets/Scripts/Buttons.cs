using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;


    public class Buttons : MonoBehaviour
    {
		public GameObject m_Reticle;
        private Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
        private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
        private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
        private Collider m_Collider;                                    // Used to make sure the target doesn't interupt other shots happening.
		private Image m_Sprite;
		public string buttonFunction;

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
			transform.GetComponent<Image> ().color = new Color32 (47, 191, 238, 117);
		}

		private void HandleOut()
		{

			m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = false;
			transform.GetComponent<Image> ().color = new Color32 (238, 193, 47, 117);
		}
		
        private void HandleDown()
        {
		transform.GetComponent<Image> ().color = new Color32 (23, 5, 47, 117);
			switch (buttonFunction)
			{
			case "center":
				Debug.Log ("one");
				UnityEngine.XR.InputTracking.Recenter();
			break;
			case "home":
				Debug.Log ("two");
			break;
			}
        }
    }
