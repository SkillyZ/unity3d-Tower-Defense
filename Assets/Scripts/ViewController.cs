using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    public float speed = 1;
    public float mouseSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float m = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(m);
        transform.Translate(new Vector3(h*speed, -m * mouseSpeed, v*speed) * Time.deltaTime, Space.World);

    }
}
