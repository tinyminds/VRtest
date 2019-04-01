using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

namespace VRStandardAssets.ShootingGallery
{
    // This script handles a target in the shooter scenes.
    // It includes what should happen when it is hit and
    // how long before it despawns.
    public class ShootingTarget : MonoBehaviour
    {
        public event Action<ShootingTarget> OnRemove;                   // This event is triggered when the target needs to be removed.
		private Image m_Sprite;
		public GameObject m_Reticle;
        [SerializeField] private int m_Score = 1;                       // This is the amount added to the users score when the target is hit.
        [SerializeField] private float m_TimeOutDuration = 2f;          // How long the target lasts before it disappears.
        [SerializeField] private float m_DestroyTimeOutDuration = 2f;   // When the target is hit, it shatters.  This is how long before the shattered pieces disappear.
        [SerializeField] private GameObject m_DestroyPrefab;            // The prefab for the shattered target.
        [SerializeField] private AudioClip m_DestroyClip;               // The audio clip to play when the target shatters.
        [SerializeField] private AudioClip m_SpawnClip;                 // The audio clip that plays when the target appears.
        [SerializeField] private AudioClip m_MissedClip;                // The audio clip that plays when the target disappears without being hit.


        private Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
        private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
        private AudioSource m_Audio;                                    // Used to play the various audio clips.
        private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
        private Collider m_Collider;                                    // Used to make sure the target doesn't interupt other shots happening.
        private bool m_IsEnding;                                        // Whether the target is currently being removed by another source.
        
        
        private void Awake()
        {
            // Setup the references.
            m_CameraTransform = Camera.main.transform;
            m_Audio = GetComponent<AudioSource> ();
            m_InteractiveItem = GetComponent<VRInteractiveItem>();
            m_Renderer = GetComponent<Renderer>();
            m_Collider = GetComponent<Collider>();
        }


        private void OnEnable ()
        {
            m_InteractiveItem.OnDown += HandleDown;
			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
        }


        private void OnDisable ()
        {
            m_InteractiveItem.OnDown -= HandleDown;
			m_InteractiveItem.OnOver -= HandleOver;
			m_InteractiveItem.OnOut -= HandleOut;
        }

		private void HandleOver()
		{

			m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = true;
		}

		private void HandleOut()
		{

			m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = false;
		}

        private void OnDestroy()
        {
        }
        

        public void Restart (float gameTimeRemaining)
        {
            // When the target is spawned turn the visual and physical aspects on.
            m_Renderer.enabled = true;
            m_Collider.enabled = true;
            
            // Play the spawn clip.
            m_Audio.clip = m_SpawnClip;
            m_Audio.Play();

            // Make sure the target is facing the camera.
            transform.LookAt(m_CameraTransform);

        }
        


        private void HandleDown()
        {

			GameObject destroyedTarget = Instantiate(m_DestroyPrefab, transform.position, transform.rotation) as GameObject;
			Vector3 direction = UnityEngine.Random.onUnitSphere;
			direction.y = direction.y + 2f;
			direction.z = transform.position.z;
			//direction.x = transform.position.x + UnityEngine.Random.Range(-4f, 4f);
			transform.position = direction;
            // Play the clip of the target being hit.
            m_Audio.clip = m_DestroyClip;
            m_Audio.Play();
            Destroy(destroyedTarget, m_DestroyTimeOutDuration);

        }
    }
}