using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {       
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishSpawner;
    public Transform bossSpawnPoint;

    public GameObject boss;

    public GameObject healthBar;

    public TextMeshProUGUI counterText;
    public TextMeshProUGUI waveNumber;

    private float timerValue;

    private bool isCount;
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
        timerValue = timeBetweenWaves;
        counterText.gameObject.SetActive(false);
    }
    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(1);
        waveNumber.gameObject.SetActive(true);
        int wave = index+1;
        waveNumber.text = "Wave " + wave.ToString();

        yield return new WaitForSeconds(2);
        waveNumber.gameObject.SetActive(false);
        isCount = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }
    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i=0;i<currentWave.count; i++)
        {
            if (player == null)
                yield break;

            Enemy randomenemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomenemy, randomSpot.position, randomSpot.rotation);

            if (i == currentWave.count - 1)
            {
                finishSpawner = true;
            }
            else
                finishSpawner = false;

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }
    void ResetCount()
    {
        counterText.gameObject.SetActive(false);
        isCount = false;
        timerValue = timeBetweenWaves;
    }
    void Count()
    {
        if (timerValue > 0)
        {
            timerValue -= Time.deltaTime;
            counterText.text = Mathf.Round(timerValue).ToString();
            if (timerValue <= 1)
            {
                Invoke("ResetCount", 0.5f);
            }
        }
    }
    private void Update()
    {
        if (isCount)
        {
            counterText.gameObject.SetActive(true);
            Invoke("Count", 0.5f);
        }        
        if (finishSpawner && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishSpawner = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                waveNumber.gameObject.SetActive(true);
                waveNumber.text = "Boss";
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                Invoke("ResetBoss", 2f);
                healthBar.SetActive(true);
            }
        }
    }
    void ResetBoss()
    {
        waveNumber.gameObject.SetActive(false);
    }
}
