using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour {

	public string electronsPerShell;
	public string atom;
	public GameObject protonPrefab;
	public GameObject electronPrefab;

	List<Electron> electronList = new List<Electron>();

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	// Update the seekobjects rotation around this object
	void FixedUpdate () {
		if (electronList.Count > 0) {
			foreach (Electron e in electronList) {
				float angle = e.maxSpeed / Vector3.Distance (e.transform.position, e.GetSeekObject ().transform.position) * 3;
				e.GetSeekObject ().transform.RotateAround (transform.position, new Vector3 (0, 1, 0), angle);
			}
		}
	}

	//spawn the objects around the parent atom
	void Spawn(){
		string[] electrons = electronsPerShell.Split(new char[] {','});
	
		for (int i = 0; i < electrons.Length; i++) {
			//couldnt remember how to convert string to int, so i converted to float, then to int
			float nInShell;
			float.TryParse (electrons [i], out nInShell);
			int numberInShell  =(int)nInShell ;

			float shellDist = (i + 1) * protonPrefab.transform.localScale.x * 4;
			Vector3 firstPoint = transform.position + new Vector3(shellDist,0,0);

			for(int j= 0; j < numberInShell; j++){
				GameObject electron = Instantiate(electronPrefab, firstPoint, transform.rotation);
				electron.transform.RotateAround(transform.position, new Vector3(0,1,0), 360/numberInShell * j);

				electronList.Add (electron.GetComponent<Electron> ());
				electron.GetComponent<Electron> ().setSeekObject ();
			}
		}
	}
}
