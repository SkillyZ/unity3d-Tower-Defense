using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台 UI
    private TurretData selectedTurretData;

    private MapCube selectedMapCube;

    public int money = 500;

    public Text moneyText;

    public Animator moneyFlicker;

    public GameObject upgradeCanvers;
    public Button upgradeButton;

    private Animator upgradeAnimatorCanvans;

    void ChangeMoney(int change = 0)
    {
        this.money += change;
        moneyText.text = "$" + this.money;
    }

    private void Start()
    {
        upgradeAnimatorCanvans = upgradeCanvers.GetComponent<Animator>();
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
                            mapCube.BuildTurret(selectedTurretData);
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
                            if (mapCube == selectedMapCube && upgradeCanvers.activeInHierarchy)
                            {
                                StartCoroutine(HideUpgradeUI());
                            }
                            else
                            {
                                ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraed);
                            }

                            selectedMapCube = mapCube;
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

    void ShowUpgradeUI(Vector3 pos ,bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI"); //HideUpgradeUI()
        upgradeCanvers.SetActive(false);
        Vector3 poss = new Vector3(pos.x, pos.y + 3.4f, pos.z);
        upgradeCanvers.SetActive(true);
        upgradeCanvers.transform.position = poss;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeAnimatorCanvans.SetTrigger("Hide");

        yield return new WaitForSeconds(0.5f);
        upgradeCanvers.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedTurretData.cost);
            selectedMapCube.UpgradTurret();
        }
        else
        {
            moneyFlicker.SetTrigger("Flicker");
        }
        StartCoroutine(HideUpgradeUI());
    }

    public void OnDestryButtonDown()
    {
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUpgradeUI());
    }

}
