using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {

	Electron electron;
	public float jitter = 1.0f;
	public float sphereRad = 10f;
	public float sphereDistance = 10f;
	public float weight = 0.2f;
	Vector3 target = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public Vector3 Jitter () {
		target = Vector3.zero;
		float jitterTime = jitter * Time.deltaTime;
		Vector3 toAdd = jitterTime * Random.insideUnitSphere;
		target += toAdd;
		target.Normalize();
		target *= sphereRad;

		Vector3 localSpace = target + ( sphereDistance * transform.forward);
		Vector3 worldSpace = transform.TransformPoint (localSpace);

		return worldSpace - transform.position;
	}
}
