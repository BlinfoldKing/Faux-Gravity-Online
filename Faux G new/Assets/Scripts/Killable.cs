using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	public float health;
	protected float stamina;
	public bool died = false;
	public GameObject DeathEffect;

	public void take_damage (float damage){
		if (health > 0 && !died) {
			health = health - damage;	

			if (health == 0 || damage > health || died){
				died = true;
				Destroy (Instantiate (DeathEffect, this.gameObject.transform.position, this.gameObject.transform.rotation), 0.5f);
				Destroy (this.gameObject);			
			}
		}
	
	}
}
	