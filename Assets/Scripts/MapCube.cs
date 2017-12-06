using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour {

    [HideInInspector]
    public GameObject turretGo;

    public GameObject buildEffect;

    public void BuildTurret(GameObject turretPrefab)
    {
        turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
        GameObject buildObject = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(buildObject, 1);
    }

}
