using UnityEngine;
using System.Collections;

public class Sphe_Cart : MonoBehaviour {

	public void SphericalToCartesian(float radius, float polar, float elevation, out Vector3 outCart){
		float a = radius * Mathf.Cos(elevation);
		outCart.x = a * Mathf.Cos(polar);
		outCart.y = radius * Mathf.Sin(elevation);
		outCart.z = a * Mathf.Sin(polar);
	}

	public void CartesianToSpherical(Vector3 cartCoords, out float outRadius, out float outPolar, out float outElevation){

		outRadius = Mathf.Sqrt((cartCoords.x * cartCoords.x)
		                       + (cartCoords.y * cartCoords.y)
		                       + (cartCoords.z * cartCoords.z));
		outPolar = Mathf.Atan2(cartCoords.z , cartCoords.x);
		if (cartCoords.x < 0)
			outPolar += Mathf.PI;
		outElevation = Mathf.Asin(cartCoords.y / outRadius);
	}


}
