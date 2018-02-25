using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed = 10000000000;
	public float damage;
	public float damage_lvl;
	public bool colide = false;
	public bool OptimalBullet = true;
	public Projectilecontrol controler;

	private float QueueNumber;

	public void SetSpeed (float NewSpeed){
		speed = NewSpeed;
	}


	// Use this for initialization
	void Start () {
		controler.AddProjectile(this);
	}



	public void destroy_self(){
	
		Destroy(this.gameObject);
	
	}

	public void addQueue(float quequeNo){
	
		QueueNumber = quequeNo;

	}


	void Update(){
	if(controler.Destroythis == 1){
		if(QueueNumber == 1){
			
			this.destroy_self();
			
		}
		else{
			
			QueueNumber -=1;
			
		}
	}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!colide) {
				//GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (Vector3.forward) * speed * Time.deltaTime);
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}else
			transform.Translate(Vector3.zero);




	}

	
	void OnCollisionEnter (Collision col)
	{

		colide = true;
		if(this.gameObject.layer != col.collider.gameObject.layer)
		{
			col.gameObject.GetComponent<Killable>().take_damage(damage*damage_lvl);

		}
		if (OptimalBullet){
			Destroy(this.gameObject);
		}
	}
	

}
