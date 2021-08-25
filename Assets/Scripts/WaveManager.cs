using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    public float waveCooldown;
    [SerializeField]private bool isCooldown;
    [SerializeField]private float timeToNextWave;
    public GameObject enemyPrefab;
    public Transform[] enemySpawns;
    public int currentWave;
    public int[] enemyCount;
    public List<GameObject> enemiesAlive = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        timeToNextWave = waveCooldown;
        isCooldown = true;
        currentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooldown) timeToNextWave -= Time.deltaTime;
        if(enemiesAlive.Count == 0)
        {
            if (!isCooldown)
            {
                isCooldown = true;
                GameObject.Find("Canvas").GetComponent<UIManager>().lvlUpWindow.SetActive(true);
            }
            EndOfWave(); 
        }
    }

    private void SpawnWave(int waveNumber)
    {
        Debug.Log("Spawning wave: " + waveNumber.ToString());
        for (int i = 0; i < enemyCount[waveNumber]; i++)
        {
            Transform spawnLocation = enemySpawns[Random.Range(0, enemySpawns.Length)];
            Vector3 changedPos = spawnLocation.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject enemySpawned = Instantiate(enemyPrefab, changedPos, Quaternion.Euler(0,0,0));
            enemiesAlive.Add(enemySpawned);
        }
    }

    private void EndOfWave()
    {
        if(timeToNextWave <= 0)
        {
            timeToNextWave = waveCooldown;
            isCooldown = false;
        }
        if(!isCooldown)
        {
            if (currentWave != enemyCount.Length)
            {
                currentWave++;
                SpawnWave(currentWave);
            }
            else
            {
                //End of game logic goes here
            }
        }
    }
}
