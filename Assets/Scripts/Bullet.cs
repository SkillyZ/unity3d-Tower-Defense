using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 50;
    public int speed = 40;

    private Transform target;

    public GameObject explosionEffectPrefab;

    public float distanceArriveTarget = 1.2f;
	
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

	// Update is called once per frame
	void Update ()
    {

        if (target == null)
        {
            Die();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)
        {
            target.GetComponent<Enemys>().TakeDamage(damage);
            Die();
        }
	}

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.tag);
    //    if (other.tag == "Enemy")
    //    {
    //        other.GetComponent<Enemys>().TakeDamage(damage);
    //        GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
    //        Destroy(this.gameObject);
    //    }
    //}

}
