﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour {

    private Transform[] positions;
    public float speed = 10;
    private int index = 0;

	// Use this for initialization
	void Start () {
        positions = Waypoints.positions;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
    }
}
