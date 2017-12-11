﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Wave[] waves;
    public Transform START;
    public float waveRate = 0.2f;
    public static int CountEnemyAlive;
    private Coroutine coroutine;

    // Use this for initialization
    void Start () {
        coroutine = StartCoroutine(SpawnEnemy());
	}

    public void stop()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i!=wave.count-1) yield return new WaitForSeconds(wave.rate);

            }
            while (CountEnemyAlive > 0)
            {
                 yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }
}
