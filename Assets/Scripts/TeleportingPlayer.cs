using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;


    public class TeleportingPlayer : MonoBehaviour
    {

		public UnityEngine.GameObject Target;
		[SerializeField] private Image m_FadeImage;  
        private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
        private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
        private Collider m_Collider;                                    // Used to make sure the target doesn't interupt other shots happening.
		private Image m_Sprite;
		public GameObject m_Reticle;
		public float dimmingSpeed = 1;
		public float fullIntensity = 1;
		public float lowIntensity = 0.5f;
		public Transform destTransform;
		public int teleportNum;
		private float lastLookAtTime = 0;
		public bool FirstVisit = true;
		public bool Visited = false;
		public bool Completed = false;
		private GameObject controllerObj;

	void Update () {
		float intensity = Mathf.SmoothStep(fullIntensity, lowIntensity, (Time.time - lastLookAtTime) * dimmingSpeed);
		GetComponent<MeshRenderer>().material.SetFloat("_Intensity", intensity);
	}

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

	private void HandleOver()
	{

		m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
		m_Sprite.enabled = true;
		lastLookAtTime = Time.time;
	}

	private void HandleOut()
	{

		m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
		m_Sprite.enabled = false;
		lastLookAtTime = 0;
	}

        private void Awake()
        {
			controllerObj = GameObject.Find ("Controller");
            m_InteractiveItem = GetComponent<VRInteractiveItem>();
            m_Renderer = GetComponent<Renderer>();
            m_Collider = GetComponent<Collider>();
        }
		

        private void HandleDown()
        {

			StartCoroutine(TeleportCoroutine(destTransform));

        }

	IEnumerator TeleportCoroutine(Transform destTransform)
	{
		Vector3 destPosition = transform.position;
		Quaternion destRotation = transform.rotation;

		float fadeLevel = 0;
		m_FadeImage.enabled = true;
		Color temp = m_FadeImage.color;
		temp.a=255f;
		m_FadeImage.color = temp;
		while (fadeLevel < 1)
		{
			yield return null;
			fadeLevel += 1f * Time.deltaTime;
			fadeLevel = Mathf.Clamp01(fadeLevel);
			m_FadeImage.CrossFadeAlpha (fadeLevel, 1f, false);
		}


		Target.transform.position = destPosition;

		if (FirstVisit) {
			Debug.Log ("first visit? " + teleportNum);
			Visited = true;
		}
		if (Visited) {
			Debug.Log ("is visited? " + teleportNum);
			controllerObj.GetComponent<Controller> ().teleportControll (teleportNum);
			Material Material = controllerObj.GetComponent<Controller> ().GlowVisted;
			GetComponent<Renderer> ().material = Material;
		}
		if (Completed) {
			Debug.Log ("is completed? " + teleportNum);
			Material Material = controllerObj.GetComponent<Controller> ().GlowCompleted;
			GetComponent<Renderer> ().material = Material;
		}
		controllerObj.GetComponent<Controller> ().teleportNumber = teleportNum;

		yield return new WaitForSeconds(0.5f);
	
		while (fadeLevel > 0)
		{
			yield return null;
			fadeLevel -= 1f * Time.deltaTime;
			fadeLevel = Mathf.Clamp01(fadeLevel);
			m_FadeImage.CrossFadeAlpha (fadeLevel, 1f, false);
		}
		m_FadeImage.enabled = false;
		FirstVisit = false;

		yield return null;
	}
    }
