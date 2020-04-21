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

    public ScoreController scoreController;

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
                scoreController.phaseLengthBonusBool = true;
                scoreController.phaseLengthBonus = 0.8f;
                scoreController.firstTextCalc++;
                break;

            case 2:
                lightMonsterController.phaseMultiplyer = 1;
                phaseDurationText.text = "Phases will last the normal amount";
                scoreController.phaseLengthBonusBool = false;
                scoreController.phaseLengthBonus = 1f;
                scoreController.firstTextCalc--;
                break;

            case 3:
                lightMonsterController.phaseMultiplyer = 2;
                phaseDurationText.text = "Phases will last twice as long";
                scoreController.phaseLengthBonusBool = true;
                scoreController.phaseLengthBonus = 1.2f;
                scoreController.firstTextCalc++;
                break;
        }
    }



    public void PlayerMustExit(bool exit)
    {

            gameController.playerMustExit = !gameController.playerMustExit;
            scoreController.returnToExitBonusBool = !scoreController.returnToExitBonusBool;
        if (exit)
        {
            scoreController.returnToExitBonus = 5000;
            scoreController.firstTextCalc++;
        }
        else
        {
            scoreController.returnToExitBonus = 0;
            scoreController.firstTextCalc--;
        }


    }



    public void RandomRuneSpawns(bool randomSpawns)

    {
            runeSpawner.randomSpawnBool = !runeSpawner.randomSpawnBool;
            scoreController.randomRunesBonusBool = !scoreController.randomRunesBonusBool;
        if (randomSpawns)
        {
            scoreController.randomRunesBonus = 7500;
            scoreController.firstTextCalc++;
        }
        else
        {
            scoreController.randomRunesBonus = 0;
            scoreController.firstTextCalc--;
        }
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
        scoreController.doubleDamageBool = !scoreController.doubleDamageBool;
        darkMonsterController.doubleDamage = !darkMonsterController.doubleDamage;
        lightMonsterController.doubleDamage = !lightMonsterController.doubleDamage;
        if (doubleDamage)
        {
            scoreController.doubleDamageBonus = 1.75f;
            scoreController.firstTextCalc++;
        }
        else
        {
            scoreController.doubleDamageBonus = 1;
            scoreController.firstTextCalc--;
        }

    }

    public void FlashDelay(float newDelay)
    {
        flashDelaySwitch = (int)newDelay;
        switch (flashDelaySwitch)
        {
            case 1:
                playerScript.flashDelayMultiplyer = 0.25f;
                flashDelayText.text = "The flash delay will be a quater as long";
                scoreController.flashDelayBonusBool = true;
                scoreController.flashDelayBonus = 0.5f;
                break;

            case 2:
                playerScript.flashDelayMultiplyer = 0.5f;
                flashDelayText.text = "The flash delay will be half as long";
                scoreController.flashDelayBonusBool = true;
                scoreController.flashDelayBonus = 0.75f;
                scoreController.firstTextCalc++;
                break;

            case 3:
                playerScript.flashDelayMultiplyer = 1;
                flashDelayText.text = "The flash delay will be it's regular time";
                scoreController.flashDelayBonusBool = false;
                scoreController.flashDelayBonus = 1f;
                scoreController.firstTextCalc--;
                break;

            case 4:
                playerScript.flashDelayMultiplyer = 2;
                flashDelayText.text = "The flash delay will be twice as long";
                scoreController.flashDelayBonusBool = true;
                scoreController.flashDelayBonus = 1.25f;
                scoreController.firstTextCalc++;
                break;

            case 5:
                playerScript.flashDelayMultiplyer = 3;
                flashDelayText.text = "The flash delay will be thrice as long";
                scoreController.flashDelayBonusBool = true;
                scoreController.flashDelayBonus = 1.5f;
                break;
        }
    }

    public void HealthRegen(bool regen)
    {
        
            playerScript.healthRegen = !playerScript.healthRegen;
            healthRegenSlider.interactable = !healthRegenSlider.interactable;
            scoreController.regenHealthBonusBool = !scoreController.regenHealthBonusBool;
            if(regen)
        {
            scoreController.firstTextCalc++;
        }
        else
        {
            scoreController.firstTextCalc--;
        }

    }


    public void HealthRegenDelay(float regenDelay)
    {
        regenDelaySwitch = (int)regenDelay;
        switch (regenDelaySwitch)
        {
            case 1:
                playerScript.healthRegenDelay = 15;
                regenDelayText.text = "The player will regenerate 1 health every 15 seconds";
                scoreController.regenHealthBonus = 0.3f;
                break;

            case 2:
                playerScript.healthRegenDelay = 30;
                regenDelayText.text = "The player will regenerate 1 health every 30 seconds";
                scoreController.regenHealthBonus = 0.4f;
                scoreController.firstTextCalc++;
                break;

            case 3:
                playerScript.healthRegenDelay = 45;
                regenDelayText.text = "The player will regenerate 1 health every 45 seconds";
                scoreController.regenHealthBonus = 0.5f;
                scoreController.firstTextCalc--;
                break;

            case 4:
                playerScript.healthRegenDelay = 60;
                regenDelayText.text = "The player will regenerate 1 health every minute";
                scoreController.regenHealthBonus = 0.7f;
                scoreController.firstTextCalc++;
                break;

            case 5:
                playerScript.healthRegenDelay = 120;
                regenDelayText.text = "The player will regenerate 1 health every 2 minutes";
                scoreController.regenHealthBonus = 0.85f;
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
                scoreController.flashEffecBonusBool = true;
                scoreController.flashEffecBonus = 0.7f;

                    
                break;

            case 2:
                darkMonsterController.flashMultiplyer = 0.5f;
                flashEffectivnessText.text = "The camera flash will be half as effective";
                scoreController.flashEffecBonusBool = true;
                scoreController.flashEffecBonus = 0.85f;
                scoreController.firstTextCalc++;
                break;

            case 3:
                darkMonsterController.flashMultiplyer = 1;
                flashEffectivnessText.text = "The camera flash will have its regular effectivness";
                scoreController.flashEffecBonusBool = false;
                scoreController.flashEffecBonus = 1f;
                scoreController.firstTextCalc--;
                break;

            case 4:
                darkMonsterController.flashMultiplyer = 2;
                flashEffectivnessText.text = "The camera flash will be twice as effective";
                scoreController.flashEffecBonusBool = true;
                scoreController.flashEffecBonus = 1.25f;
                scoreController.firstTextCalc++;
                break;

            case 5:
                darkMonsterController.flashMultiplyer = 3;
                flashEffectivnessText.text = "The camera flash will be thrice as effective";
                scoreController.flashEffecBonusBool = true;
                scoreController.flashEffecBonus = 1.5f;

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
                scoreController.monsterSpeedBonusBool = true;
                scoreController.monsterSpeedBonus = 0.75f;
                scoreController.firstTextCalc++;
                break;

            case 2:
                darkMonsterController.speed = 6;
                monsterSpeedText.text = "The Dark Monster will move at its normal speed";
                scoreController.monsterSpeedBonusBool = false;
                scoreController.monsterSpeedBonus = 1f;
                scoreController.firstTextCalc--;
                break;

            case 3:
                darkMonsterController.speed = 9;
                monsterSpeedText.text = "The Dark Monster will move at double its speed";
                scoreController.monsterSpeedBonusBool = true;
                scoreController.monsterSpeedBonus = 1.25f;
                scoreController.firstTextCalc++;
                break;
        }
        
    }

    public void NumberOfHearts(float newHealth)
    {

        playerScript.numberOfHearts = (int)newHealth;
        int health = (int)newHealth - 1;
        playerScript.health = health;
        gameController.startHealth = health;
        scoreController.totalHealth = health;
        
        if (newHealth == 2)
        {
            numberOfHeartsText.text = "You will have " + health.ToString() + " heart";
            scoreController.healthAmountBonus = 4;
        }
        else
        {
            numberOfHeartsText.text = "You will have " + health.ToString() + " hearts";
            float healthBonus = 1 / health + 1;
            scoreController.healthAmountBonus = (int)healthBonus;
        }
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
