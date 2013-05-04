using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlacementColliderScript : MonoBehaviour {
	
	public Material okMat;
	public Material blockedMat;
	private float radius;
	
	private bool blocked = false;
	private bool hidden = false;
	private bool dragged = true;
	
	private List<GameObject> collidedWith;
	
	// Use this for initialization
	void Start () {
		collidedWith = new List<GameObject>();
		renderer.material = okMat;
	}
	
	// Update is called once per frame
	void Update () {
		if (collidedWith.Count > 0) {
			UnblockOthers();
		}		
		if (dragged) {
			
			bool hasCollided = false;
			GameObject[] placementColliders;
        	placementColliders = GameObject.FindGameObjectsWithTag("PlacementCollider");
			foreach (GameObject otherCollider in placementColliders) {
				if (otherCollider != this.gameObject) {
					Vector3 offset = this.transform.position - otherCollider.transform.position;
					PlacementColliderScript otherColliderScript = otherCollider.GetComponent<PlacementColliderScript>();
					float otherRadius = otherColliderScript.radius;
					//print (offset.magnitude);
					if (offset.magnitude < radius + otherRadius) {
						hasCollided = true;
						Blocked ();
						otherCollider.GetComponent<PlacementColliderScript>().Blocked ();
						collidedWith.Add(otherCollider);
					}
				}
			}
			if (!hasCollided) {
				Unblocked();
			}
		}
	}
	
	public void Show () {
		this.renderer.enabled = true;
		hidden = false;
	}
	
	public void Hide () {
		this.renderer.enabled = false;
		hidden = true;
	}
	
	public void Blocked () {
		//print("Blocked");
		this.renderer.material = blockedMat;
		blocked = true;
	}
	
	public void Unblocked () {
		this.renderer.material = okMat;
		blocked = false;
	}
	
	public void SetRadius(float radius) {
		this.radius = radius;
		this.transform.localScale = new Vector3(radius*2, radius*2, radius*2);
	}
	
	public bool IsBlocked() {
		return blocked;
	}
	
	public void StopDrag() {
		dragged = false;
		UnblockOthers();
	}
	
	private void UnblockOthers() {
		collidedWith.ForEach (delegate(GameObject otherCollider) {
				otherCollider.GetComponent<PlacementColliderScript>().Unblocked ();
			});
		collidedWith.Clear();
	}
	
	public void Delete(){
		UnblockOthers();
		Destroy(this.gameObject);
	}
		
}
