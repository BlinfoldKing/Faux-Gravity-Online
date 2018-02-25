using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player_Network : NetworkBehaviour {

	[SyncVar(hook = "SyncPosValue")] Vector3 syncPos;
	[SyncVar] Quaternion syncRot;
	[SyncVar] Quaternion syncCamRot;
	
	public PlayerController player;
	public Camera cam;
	public float NormalLerp = 18;
	public float FasterLerp = 36;
	public bool useHistoricalLerp = false;
	public float AlmostPos = 0.5f;

	float LerpRate;
	Vector3 LastPosisition;
	Quaternion CamLastRot;
	Quaternion PlayerLastRot;
	Transform myTransform;

	List<Vector3> syncPostList = new List<Vector3>();

	// Use this for initialization
	void Start () {
		player = this.gameObject.GetComponent<PlayerController>();
		cam = this.gameObject.GetComponentInChildren<Camera>();
		myTransform = this.gameObject.transform;
		if (isLocalPlayer){
			player.enabled = true;
			cam.enabled = true;
			player.gameObject.GetComponent<FauxGravityBody>().enabled = true;
			player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}else{
			player.gameObject.GetComponent<FauxGravityBody>().enabled = false;
		
		}
	}
	
	// Update is called once per frame
	void Update () {
			TransmitPosisition();
			TransmitRotation();
			LerpPosition();
			LerpRotation();

			
		}
	void LerpPosition(){

		if(!isLocalPlayer){
			if(useHistoricalLerp){
				HistoricalLerping();
			}else{
				OrdinaryLerping();
			}
		}
	}


	void LerpRotation(){
		if(!isLocalPlayer){
			myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation,syncRot,60*Time.deltaTime);	
			cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation,syncCamRot,60*Time.deltaTime);	

		}
	
	}

	[Command] void CmdProvideRotationToServer(Quaternion PlayerRot,Quaternion CamRot){
		syncRot = PlayerRot;
		syncCamRot = CamRot;
	
	}
	[Command] void CmdProvidePositionToServer(Vector3 pos){
		syncPos = pos;
	}

	[ClientCallback] void TransmitPosisition(){
		if(isLocalPlayer){
			CmdProvidePositionToServer(myTransform.position);
			LastPosisition = myTransform.position;
		}
	}
	[ClientCallback] void TransmitRotation(){
		if(isLocalPlayer){
			CmdProvideRotationToServer(myTransform.rotation,cam.transform.rotation);
			CamLastRot = cam.transform.rotation;
			PlayerLastRot = myTransform.rotation;
		}
	}
	[Client] void SyncPosValue(Vector3 LatestPost){
		syncPos = LatestPost;
		syncPostList.Add(syncPos);
	
	}
	void OrdinaryLerping(){
		myTransform.position = Vector3.MoveTowards(myTransform.position,syncPos,Time.deltaTime * LerpRate);

	}

	void HistoricalLerping(){
		if(syncPostList.Count > 0){
			myTransform.position = Vector3.MoveTowards(myTransform.position,syncPostList[0],Time.deltaTime * LerpRate);

			if(Vector3.Distance(myTransform.position,syncPostList[0]) <= AlmostPos ){
				syncPostList.RemoveAt(0);
			}
		if(syncPostList.Count > 10){
				LerpRate = FasterLerp;
			}else{
				LerpRate = NormalLerp;
			}
		
		}
	
	}
	

}
