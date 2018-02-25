using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor (typeof(FoW))]
public class FowEditor : Editor {


	void OnSceneGUI(){
		FoW fow = (FoW) target;
		Handles.color = Color.blue;

		Handles.DrawWireArc(fow.transform.position,Vector3.up,Vector3.forward,360,fow.ViewRadius);
		Vector3 AngleA = fow.AngleToDir(-fow.ViewAngle/2,false);
		Vector3 AngleB = fow.AngleToDir(fow.ViewAngle/2,false);

		Handles.DrawLine(fow.transform.position,fow.transform.position + AngleA * fow.ViewRadius);
		Handles.DrawLine(fow.transform.position,fow.transform.position + AngleB * fow.ViewRadius);
	
		Handles.color = Color.red;
		foreach(Transform everytarget in fow.Targets){
			Handles.DrawLine(fow.transform.position,everytarget.position);
		}

	}


}
