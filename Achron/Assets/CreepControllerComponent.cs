using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepControllerComponent : MonoBehaviour {
	
	public float spawnDelay = 0.5f;
	private float timeUntilSpawn = 0;
	private List<Transform> creeps;
		
	// Use this for initialization
	void Start () {
		creeps = new List<Transform>();
		//CreateCreep("Creep", new Vector3(0,0,-20));
		//CreateCreep("Creep", new Vector3(-10,0,-20));
		//CreateCreep("Creep", new Vector3(10,0,-20));
	}
	
	public void CreateCreep (string resource, Vector3 position) {
		GameObject creepPrefab = (GameObject)Resources.Load(resource);
		GameObject newCreep = (GameObject)Instantiate(creepPrefab, position, Quaternion.identity);
		newCreep.transform.parent = this.transform;
		creeps.Add(newCreep.transform);
		newCreep.GetComponent<CreepComponent>().CreepDestroyed += CreepDestroyedListener;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilSpawn <= 0) {
			CreateCreep("Creep", new Vector3(-10,0,-20));
			timeUntilSpawn = spawnDelay;
		}
		else {
			timeUntilSpawn -= Time.deltaTime;
		}
	}
	
	// runs whenever the projectile's target creep is destroyed
	void CreepDestroyedListener(GameObject creep) {
		creeps.Remove(creep.transform);
	}
	
}
