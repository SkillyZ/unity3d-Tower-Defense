using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour {

    private Transform[] positions;
    public float speed = 10;
    private int index = 0;
    public int HP = 150;
    public GameObject explotionEffect;

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
        if (index > positions.Length - 1)
        {
            ReachDestition();
        }
    }

    void ReachDestition()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(int damage)
    {
        if (HP < 0) return;
        HP -= damage;
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
