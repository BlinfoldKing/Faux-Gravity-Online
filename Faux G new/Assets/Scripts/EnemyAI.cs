using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

	public Transform playerTransform;
	Vector3 destination;
	Ray  playerdistance;
	RaycastHit Rhit;
	Vector3 direction;


	// Use this for initialization
	void Start () {



	}


	// Update is called once per frame
	void Update () {
		Ray waypoint = new Ray(Vector3.zero , Vector3.forward);
		if(Physics.Raycast(waypoint,Mathf.Infinity)){

			direction = waypoint.GetPoint(1);
		
		}


			GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (direction) * 10 * Time.deltaTime);



	}

}
