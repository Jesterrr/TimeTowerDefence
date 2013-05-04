using UnityEngine;

public class Stage {
		
	public float startTime;
	public bool started;
	
	public GameObject buildController;
	
	public Stage (float newStartTime) {
		startTime = newStartTime;
		started = false;		
	}
	
	public void Start () {
		started = true;	
		// enable the build controller
		buildController.GetComponent<TowerBuildController>().BeginBuilding();
		// set the start time
		startTime = Time.timeSinceLevelLoad;
	}
	
	public GameObject GetBuildController () {
		return buildController;
	}
	
	public string GetText() {
		return("Game Stage. Started = " + started + " start time = " + startTime);
	}
}
