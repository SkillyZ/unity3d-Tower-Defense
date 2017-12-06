using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public List<GameObject> enemys = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    public float attackRateTime = 1; //每秒攻击速率
    private float timer = 0;

    public GameObject bulletPrefab;
    public Transform firePosition;

    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer >= attackRateTime)
        {
            timer -= attackRateTime;
            Attack();
        }

    }

    void Attack()
    {
        GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
    }

}
