using UnityEngine;
using System.Collections;

public class FauxGravityAttractor : MonoBehaviour {

	float gravity = -12f;
	public bool useDynamicGravityField = false;
	const float G = 120;



	public void FixedUpdated(){
	
	}

	public void Attract(Transform body) {
		float r = Vector3.Distance(Vector3.zero,body.position);
		if(useDynamicGravityField){
			gravity = -((this.gameObject.GetComponent<Rigidbody>().mass * G)/(r*r));
		}else{
			gravity = -12f;
		}
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.up;
		Quaternion targetRotation = Quaternion.FromToRotation (localUp, gravityUp) * body.rotation;

		body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity * (body.GetComponent<Rigidbody>().mass));


		if (body.gameObject.GetComponent<Projectile>() != null) {
				body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 100 * Time.deltaTime);
		} else {
				body.rotation = Quaternion.RotateTowards (body.rotation, targetRotation, 60 * Time.deltaTime);
		}

	}   

}
