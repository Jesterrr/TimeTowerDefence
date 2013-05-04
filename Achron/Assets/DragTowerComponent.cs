using UnityEngine;
using System.Collections;

public class DragTowerComponent : MonoBehaviour {
	public RaycastHit hit;
	
	
	private GameObject towerType;
	private GameObject stageController;	
	private GameObject placementCollider;
	
	private GameObject placementColliderResource = Resources.Load("PlacementCollider") as GameObject;
	
	// Use this for initialization
	void Start () {
		stageController = (GameObject)GameObject.Find("GameStageController");
		float collisionRadius = this.towerType.GetComponent<TowerComponent>().collisionRadius;
		placementCollider = (GameObject)Instantiate(placementColliderResource, this.transform.position, Quaternion.identity);
		placementCollider.GetComponent<PlacementColliderScript>().SetRadius(collisionRadius);
	}
	
	// Update is called once per frame
	void Update () {
		MoveToMouse();
		placementCollider.transform.position = this.transform.position;
	}
	
	// Move to mouse
	void MoveToMouse () {
		int layermask = 1 << 9;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100, layermask)) {
            this.transform.position = hit.point;
        }
		
	}
	
	public void MouseUp () {
		Object towerPrototype = towerType;
		if (towerPrototype == null) {
			print("ERROR: prefab is null");
		}
		if (!this.placementCollider.GetComponent<PlacementColliderScript>().IsBlocked()) {
			GameObject tower = (GameObject)Instantiate(towerPrototype, this.transform.position, Quaternion.identity);
			TowerBuildController nextStage = stageController.GetComponent<GameStageController>().GetNextStage();
			TowerBuildController currentStage = stageController.GetComponent<GameStageController>().GetCurrentStage();
			float currentStageStartTime = currentStage.GetComponent<TowerBuildController>().GetStartTime();
			float buildTime = Time.timeSinceLevelLoad - currentStageStartTime;
			nextStage.GetComponent<TowerBuildController>().StoreTowerBuild(this.transform.position, buildTime, towerPrototype);
			this.placementCollider.GetComponent<PlacementColliderScript>().Hide();
			this.placementCollider.GetComponent<PlacementColliderScript>().StopDrag();
		}
		else {
			this.placementCollider.GetComponent<PlacementColliderScript>().Delete();
		}
		Destroy (placementCollider);
		Destroy (this.gameObject);
		
	}
	
	public void SetTowerType (GameObject newTowerType) {
		towerType = newTowerType;
	}
}
