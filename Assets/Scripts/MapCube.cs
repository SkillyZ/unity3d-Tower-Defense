using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {

    [HideInInspector]
    public GameObject turretGo;

    public GameObject buildEffect;

    private Renderer renderer;

    private void Start()
    {
        this.renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(GameObject turretPrefab)
    {
        turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
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

}
