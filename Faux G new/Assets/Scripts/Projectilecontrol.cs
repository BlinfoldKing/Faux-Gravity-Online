using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectilecontrol : MonoBehaviour {
	
	public const int maxBullet = 5;

	private int bullet0 ;
	public float Destroythis = 0 ;

	float Count = 0;

	// Use this for initialization
	void Start () {
	
	}


	
	// Update is called once per frame
	void Update () {



		if(Count > maxBullet){
			Destroythis = 1;
			Count -= 1;
		}else{
			Destroythis = 0;
		}
	}

	public void AddProjectile(Projectile projectile){
			Count+=1;
			projectile.addQueue(Count);

	}

}
