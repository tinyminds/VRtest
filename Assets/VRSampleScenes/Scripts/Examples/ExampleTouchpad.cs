using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

namespace VRStandardAssets.Examples
{
    // This script shows a simple example of how
    // swipe controls can be handled.
    public class ExampleTouchpad : MonoBehaviour
    {
        [SerializeField] private float m_Torque = 10f;
        [SerializeField] private VRInput m_VRInput;                                        
        [SerializeField] private Rigidbody m_Rigidbody;                                    
		private Transform m_CameraTransform;                            // Used to make sure the target is facing the camera.
		private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
		private Renderer m_Renderer;                                    // Used to make the target disappear before it is removed.
		private Collider m_Collider;  
		public GameObject m_Reticle;
		private Image m_Sprite;
		private bool eyesOn;

        private void OnEnable()
        {
           m_VRInput.OnSwipe += HandleSwipe;
			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
        }

		private void Awake()
		{
			// Setup the references.
			m_CameraTransform = Camera.main.transform;
			m_InteractiveItem = GetComponent<VRInteractiveItem>();
			m_Renderer = GetComponent<Renderer>();
			m_Collider = GetComponent<Collider>();
		}
        private void OnDisable()
        {
            m_VRInput.OnSwipe -= HandleSwipe;
			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
        }

		private void HandleOver()
		{

			m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = true;
			eyesOn = true;

		}

		private void HandleOut()
		{

			m_Sprite = m_Reticle.GetComponentsInChildren<Image> ()[0];
			m_Sprite.enabled = false;
			eyesOn = false;
		}
        //Handle the swipe events by applying AddTorque to the Ridigbody
        private void HandleSwipe(VRInput.SwipeDirection swipeDirection)
        {
			if (eyesOn) {
				switch (swipeDirection) {
				case VRInput.SwipeDirection.NONE:
					break;
				case VRInput.SwipeDirection.UP:
					m_Rigidbody.AddTorque (Vector3.right * m_Torque);
					break;
				case VRInput.SwipeDirection.DOWN:
					m_Rigidbody.AddTorque (-Vector3.right * m_Torque);
					break;
				case VRInput.SwipeDirection.LEFT:
					m_Rigidbody.AddTorque (Vector3.up * m_Torque);
					break;
				case VRInput.SwipeDirection.RIGHT:
					m_Rigidbody.AddTorque (-Vector3.up * m_Torque);
					break;
				}
			}
        }
    }
}