using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour {
	Vector3 velocity;
	Vector3 acceleration;
	Vector3 force;
	Rigidbody rb;

	public GameObject parentAtom;

	public GameObject seekObject = null;//object that they seek towards
	public Vector3 seekpoint;

	public float maxSpeed = 20f;
	public bool wanderenabled = true;
	public bool seekenabled = true;

	Wander wander;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		wander = GetComponent<Wander> ();
	}
	

	void FixedUpdate () {
		force = Calculate ();
		Vector3 newacceleration = force / rb.mass;
		float smoothVal = 0.15f;
		acceleration = Vector3.Lerp (acceleration, newacceleration, smoothVal);
		velocity += acceleration / Time.deltaTime;
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);

		if (velocity.magnitude > float.Epsilon) {
			transform.forward = velocity;
		}

		transform.position += velocity * Time.deltaTime;

		SetTrailColours ();
	}

	Vector3 Calculate(){
		force = Vector3.zero;
		if (wanderenabled)
			force += wander.Jitter () * wander.weight;
		if (seekenabled)
			force += Seek (seekObject.transform.position);
		
		return force;
	}

	Vector3 Seek(Vector3 point){
		Vector3 desired = point - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		return desired - rb.velocity;
	}

	public GameObject GetSeekObject(){
		return seekObject;
	}

	public void setSeekObject(){
		seekObject = Instantiate(new GameObject(), transform.position, transform.rotation);
		seekObject.name = "SeekObject";
	}

	//set color relative to rotation
	void SetTrailColours(){
		Material m = GetComponent<TrailRenderer> ().material;
		Color c = new Color ();
		c.a = transform.rotation.x;
		c.b = transform.rotation.z;
		c.g = transform.rotation.y;
		c.r = transform.rotation.w;
		m.SetColor ("_Color", c);

	}
}
