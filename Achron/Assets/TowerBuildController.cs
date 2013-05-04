using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class TowerBuildController : MonoBehaviour {
	
	private class TowerBuildInstruction {
		
		public bool built;
		
		private Vector3 position;
		private float buildTime;
		private Object towerType;
	
		public TowerBuildInstruction (Vector3 newPosition, float newBuildTime, Object newTowerType)
		{
			position = newPosition;
			buildTime = newBuildTime;
			towerType = newTowerType;
			//print("new build instruciton with build time " + newBuildTime);
		}
		
		public void Build() {
			//print("rebuilding a tower");
			GameObject.Instantiate(towerType, position, Quaternion.identity);
			built = true;
		}
		
		public float GetBuildTime() {
			return(buildTime);
		}
	}
	
	private float startDelay;
	private bool started = false;
	private float time = 0;
	
	private float startTime;
	private List<TowerBuildInstruction> towerBuildQueue;
	
	// Use this for initialization
	void Start () {
		towerBuildQueue = new List<TowerBuildInstruction>();
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			return;
		}
		float time = Time.timeSinceLevelLoad - startDelay;
		towerBuildQueue.ForEach(delegate(TowerBuildInstruction instruction) {
			if (!instruction.built && time >= instruction.GetBuildTime()) {
				//print ("building new tower at actual time " + Time.timeSinceLevelLoad + "virtual time " + time);
				instruction.Build();
				//towerBuildQueue.Remove(instruction);
			}
		});
	}
	
	public void BeginBuilding() {
		started = true;
		startDelay = Time.timeSinceLevelLoad;
		// set each instruciton back to not built
		towerBuildQueue.ForEach(delegate(TowerBuildInstruction instruction) {
			instruction.built = false;
		});
	}
	
	// Store a tower build instruction
	public void StoreTowerBuild (Vector3 location, float time, Object towerType) {
		//print ("adding new tower build instruction to controller with start time " + startTime + " at build time " + time);
		if (towerBuildQueue == null) {
			Debug.LogError ("ERROR - towerBuildQueue is null");
		}
			
		towerBuildQueue.Add(new TowerBuildInstruction(location, time, towerType));
	}
	
	// set the start time
	public void SetStartTime(float newTime) {
		//print ("Set start time to " + newTime);
		startTime = newTime;
	}
	
	// 
	public bool IsStarted () {
		return(started);
	}
	
	//
	public float GetStartTime () {
		return(startTime);
	}
}
