using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {
	
	public Transform Hand_R;
	Weapon Equipped;
	string AttackType;
	public Weapon StartingWeapon;
	void Start(){
		equip(StartingWeapon);
	
	}

	public void equip(Weapon TargetWeapon){
		if (Equipped != null) {
			Destroy(Equipped.gameObject);
		}
		
		Equipped = Instantiate (TargetWeapon, Hand_R.position, Hand_R.rotation) as Weapon;
		Equipped.transform.parent = Hand_R;
		AttackType = Equipped.typeofweapon;
		
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			Equipped.Attack (AttackType);
			Equipped._damage = this.gameObject.GetComponent<PlayerController>().gun_level;
		}
		
	}
	void OnCollisionEnter (Collision col){
		if (col.gameObject.GetComponent<Weapon_Obj> () != null) {
			Debug.Log ("weapon get");
			equip(col.gameObject.GetComponent<Weapon_Obj>().weapon_);
			Destroy(col.gameObject);
				
		}
		
	}
}
