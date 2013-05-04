using UnityEngine;
using System.Collections;

public class TowerComponent : MonoBehaviour {
	
	public float fireDelay = 3;
	public float damage = 5;
	public float velocity = 10;
	public float blastRadius;
	public float snarePercent;
	public float snareDuration;
	public float cost;
	public bool homing;
	public float collisionRadius;
	
	public GameObject projectilePrefab;
	
	private float reloadTime = 0;
	private CreepControllerComponent creepController;
	
	// Use this for initialization
	void Start () {
		creepController = GameObject.Find("Creeps").GetComponent("CreepControllerComponent") as CreepControllerComponent;
		//projectilePrefab = (GameObject)Resources.Load(projectileType);
	}
	
	// Update is called once per frame
	void Update () {
		if (reloadTime <= 0) {
			Fire ();
			reloadTime = fireDelay;
			return;
		}
		reloadTime -= Time.deltaTime;
	}
	
	// Fires a shot
	void Fire () {
		//print ("firing");
		Transform target = FindTarget();
		GameObject projectile = (GameObject)Instantiate(projectilePrefab);
		ProjectileComponent projectileComponent = projectile.GetComponent("ProjectileComponent") as ProjectileComponent;
		projectileComponent.SetTarget(target.position);
		projectileComponent.SetDamage(damage);
		projectileComponent.SetVelocity(velocity);
		projectile.transform.position = this.transform.position;
	}
	
	// Find a target to hit
	Transform FindTarget () {
		//Transform target = creepController.GetNearestCreep(this.transform.position);		
		//return target;
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag ("Creep");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject target in targets) {
			Vector3 diff = target.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = target;
				distance = curDistance;
			}
		}
		return closest.transform;
	}
	
	// Works out the correct position to aim at to hit a target
	Vector3 CalculateTargetCoord (Transform creep) {
		Vector3 target = creep.position;
		Vector3 position = this.transform.position;
		return(target);
	}
	
	public void Delete() {
		Destroy(this.gameObject);
	}
}
