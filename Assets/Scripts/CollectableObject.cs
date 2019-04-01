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
using System.Collections;

public class CollectableObject : MonoBehaviour {
	
    private float lastLookAtTime = 0;
	public bool isHeld = false;
	public bool isInInv = false;
	public string theItemName;
	public string theItemUsedName = "none";
	public bool canBeConsumed = false;
	public string ConsumeTriggerAction = "none";
	public bool canBeUsed = false;
	public bool isUsed = false;
	public string[] TagsToUseWith;
	public string[] TagsToUseUsedWith;
	public string TriggerAction = "none"; //action triggered by use - disappear/change to full/
	public string UsedTriggerAction = "none";//action triggered by using used - both combine with tag found, ie fire/plant different action
	public Sprite itemImage;
	public Sprite itemUsedImage;
	public int itemNumber;
	public string useButtonText = "Use";
	public string usedUseButtonText = "Use";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnLookAt()
    {
        lastLookAtTime = Time.time;
    }
}
