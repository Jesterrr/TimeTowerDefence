using UnityEngine;
using System.Collections;

public class ProjectileComponent : MonoBehaviour {
	
	private Vector3 target;
	private Vector3 origin;
	
	private float velocity;
	private float damage;
	
	private const float sqrMaxDistance = 300;
	
	// Use this for initialization
	void Start () {
		Vector3 direction = (target - this.transform.position).normalized;
		direction = direction * velocity;
		this.rigidbody.velocity = direction;
		origin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// delete projectiles that get too far away
		if ((origin - this.transform.position).sqrMagnitude > sqrMaxDistance) {
			Missed();
		}
	}
	
	void OnTriggerEnter(Collider other) {
		//print("collision");
		if (other.gameObject.tag == "Creep"	) {
			Hit(other.gameObject);
		}
	}
	
	void Hit (GameObject creep) {
		CreepComponent creepComponent = creep.GetComponent<CreepComponent>();
		creepComponent.Hit(damage);
		Destroy(this.gameObject);
	}
	
	void Missed() {
		Destroy (this.gameObject);
	}
	
	void TargetLost () {
		Destroy(this.gameObject);
	}
	
	// Set the projectile's target
	public void SetTarget (Vector3 newTarget) {
		target = newTarget;
	}
	
	// Set the damage value of the projectile
	public void SetDamage (float newDamage) {
		damage = newDamage;
	}
	
	// Set the damage value of the projectile
	public void SetVelocity (float newVelocity) {
		velocity = newVelocity;
	}
}
