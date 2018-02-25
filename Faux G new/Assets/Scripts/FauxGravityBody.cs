using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttractor attractor;	
	Transform myTransform;
	List<GameObject>  _attractor = new List<GameObject>();

	public bool useMultiAttractor = false;
	public FauxGravityAttractor starting_attractor;

	void Start () {
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

		myTransform = transform;
		if(useMultiAttractor)
			_attractor.Add(starting_attractor.gameObject);
		else
			attractor = starting_attractor;


	}

	void Update () {
		  
		if (useMultiAttractor){
			for(int i = 1 ; i < _attractor.Count ; i++){
				_attractor[i-1].GetComponent<FauxGravityAttractor>().Attract(myTransform);	
			
			}

		}else
			attractor.Attract(myTransform);
	
	}


	void OnCollisionEnter(Collision col){
		if(col.gameObject.GetComponent<FauxGravityAttractor>() != null)
				transform.SetParent (col.gameObject.transform);
		
	}

	void OnCollisionExit(Collision col){
		if(col.gameObject.GetComponent<FauxGravityAttractor>() != null)
			transform.SetParent (null);
	}

	void OnTriggerEnter(Collider collision){
		if (collision.GetComponent<Collider> ().GetComponent<FauxGravityAttractor> () != null) {
			if(attractor != null){
				attractor =null;
			}
			if(transform.parent != null){
				transform.parent = null;
			}
			if(useMultiAttractor)
				_attractor.Add(collision.GetComponent<Collider> ().gameObject);
			else
				attractor = collision.GetComponent<Collider> ().GetComponent<FauxGravityAttractor> ();
				
	
		}
	}
	
	void OnTriggerExit(Collider collision){
		if (collision.GetComponent<Collider> ().GetComponent<FauxGravityAttractor> () != null) {
			_attractor.Remove((collision.GetComponent<Collider> ().gameObject));
				

			
		}
	}
	
}
