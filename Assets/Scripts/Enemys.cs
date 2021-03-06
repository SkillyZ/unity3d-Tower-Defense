﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemys : MonoBehaviour {

    private Transform[] positions;
    public float speed = 10;
    private int index = 0;
    public float HP = 150;
    public GameObject explotionEffect;
    private Slider hpSlider;
    private float totalHp;
	// Use this for initialization
	void Start () {
        positions = Waypoints.positions;
        totalHp = HP;
        hpSlider = GetComponentInChildren<Slider>();
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
        if (index > positions.Length - 1)
        {
            ReachDestition();
        }
    }

    //到达终点
    void ReachDestition()
    {
        GameObject.Destroy(this.gameObject);
        GameManager.Instance.Failed();
    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        if (HP < 0) return;
        HP -= damage;
        hpSlider.value = (float)HP / totalHp;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject effect = GameObject.Instantiate(explotionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }

}
