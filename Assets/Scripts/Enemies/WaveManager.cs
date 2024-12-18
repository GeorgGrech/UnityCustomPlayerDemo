using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager _instance;

    [SerializeField] private GameObject flyingEnemyPrefab;
    //[SerializeField] private GameObject groundEnemyPrefab;

    [SerializeField] private Transform[] flyingSpawnPoints;
    //[SerializeField] private Transform[] groundSpawnPoints;

    [SerializeField] private float spawnDelay = 2; //Delay between each enemy in wave

    [SerializeField] private int initialWaitMin; //Wait before start of first wave
    [SerializeField] private int initialWaitMax;

    [Space(10)]

    [SerializeField] private int endWaveWaitMin; //Wait between waves
    [SerializeField] private int endWaveWaitMax;

    [Space(10)]

    [SerializeField] private int initialFlyingEnemyAmount; //Only initial amounts. Rest will be generated procedurally 
    [SerializeField] private int enemyIncrease; //Only initial amounts. Rest will be generated procedurally 
    //[SerializeField] private int initialGroundEnemyAmount; //Only initial amounts. Rest will be generated procedurally 
    //[SerializeField] private int initialWavesBeforeTown;

    //[SerializeField] private int townTimer;

    private BoidManager boidManager;

    [Space(10)]

    public int leftInWave; //To be decremented from enemies
    public List<Boid> boidsInScene;

    [Space(10)]
    public int waveNo;
    [SerializeField] TextMeshProUGUI waveNoText;
    [SerializeField] TextMeshProUGUI leftInWaveText;
    [SerializeField] TextMeshProUGUI nextWaveMessage;
    [Space(10)]
    int totalEnemiesKilled; //Total Enemies Killed
    [SerializeField] GameObject[] objectsToShow;
    [SerializeField] TextMeshProUGUI wavesSurvivedText;
    [SerializeField] TextMeshProUGUI enemiesKilledText;

    private Transform lastSpawnPoint;

    //private List<GameObject> enemies; //Spawned enemies

    private void Awake()
    {
        Time.timeScale = 1f; //Reset timescale in case coming from game over
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        boidManager = FindObjectOfType<BoidManager>();
        //boidManager.waveManager = this;
        StartCoroutine(WaveCycle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaveCycle()
    {
        //Initial wait
        int wait = Random.Range(initialWaitMin, initialWaitMax);
        Debug.Log("First wave starting in " + wait + " seconds");
        StartCoroutine(ShowNextWaveMessage(wait));
        yield return new WaitForSeconds(wait);

        int flyingInWave = initialFlyingEnemyAmount;

        waveNo = 1;

        //int leftInWave = initialFlyingEnemyAmount+initialGroundEnemyAmount;
        while (true)
        {
            Debug.Log("flyingInWave; " + flyingInWave);
            leftInWave = flyingInWave /* groundInWave*/;
            UpdateUI();

            for (int j = 0; j < flyingInWave; j++)
            {
                GameObject enemy = SpawnEnemyAtPoint(flyingEnemyPrefab, flyingSpawnPoints);


                Boid b = enemy.GetComponent<Boid>();
                boidManager.InitialiseBoid(b);
                boidsInScene.Add(b);
                boidManager.UpdateBoidList(boidsInScene);

                yield return new WaitForSeconds(spawnDelay);
            }

            Debug.Log("All enemies in wave spawned. Waiting...");
            while (leftInWave > 0) //While still enemies left in wave, wait
            {
                yield return null;
            }

            wait = Random.Range(endWaveWaitMin, endWaveWaitMax);
            Debug.Log("Next wave starting in " + wait + " seconds");
            StartCoroutine(ShowNextWaveMessage(wait));
            yield return new WaitForSeconds(wait);

            flyingInWave += enemyIncrease;
            waveNo++;
        }
    }

    GameObject SpawnEnemyAtPoint(GameObject enemyPrefab, Transform[] spawns)
    {
        Transform selectedSpawn;

        if (spawns.Length > 1)
        {
            do
            {
                selectedSpawn = spawns[Random.Range(0, spawns.Length)]; //Select random spawn

            } while (selectedSpawn == lastSpawnPoint);
        }
        else selectedSpawn = spawns[0]; //If just one spawn point, don't bother checking duplicate point

        GameObject enemy = Instantiate(enemyPrefab, selectedSpawn.position, Quaternion.identity); //Spawn enemy.

        lastSpawnPoint = selectedSpawn;
        enemy.GetComponent<DemoEnemy>().waveManager = this; //Make sure enemy can access this script

        return enemy;
    }

    public void RemoveBoidFromList(Boid b) //Technically, it seems to work without removing dead boids from the boids list, but for safety
    {
        boidsInScene.Remove(b);
        boidManager.UpdateBoidList(boidsInScene);
    }

    public void OnEnemyKilled()
    {
        leftInWave--;
        totalEnemiesKilled++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        waveNoText.SetText(waveNo.ToString());
        leftInWaveText.SetText(leftInWave.ToString());
    }

    private IEnumerator ShowNextWaveMessage(int time)
    {
        yield return new WaitForSeconds(1); //Grace time
        nextWaveMessage.SetText("Next Wave starting in "+time.ToString()+" seconds...");
        nextWaveMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        nextWaveMessage.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverPanel());
    }

    private IEnumerator ShowGameOverPanel()
    {
        Time.timeScale = 0;
        FindObjectOfType<AudioListener>().enabled = false; //Disable audio
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int survivedWaves = waveNo - 1;
        if (survivedWaves == 1)
            wavesSurvivedText.SetText(survivedWaves.ToString() + "\nWAVE");
        else
            wavesSurvivedText.SetText(survivedWaves.ToString() + "\nWAVES");

        if(totalEnemiesKilled == 1)
            enemiesKilledText.SetText(totalEnemiesKilled.ToString() + "\nENEMY");
        else
            enemiesKilledText.SetText(totalEnemiesKilled.ToString() + "\nENEMIES");

        foreach (GameObject uiObject in objectsToShow)
        {
            uiObject.SetActive(true);
            yield return new WaitForSecondsRealtime(2);
        }
    }
}