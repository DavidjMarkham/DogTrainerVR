using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        transform.position = new Vector3(transform.position.x - (.025f * Time.deltaTime), 5.496487f, transform.position.z + (.1f * Time.deltaTime));
        
	}
}
