using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchNormal : MonoBehaviour {
	public float speed = 0.5f;
	private Vector3 castPos;
	private Vector3 proj;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update ()
	{
		//RaycastHit hit;
		//castPos.x = transform.position.x;
		//castPos.y = transform.position.y-.25f;
		//castPos.z = transform.position.z;
		//	if (Physics.Raycast (castPos, -transform.up, hit)) {
		//		transform.rotation = Quaternion.FromToRotation (Vector3.up, out hit.normal);
		//	}
	}
		
		/*fwd = transform.forward;
		transform.position += fwd * speed * Time.deltaTime;
		RaycastHit hit;
		// instead of -Vector3.up you could use -transform.up but as hit point will jump
		// when slope changes it will give jitter. That's solvable as well by working from
		// a pivot point in bottom centre of object instead of centre (and to make sure
		// your raycast won't be too low move start pos back by a bit using again
		// transform.up as direction.
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 10, 19)) {
			if (hit.distance > 1) {
				Vector3 temp = transform.localPosition;
				temp.y -= hit.distance - 1;
				transform.localPosition = temp;
			} else if (hit.distance < 1) {
				Vector3 temp1 = transform.localPosition;
				temp1.y += 1 - hit.distance;
				transform.localPosition = temp1;
			}
			proj = fwd - (Vector3.Dot (fwd, hit.normal)) * hit.normal;
			transform.rotation = Quaternion.LookRotation (proj, hit.normal);
		}*/
	
}
