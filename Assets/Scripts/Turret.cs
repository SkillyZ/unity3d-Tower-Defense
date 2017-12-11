using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private List<GameObject> enemys = new List<GameObject>();

    public Transform head;

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

    public bool isLaser = false;
    public float damgeRate = 40f;

    public LineRenderer laserRenderer;

    public GameObject laserEffect;

    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {

        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        if (!isLaser)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else
        {
            if (enemys.Count > 0)
            {
                AttackLaser();
            } else
            {
                laserEffect.SetActive(false);
                laserRenderer.enabled = false;
            }
            
        }
    }

    void AttackLaser()
    {
        if (enemys[0] == null)
        {
            updateEnemy();
        }

        if (enemys.Count > 0)
        {
            if (laserRenderer.enabled == false) laserRenderer.enabled = true;
            laserEffect.SetActive(true);
            laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
            enemys[0].GetComponent<Enemys>().TakeDamage(damgeRate * Time.deltaTime);
            laserEffect.transform.position = enemys[0].transform.position;
            Vector3 pos = transform.position;
            pos.y = enemys[0].transform.position.y;
            laserEffect.transform.LookAt(pos);
        }
    }

    void Attack()
    {
        if (enemys[0] == null)
        {
            updateEnemy();
        }

        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
    }

    void updateEnemy()
    {
        //enemys.RemoveAll(null);
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                enemys.RemoveAt(i);
            }
        }
    }

}
