using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Serialization;
using System;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class blobSystem : MonoBehaviour
{

    bool previousState;
    bool currentState;
    bool moving;
    float movement = -40;
    private float speed;

    [Serializable]
    public class BlobClicksEvent : UnityEvent<bool> { }

    [SerializeField]
    private BlobClicksEvent onClick = new BlobClicksEvent();

    public bool PreviousState
    {
        get
        {
            return previousState;
        }

        set
        {
            previousState = value;
        }
    }

    public bool CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }
    // Use this for initialization
    void Start()
    {
        transform.localRotation = Quaternion.Euler(movement, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            float initialSign = Mathf.Sign(movement);
            movement += Time.deltaTime * (PreviousState ? -1 : 1) * speed;

            transform.rotation = Quaternion.Euler(new Vector3(movement, 0, 0));
            if ((movement > 40f && !PreviousState) || (movement < -40 && PreviousState))
            {
                CurrentState = !PreviousState;
                moving = false;
            }
            if (Mathf.Sign(movement) != initialSign)
            {
                onClick.Invoke(!PreviousState);
            }

            transform.localRotation = Quaternion.Euler(movement, 0, 0);
        }

    }

    public void OnPointerClick()
    {
        if (!moving)
        {
            PreviousState = CurrentState;
            moving = true;
        }
    }
}