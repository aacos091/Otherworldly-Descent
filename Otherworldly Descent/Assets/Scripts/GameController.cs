using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Object spawning
    public OrbSpawner orbSpawner;
    public RuneSpawner runeSpawner;
    public bool canSpawnObjects = true;

    //Player information
    public PlayerMove playerScript;
    public PlayerLook cameraScript;
    public int startHealth = 5;
    public GameObject playerHealthUI;

    //Pause menu stuff
    public GameObject pauseMenu;
    private bool notPaused = true;

    //Victory stuff
    public bool playerMustExit;

    //Other scripts
    public ScoreController scoreController;

    //Start game stuff
    public GameObject entranceBlock;
    public Light mainLight;
    public GameObject islandCollider;
    public GameObject templeStartPoint;


    //Spawn point stuff
    public Transform[] spawnPoints;

    public GameObject playerPosition;
    private float distance;

    public GameObject darkMonster;
    DarkMonsterController darkMonsterController;


    public bool canPause = true;
    private bool playerLost;

    //Spawn stuff
    public bool canSpawnDM;
    private bool startDMSpawn;
    private Transform currentDMPoint;
    private int randomDMSpawnPoint;
    private GameObject spawnedDarkMonster;
    

    //Spawn times
    public float monsterSpawnTimeStart;
    public float monsterSpawnTimeMinimum;
    public float monsterSpawnTimeMaximum;
    private float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape") && canPause)
        {
            if (notPaused)
            {
                Pause();
                playerScript.canFlash = false;
            }
            //Application.Quit();
            else
            {
                Resume();
                playerScript.canFlash = true;
            }
        }

        
        if (playerLost)
        {
            //GameObject.Find("PauseMenuCanvas").GetComponent<pausemenu>().Pause();
        }

        if(canSpawnDM)
        {
            StartCoroutine(Spawn());
        }

    }

    public void Pause()
    {
        notPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        notPaused = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void DimMainLight()
    {
        entranceBlock.SetActive(true);
        StartCoroutine(DimLight());
        islandCollider.GetComponent<MeshCollider>().enabled = false;
        StartGame();
    }

    private IEnumerator DimLight()
    {
        for (float i = 1; i > 0; i -= ((Time.deltaTime * 0.5f)))
        {

            mainLight.intensity = i;
            yield return null;
        }
    }

    public void ResetGame()
    {
        SpawnObjects();
        CharacterController cc = playerPosition.GetComponent<CharacterController>();

        cc.enabled = false;


        playerPosition.transform.position = templeStartPoint.transform.position;
        playerPosition.transform.rotation = templeStartPoint.transform.rotation;
        cc.enabled = true;
        cameraScript.GetComponent<PlayerLook>().enabled = true;
        playerScript.GetComponent<PlayerMove>().enabled = true;
        scoreController.Restart();
        
        StartGame();
        Destroy(spawnedDarkMonster);

        islandCollider.GetComponent<MeshCollider>().enabled = false;
    }


    public void StartGame()
    {

        playerHealthUI.SetActive(true);
        playerScript.health = startHealth;
        distance = 0;
        playerLost = false;
        startDMSpawn = true;
        StartCoroutine(StartSpawn());
        canSpawnDM = false;
    }

    public void SpawnObjects()
    {
        orbSpawner.DeleteOrbs();
        runeSpawner.DeleteRunes();
    }

    public void FindSpawnPoint()
    {

        randomDMSpawnPoint = Random.Range(0, spawnPoints.Length);
        currentDMPoint = spawnPoints[randomDMSpawnPoint];
        distance = Vector3.Distance(playerPosition.transform.position, currentDMPoint.position);
        
        if (distance > 30)
        {
            currentDMPoint = spawnPoints[randomDMSpawnPoint];
            spawnedDarkMonster = Instantiate(darkMonster, spawnPoints[randomDMSpawnPoint].position, Quaternion.identity) as GameObject;
            darkMonsterController = spawnedDarkMonster.GetComponent<DarkMonsterController>();
        }
        else
        {
            FindSpawnPoint();
        }
        
    }

    public IEnumerator StartSpawn()
    {
        if(startDMSpawn)
        {
            yield return new WaitForSeconds(monsterSpawnTimeStart);
            FindSpawnPoint();
            startDMSpawn = false;
            yield return new WaitForSeconds(1);
            darkMonsterController.behavior = darkMonsterController.AttackPlayer;
            darkMonsterController.checkLargeRange = true;
        }

            
    }

    private IEnumerator Spawn()
    {
        if(canSpawnDM)
        {
            canSpawnDM = false;

            spawnDelay = Random.Range(monsterSpawnTimeMinimum, monsterSpawnTimeMaximum);
            yield return new WaitForSeconds(spawnDelay);
            FindSpawnPoint();
            yield return new WaitForSeconds(1);
            darkMonsterController.behavior = darkMonsterController.AttackPlayer;
            yield return null;
        }

    }

    public void PlayerWins()
    {
        GameObject playerLook = GameObject.Find("Camera");
        Destroy(playerLook.GetComponent<PlayerLook>());
        GameObject playerMove = GameObject.Find("Player");
        Destroy(playerMove.GetComponent<PlayerMove>());
    }


   public void PlayerLoses()
    {
        StartCoroutine(scoreController.TallyScore());
        playerLost = true;
        cameraScript.GetComponent<PlayerLook>().enabled = false;
        playerScript.GetComponent<PlayerMove>().enabled = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }



}
