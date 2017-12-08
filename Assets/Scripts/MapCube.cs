using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;
    [HideInInspector]
    public bool isUpgraed = false;

    public GameObject buildEffect;

    private new Renderer renderer;

    private TurretData turretData;

    private void Start()
    {
        this.renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraed = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject buildObject = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(buildObject, 1);
    }

    private void OnMouseEnter()
    {
        if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (renderer.material.color != Color.white)
        {
            renderer.material.color = Color.white;
        }
    }

    public void UpgradTurret()
    {
        if (isUpgraed == true) return;
        isUpgraed = true;
        Destroy(turretGo);
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject buildObject = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(buildObject, 1);
    }

    public void DestoryTurret()
    {
        Destroy(turretGo);
        isUpgraed = false;
        turretData = null;
        turretGo = null;
        GameObject buildObject = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(buildObject, 1);

    }

}
