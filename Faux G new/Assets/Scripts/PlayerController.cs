using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float moveSpeed = 20;
	private Vector3 moveDirection;

	public float runspeed = 3;
	public float MaxFuel = 100;
	public float MaxUpForce = 10;
	public float UpForce = 0;
	public GameObject rocketFX;
	public Transform Rocket_Launcher1;
	public Transform Rocket_Launcher2;
	public GameObject Jetpack;
	public float gun_level = 1;
	public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;

	
	float verticalLookRotation;
	Transform cameraTransform;


	float Jetpack_level = 1;
	bool grounded = false;
	float _Fuel ;
	Jetpack jetpack_;
	GameObject NEWrocketFX1;
	GameObject NEWrocketFX2;
	RaycastHit hit;


	void Start(){
		
		//Screen.lockCursor = true;
		cameraTransform = this.gameObject.GetComponentInChildren<Camera>().transform;

		_Fuel = MaxFuel;
	}



	void Update() {
		moveDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-30,45);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

	
	
	}

		


	void FixedUpdate() {

		if (this.gameObject.GetComponent<FauxGravityBody> () != null) {
						this.gameObject.GetComponent<Rigidbody> ().isKinematic = false;		
				} else {
						this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
				}
				
		MaxFuel *= Jetpack_level;
			
			
				if (Input.GetKey (KeyCode.LeftShift)) {
						
						if (_Fuel != 0) {
								_Fuel -= 1;
									
								moveDirection = new Vector3 (moveDirection.x * (Input.GetAxisRaw ("Horizontal") * UpForce), moveDirection.y + UpForce, moveDirection.z * (Input.GetAxisRaw ("Vertical") * UpForce));
								//this.gameObject.GetComponent<Rigidbody> ().AddRelativeForce (moveDirection.x, UpForce, moveDirection.z);
								if (UpForce < MaxUpForce) {
										UpForce += 0.1f;
								}
								if (NEWrocketFX1 == null && NEWrocketFX2 == null) {
										NEWrocketFX1 = Instantiate (rocketFX, Rocket_Launcher1.position, Rocket_Launcher1.rotation) as GameObject;
										NEWrocketFX2 = Instantiate (rocketFX, Rocket_Launcher2.position, Rocket_Launcher2.rotation) as GameObject;

										NEWrocketFX1.transform.parent = Jetpack.transform;
										NEWrocketFX2.transform.parent = Jetpack.transform;
								}
						}

				}

				if (Input.GetKey (KeyCode.LeftControl) && !grounded) {
						UpForce += 0.1f;
						moveDirection = new Vector3 (moveDirection.x + Input.GetAxisRaw ("Horizontal"), moveDirection.y - UpForce, moveDirection.z + Input.GetAxisRaw ("Vertical"));
				
				}

				if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.LeftControl) || _Fuel == 0) {
						UpForce = 0;
						moveDirection.y *= UpForce;
						Destroy (NEWrocketFX1);
						Destroy (NEWrocketFX2);
						
					
				}														
				GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (moveDirection) * moveSpeed * Time.deltaTime);
			
		}

	void OnCollisionEnter (Collision col){
				if (col.gameObject.GetComponent<Fuel> () != null) {
					_Fuel += col.gameObject.GetComponent<Fuel> ().refill_fuel_;
				if(_Fuel > MaxFuel){
					_Fuel = MaxFuel;
				}
						Destroy(col.gameObject);
				}

	}

	void OnCollisionEnter(Collider col){
				if (col.gameObject.tag == "Gravity") {
						grounded = true;		
				}
		}
	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "Gravity") {
				grounded = false;		
		}
	
	}
		
	
}