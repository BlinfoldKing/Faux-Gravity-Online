using UnityEngine;
using System.Collections;

public class object_rotation : MonoBehaviour {

	public float speed;


	void FixedUpdate () {
		
		this.gameObject.transform.Rotate (Vector3.up * speed * Time.deltaTime);
		
		
	}
}
