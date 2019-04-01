/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TeleportController1 : MonoBehaviour {
	public GameObject m_Player;
	public Transform m_originTeleportTransform;
    public float maxTeleportRange;
    public OVRInput.Button teleportButton;
    public KeyCode teleportKey;
    public Transform pointerTransform; // Could be a tracked controller
    public bool allowRotation;
    public bool allowForRealHeadRotation;
    public float realHeadMovementCompensation;
    public float rotationSpeed = 1;
	public GameObject skyStuff;
    public float fadeSpeed = 1f;
    public float fadeLength = 0.5f;

    public float rotateStickThreshold = 0.5f;

    [HideInInspector()]
    public bool teleportEnabled = true;

    public GameObject positionIndicatorPrefab;

    

    public LayerMask teleportLayerMask;


    private GameObject positionIndicator;
    private TeleportPoint1 currentTeleportPoint;
    private float rotationAmount;
    private Quaternion initialRotation;
    private bool teleporting = false;
	public GameObject objFloorMenu;
	public Transform m_CameraTransform;
	private bool doneOnce = false;
	public GameObject menuParent;

	public Material GlowVisted;
	public Material GlowUnVisted;
	public Material GlowCompleted;
	public GameObject[] Teleports;
	public GameObject[] RainClouds;
	public GameObject[] SnowClouds;
	public GameObject rainParticles;
	public GameObject snowParticles;
	public GameObject fireParticles;
	public GameObject campFire;
	public GameObject smokeParticles;
	public string[] TeleportTxts;
	public int teleportNumber = 1;
	public GameObject hintText;

	void Awake() {
		currentTeleportPoint = Teleports [0].GetComponent<TeleportPoint1> ();
		Debug.Log ("awake");
	}
	void Update () {
		if (m_Player.transform.position.y < -26) {
			StartCoroutine(TeleportCoroutine(m_originTeleportTransform));
		}	

		if (m_CameraTransform.transform.eulerAngles.x > 30 && m_CameraTransform.transform.eulerAngles.x < 180) 
		{
			if (!doneOnce) {
				Vector3 camPos = menuParent.transform.localPosition;
				Vector3 camAng = m_CameraTransform.transform.eulerAngles;
				objFloorMenu.transform.parent =	null;
				objFloorMenu.transform.position = camPos;

				objFloorMenu.transform.eulerAngles = new Vector3 (0, camAng.y, 0);
				objFloorMenu.SetActive (true);
				doneOnce = true;
			}
		}
		else
		{
			objFloorMenu.SetActive(false);
			doneOnce = false;
		}

        RaycastHit hit;
        if (positionIndicator)
        {
            
			if (allowRotation)
            {
                float leftStickRotation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
                if (Mathf.Abs(leftStickRotation) > rotateStickThreshold)
                {
                    rotationAmount += Time.deltaTime * leftStickRotation * rotationSpeed;
                    positionIndicator.transform.rotation = Quaternion.Euler(new Vector3(0, rotationAmount, 0)) * initialRotation;
                }
            }

            if (OVRInput.GetUp(teleportButton) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                DoTeleport(positionIndicator.transform);
            }
        }
        else if (Physics.Raycast(pointerTransform.position, pointerTransform.forward, out hit, maxTeleportRange, teleportLayerMask))
        {
            TeleportPoint1 tp = hit.collider.gameObject.GetComponent<TeleportPoint1>();
            tp.OnLookAt();

            if (teleportEnabled && !teleporting && (OVRInput.GetDown(teleportButton) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                StartTeleport(tp);
				teleportNumber = tp.teleportNumber;
            }
            
        }   

	}

    void StartTeleport(TeleportPoint1 tp)
    {
        teleporting = true;

        if (currentTeleportPoint)
        {
            currentTeleportPoint.GetComponent<MeshRenderer>().enabled = true;
			currentTeleportPoint.GetComponent<CapsuleCollider>().enabled = true;
        }
        currentTeleportPoint = tp;
        currentTeleportPoint.GetComponent<MeshRenderer>().enabled = false;
		currentTeleportPoint.GetComponent<CapsuleCollider>().enabled = false;
        positionIndicator = GameObject.Instantiate<GameObject>(positionIndicatorPrefab);
        positionIndicator.transform.position = tp.GetDestTransform().position;
        initialRotation = positionIndicator.transform.rotation = tp.GetDestTransform().rotation;  
        rotationAmount = 0;
    }

    void DoTeleport(Transform destTransform)
    {
        StartCoroutine(TeleportCoroutine(destTransform));
    }
		
    IEnumerator TeleportCoroutine(Transform destTransform)
    {
        Vector3 destPosition = destTransform.position;
        Quaternion destRotation = destTransform.rotation;
        float fadeLevel = 0;

        while (fadeLevel < 1)
        {
            yield return null;
            fadeLevel += fadeSpeed * Time.deltaTime;
            fadeLevel = Mathf.Clamp01(fadeLevel);
//            OVRInspector.instance.fader.SetFadeLevel(fadeLevel);
        }

        transform.position = destPosition;
        GameObject.DestroyObject(positionIndicator);
        positionIndicator = null;
		turnOffAllClouds ();
		hintText.GetComponent<Text> ().text = TeleportTxts [teleportNumber-1];

        if (allowForRealHeadRotation)
        {
            Quaternion headRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.Head);
            Vector3 euler = headRotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;
            headRotation = Quaternion.Euler(euler);
            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Inverse(headRotation), realHeadMovementCompensation) * destRotation;
        }
        else
        {
            transform.rotation = destRotation;
        }

        yield return new WaitForSeconds(fadeLength);
        teleporting = false;

        while (fadeLevel > 0)
        {
            yield return null;
            fadeLevel -= fadeSpeed * Time.deltaTime;
            fadeLevel = Mathf.Clamp01(fadeLevel);
          //  OVRInspector.instance.fader.SetFadeLevel(fadeLevel);
        }
		//Debug.Log ("hello" + teleportNumber);

        yield return null;
    }

	public void turnOffAllClouds()
	{
		turnOffRainClouds ();
		turnOffSnowClouds ();
	}

	public void turnOffRainClouds()
	{
		if(rainParticles.activeSelf)
		{
			skyStuff.GetComponent<checkWeather> ().isRaining = false;
			rainParticles.SetActive (false);
		}

	}

	public void turnOffSnowClouds()
	{
		if (snowParticles.activeSelf) {
			skyStuff.GetComponent<checkWeather> ().isSnowing = false;
			snowParticles.SetActive (false);
		}
		fireParticles.SetActive(true);
		campFire.GetComponent<AudioSource> ().Play ();
		smokeParticles.SetActive(false);
	}

}
