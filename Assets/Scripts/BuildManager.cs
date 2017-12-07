using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台
    private TurretData selectedTurretData;

    public int money = 1000;

    public Text moneyText;

    public Animator moneyFlicker;

    void ChangeMoney(int change = 0)
    {
        this.money += change;
        moneyText.text = "$" + this.money;
    }

    void Update()
    {
        //如果按下
        if (Input.GetMouseButtonDown(0))
        {
            //并且不在ui
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //射线碰撞
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //GameObject gameObject = hit.collider.gameObject;
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        //可以建造
                        if(money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                        else
                        {
                            moneyFlicker.SetTrigger("Flicker");
                        }
                    }
                    else
                    {
                        //升级
                        if (mapCube.turretGo != null)
                        {

                        }
                    }
                }
            }
        }
        
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            this.selectedTurretData = this.laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            this.selectedTurretData = this.missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            this.selectedTurretData = this.standardTurretData;
        }
    }
}
