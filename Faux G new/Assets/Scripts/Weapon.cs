using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public string typeofweapon;
	public float _damage;
	public GameObject bullet;
	public Transform gunpoint;
	public float shotSpeed;
	public float mspershot;
	float vPos;
	float NextShot;


	public void Attack(string tow){
				if (tow == "GUN") {
						if (Time.time > NextShot){
							NextShot = Time.time + mspershot / 1000;
							Projectile NewBullet =  Instantiate (bullet, gunpoint.position , gunpoint.rotation) as Projectile;
//							NewBullet.SetSpeed(shotSpeed);
							NewBullet.transform.SetParent (null);
							NewBullet.damage_lvl = _damage;
					
						}
				}
		}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (typeofweapon == "GUN") {
			vPos += Input.GetAxis("Mouse Y") * 250f * Time.deltaTime;
			vPos = Mathf.Clamp(vPos,-30,45);
			this.transform.localEulerAngles = Vector3.left * vPos;		
		}

	
	}
}
