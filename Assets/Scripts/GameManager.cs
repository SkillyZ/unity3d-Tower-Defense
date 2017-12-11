using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject endMenu;
    public Text endText;

    public static GameManager Instance;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        endMenu.SetActive(true);
        endText.text = "胜 利";
    }

    public void Failed()
    {
        enemySpawner.stop();
        endMenu.SetActive(true);
        endText.text = "失 败";
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onButtonMenu()
    {
        SceneManager.LoadScene(0);
    }

}
