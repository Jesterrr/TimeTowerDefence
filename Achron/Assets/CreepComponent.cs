using UnityEngine;
using System.Collections;

public class CreepComponent : MonoBehaviour {
	
	public float health = 40;
	public float speed = 5;
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler CreepDestroyed;
	
	// Use this for initialization
	void Start () {
		this.rigidbody.velocity = new Vector3(speed, 0, 0);
	}
	
	// Whenever a creep takes damage
	public void Hit (float damage) {
		health -= damage;
		if (health <= 0) {
			Death();
		}
	}
	
	// Destroy a creep
	void Death () {
		Destroy(this.gameObject);
		if (CreepDestroyed != null) {
			CreepDestroyed (this.gameObject);
		}
	}
}
