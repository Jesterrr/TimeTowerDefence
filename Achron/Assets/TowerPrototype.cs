using UnityEngine;
using System.Collections;


	
public class TowerPrototype : MonoBehaviour {
	
	private GameObject tower;
	public GameObject towerType;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		Object towerPrototype = Resources.Load("DragTower");
		if (towerPrototype == null) {
			Debug.Log("ERROR: prefab is null");
		}
		tower = (GameObject)Instantiate(towerPrototype);
		tower.GetComponent<DragTowerComponent>().SetTowerType(towerType);
		// show all collison spheres
		GameObject[] placementColliders;
        placementColliders = GameObject.FindGameObjectsWithTag("PlacementCollider");
		foreach (GameObject otherCollider in placementColliders) {
			otherCollider.GetComponent<PlacementColliderScript>().Show();
		}
	}
		
	
	void OnMouseUp () {
		
		tower.GetComponent<DragTowerComponent>().MouseUp();
		
		// hide all collison spheres
		GameObject[] placementColliders;
        placementColliders = GameObject.FindGameObjectsWithTag("PlacementCollider");
		foreach (GameObject otherCollider in placementColliders) {
			otherCollider.GetComponent<PlacementColliderScript>().Hide();
		}
	}
}