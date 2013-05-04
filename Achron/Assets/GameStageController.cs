using UnityEngine;
using System.Collections;

public class GameStageController : MonoBehaviour {
	
	public int nStages = 10;
	public float stageLength = 10;
	
	private int currentStage = -1;
	private TowerBuildController[] stages;
	
	// Use this for initialization
	void Start () {
		Object buildControllerPrototype = Resources.Load("TowerBuildController");
		stages = new TowerBuildController[nStages];
		for (int i = 0; i < nStages; i++) {
			GameObject stageObject = (GameObject)Instantiate(buildControllerPrototype);
			TowerBuildController stage = stageObject.GetComponent<TowerBuildController>();
			stages[i] = stage;
			stage.SetStartTime(stageLength * i);
		}		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < nStages; i++) {
			TowerBuildController stage = stages[i];
			//print (stage.started + " " + stage.startTime);
			if (!stage.IsStarted () && Time.timeSinceLevelLoad >= stage.GetStartTime()) {
				// start this stage
				//print ("Starting next stage");
				currentStage++;
				stage.BeginBuilding ();
				// start all of the stages
				for (int j = 0; j < i; j++) {
					TowerBuildController oldStage = stages[j];
					oldStage.BeginBuilding();
				}
				// now delete any towers in the scene
				GameObject[] towers;
				towers = GameObject.FindGameObjectsWithTag ("Tower");
				foreach (GameObject tower in towers) {
					tower.GetComponent<TowerComponent>().Delete();
				}
			}
		}

	}
	
	public TowerBuildController GetCurrentStage () {
		TowerBuildController stage = stages[currentStage];
		if (stage == null) {
			Debug.LogError("Current stage is null");
		}
		return(stage);
	}
	
	public TowerBuildController GetNextStage () {
		TowerBuildController stage = stages[currentStage + 1];
		if (stage == null) {
			Debug.LogError("Current stage is null");
		}
		return(stage);
	}
}
