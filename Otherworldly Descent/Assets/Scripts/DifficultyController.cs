using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyController : MonoBehaviour
{
    public DarkMonsterController darkMonsterController;

    public PlayerLook cameraScript;

    public PlayerMove playerScript;

    public LightMonster lightMonsterController;

    ScoreController scoreController;

    public GameController gameController;

    public RuneSpawner runeSpawner;



    //UI stuff
    public TextMeshProUGUI interactText;
    private bool playerInRange;
    private bool uiActive;
    public GameObject difficultyUI;

    //DarkMonster speed
    public float darkMonsterSpeedSwitch;
    public TextMeshProUGUI monsterSpeedText;

    //Number of hearts
    public TextMeshProUGUI numberOfHeartsText;

    //Phase duration
    public TextMeshProUGUI phaseDurationText;
    private float durationSwitch;

    //Rune amount
    public TextMeshProUGUI runeAmountText;

    //Flash delay
    private float flashDelaySwitch;
    public TextMeshProUGUI flashDelayText;

    //Flash effectivness
    private float flashEffectivnessSwitch;
    public TextMeshProUGUI flashEffectivnessText;





    //Health regen
    public Slider healthRegenSlider;
    private int regenDelaySwitch;
    public TextMeshProUGUI regenDelayText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerInRange)
        {
            if (Input.GetKey("e"))
            {
                cameraScript.enabled = false;
                gameController.canPause = false;
                difficultyUI.SetActive(true);
                interactText.text = "";
                uiActive = true;
                playerScript.canFlash = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (uiActive)
            {
                if (Input.GetKeyUp("escape"))
                {
                    gameController.canPause = true;
                    DisableUI();
                }
            }
        }










    }

    public void PhaseDuration(float newTime)
    {
        durationSwitch = (int)newTime;
        switch (durationSwitch)
        {
            case 1:
                lightMonsterController.phaseMultiplyer = 0.5f;
                phaseDurationText.text = "Phases will last half as long";
                break;

            case 2:
                lightMonsterController.phaseMultiplyer = 1;
                phaseDurationText.text = "Phases will last the normal amount";
                break;

            case 3:
                lightMonsterController.phaseMultiplyer = 2;
                phaseDurationText.text = "Phases will last twice as long";
                break;
        }
    }



    public void PlayerMustExit(bool exit)
    {

            gameController.playerMustExit = !gameController.playerMustExit;

    }



    public void RandomRuneSpawns(bool randomSpawns)

    {
            runeSpawner.randomSpawnBool = !runeSpawner.randomSpawnBool;

    }


    public void RuneAmount(float newRuneAmount)
    {
        runeSpawner.runeAmount = newRuneAmount;
        if(newRuneAmount == 1)
            runeAmountText.text = newRuneAmount.ToString() + " rune will spawn";
        else
            runeAmountText.text = newRuneAmount.ToString() + " runes will spawn";
    }


    public void DoubleDamage(bool doubleDamage)
    {

        darkMonsterController.doubleDamage = !darkMonsterController.doubleDamage;
        lightMonsterController.doubleDamage = !lightMonsterController.doubleDamage;


    }

    public void FlashDelay(float newDelay)
    {
        flashDelaySwitch = (int)newDelay;
        switch (flashDelaySwitch)
        {
            case 1:
                playerScript.flashDelayMultiplyer = 0.25f;
                flashDelayText.text = "The flash delay will be a quater as long";
                break;

            case 2:
                playerScript.flashDelayMultiplyer = 0.5f;
                flashDelayText.text = "The flash delay will be half as long";
                break;

            case 3:
                playerScript.flashDelayMultiplyer = 1;
                flashDelayText.text = "The flash delay will be it's regular time";
                break;

            case 4:
                playerScript.flashDelayMultiplyer = 2;
                flashDelayText.text = "The flash delay will be twice as long";
                break;

            case 5:
                playerScript.flashDelayMultiplyer = 3;
                flashDelayText.text = "The flash delay will be thrice as long";
                break;
        }
    }

    public void HealthRegen(bool regen)
    {
        
            playerScript.healthRegen = !playerScript.healthRegen;
        healthRegenSlider.interactable = !healthRegenSlider.interactable;

    }


    public void HealthRegenDelay(float regenDelay)
    {
        regenDelaySwitch = (int)regenDelay;
        switch (regenDelaySwitch)
        {
            case 1:
                playerScript.healthRegenDelay = 15;
                regenDelayText.text = "The player will regenerate 1 health every 15 seconds";
                break;

            case 2:
                playerScript.healthRegenDelay = 30;
                regenDelayText.text = "The player will regenerate 1 health every 30 seconds";
                break;

            case 3:
                playerScript.healthRegenDelay = 45;
                regenDelayText.text = "The player will regenerate 1 health every 45 seconds";
                break;

            case 4:
                playerScript.healthRegenDelay = 60;
                regenDelayText.text = "The player will regenerate 1 health every minute";
                break;

            case 5:
                playerScript.healthRegenDelay = 120;
                regenDelayText.text = "The player will regenerate 1 health every 2 minutes";
                break;
        }
    }

    public void FlashEffectivness(float newEffect)
    {
        flashEffectivnessSwitch = (int)newEffect;
        switch (flashEffectivnessSwitch)
        {
            case 1:
                darkMonsterController.flashMultiplyer = 0.25f;
                flashEffectivnessText.text = "The camera flash will be a quater as effective";
                break;

            case 2:
                darkMonsterController.flashMultiplyer = 0.5f;
                flashEffectivnessText.text = "The camera flash will be half as effective";
                break;

            case 3:
                darkMonsterController.flashMultiplyer = 1;
                flashEffectivnessText.text = "The camera flash will have its regular effectivness";
                break;

            case 4:
                darkMonsterController.flashMultiplyer = 2;
                flashEffectivnessText.text = "The camera flash will be twice as effective";
                break;

            case 5:
                darkMonsterController.flashMultiplyer = 3;
                flashEffectivnessText.text = "The camera flash will be thrice as effective";
                break;
        }
    }



    public void MonsterSpeed(float newSpeed)
    {
        darkMonsterSpeedSwitch = (int)newSpeed;
        switch (darkMonsterSpeedSwitch)
        {
            case 1:
                darkMonsterController.speed = 3;
                monsterSpeedText.text = "The Dark Monster will move at half its speed";
                break;

            case 2:
                darkMonsterController.speed = 6;
                monsterSpeedText.text = "The Dark Monster will move at its normal speed";
                break;

            case 3:
                darkMonsterController.speed = 9;
                monsterSpeedText.text = "The Dark Monster will move at double its speed";
                break;
        }
        
    }

    public void NumberOfHearts(float newHealth)
    {
        playerScript.numberOfHearts = (int)newHealth;
        playerScript.health = (int)newHealth - 1;
        int health = (int)newHealth - 1;
        if (newHealth == 2)
            numberOfHeartsText.text =  "You will have " + health.ToString() + " heart";
        else
            numberOfHeartsText.text =  "You will have " + health.ToString() + " hearts";
    }



    private void DisableUI()
    {
        cameraScript.LockCursor();
        Cursor.visible = false;
        cameraScript.enabled = true;
        uiActive = false;
        difficultyUI.SetActive(false);
        playerScript.canFlash = true;
        interactText.text = "Press \"E\" to interact.";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitbox")
            {
            playerInRange = true;
            interactText.text = "Press \"E\" to interact.";
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerHitbox")
        {
            playerInRange = false;
            interactText.text = "";
        }
    }
}
