using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    //Random ranges
    private float randomMinFloat;
    private float randomMaxFloat;
    public int randomMinInt;
    public int randomMaxInt;

    //Total health scoring
    public int totalHealth;
    private int healthScore;

    //Orb scoring

    public int orbsCollected;
    private int orbScore;

    //Rune scoring

    public int runesCollected;
    private int runeScore;

    //Bonus scores
    public TextMeshProUGUI[] bonusTextsLeft;
    public TextMeshProUGUI[] bonusTextsRight;
    private int currentBonusText;

    //Other stuff
    private float tallySpeed = 0.5f;
    private bool tallySpeedUpBool;
    public int firstTextCalc;
    public int firstText = 4;

    //Monster bonuses
    public bool doubleDamageBool;
    public float doubleDamageBonus; //Multiplyer
    public bool monsterSpeedBonusBool;
    public float monsterSpeedBonus; //Multiplyer
    public bool phaseLengthBonusBool;
    public float phaseLengthBonus;  //Bonus

    //Player Bonuses

    public bool regenHealthBool; //Multiplyer

    public bool regenHealthBonusBool;
    public float regenHealthBonus; //Multiplyer
    public bool healthAmountBonusBool;
    public int healthAmountBonus; //Multiplyer
    public bool flashEffecBonusBool;
    public float flashEffecBonus;   //Bonus
    public bool flashDelayBonusBool;
    public float flashDelayBonus;  //Bonus

    //Level bonuses
    public float returnToExitBonus;
    public bool returnToExitBonusBool; //Bonus
    public float randomRunesBonus; //Bonus
    public bool randomRunesBonusBool;




    //Total score
    public TextMeshProUGUI scoreTallyText;
    public TextMeshProUGUI totalScoreText;
    private float totalScoreFloat;
    public float totalScoreInt;
    public float multiplyer;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(1)))
        {
            StartCoroutine(TallyScore());

        }

        if ((Input.GetMouseButtonDown(0)))
        {
            tallySpeed = 0.03f;
            tallySpeedUpBool = true;
        }

        if (firstTextCalc == 0)
            firstText = 4;

        if (firstTextCalc == 2)
            firstText = 3;

        else if (firstTextCalc == 4)
            firstText = 2;

        else if (firstTextCalc == 6)
            firstText = 1;

        else if (firstTextCalc == 8)
            firstText = 0;
        


    }

    public void Restart()
    {
        tallySpeed = 0.5f;
        tallySpeedUpBool = false;
        totalScoreText.text = "";
        foreach (TextMeshProUGUI bonusText in bonusTextsLeft)
        {
            bonusText.text = "";

        }
        foreach (TextMeshProUGUI bonusText in bonusTextsRight)
        {
            bonusText.text = "";

        }
    }



    public IEnumerator TallyScore()
    {
        tallySpeed = 0.5f;
        tallySpeedUpBool = false;
        currentBonusText = firstText;
        if (orbsCollected > 0)
        {
            for (int i = 0; i <= orbsCollected; i++)
            {
                bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\"> Orbs Collected: </font>" + i.ToString();
                yield return null;
            }

            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\"> Orbs Collected: " + orbsCollected.ToString() + "</font>";

            yield return new WaitForSeconds(tallySpeed);
            orbScore = orbsCollected * 50;
            randomMinFloat = orbScore * 0.01f;
            randomMaxFloat = orbScore * 0.02f;
            if (tallySpeedUpBool)
            {
                randomMinFloat = 999999;
                randomMaxFloat = 999999;
            }
            randomMinInt = (int)randomMinFloat;
            randomMaxInt = (int)randomMaxFloat;

            for (int i = 0; i <= orbScore; i += Random.Range(randomMinInt, randomMaxInt))
            {

                bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
                yield return null;
            }

            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: " + orbScore.ToString() + "</font>";
        }
        else
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\"> Orbs Collected: " + orbsCollected.ToString() + "</font>";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: " + orbScore.ToString() + "</font>";
        }
        currentBonusText++;
        yield return new WaitForSeconds(tallySpeed);


        for (int i = 0; i <= totalHealth; i++)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
            yield return null;
        }

        bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Health Remaining: " + totalHealth.ToString() + "</font>";

        yield return new WaitForSeconds(tallySpeed);
        healthScore = totalHealth * 300 * healthAmountBonus;

        randomMinFloat = healthScore * 0.01f;
        randomMaxFloat = healthScore * 0.02f;
        if (tallySpeedUpBool)
        {
            randomMinFloat = 999999;
            randomMaxFloat = 999999;
        }
        randomMinInt = (int)randomMinFloat;
        randomMaxInt = (int)randomMaxFloat;

        for (int i = 0; i <= healthScore; i += Random.Range(randomMinInt, randomMaxInt))
        {

            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
            yield return null;
        }

        bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: " + healthScore.ToString() + "</font>";
        currentBonusText++;
        yield return new WaitForSeconds(tallySpeed);

        if (runesCollected > 0)
        {
            for (int i = 0; i <= runesCollected; i++)
            {
                bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: </font> " + i.ToString() + "</font>";
                yield return null;
            }

            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Runes Collected: " + runesCollected.ToString() + "</font>";

            yield return new WaitForSeconds(tallySpeed);
            runeScore = orbsCollected * 50;
            randomMinFloat = runeScore * 0.01f;
            randomMaxFloat = runeScore * 0.02f;
            if (tallySpeedUpBool)
            {
                randomMinFloat = 999999;
                randomMaxFloat = 999999;
            }
            randomMinInt = (int)randomMinFloat;
            randomMaxInt = (int)randomMaxFloat;

            for (int i = 0; i <= runeScore; i += Random.Range(randomMinInt, randomMaxInt))
            {

                bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
                yield return null;
            }
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: " + runeScore.ToString() + "</font>";
        }
        else
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Runes Collected: " + runesCollected.ToString() + "</font>";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">Score: " + runeScore.ToString() + "</font>";
        }

        currentBonusText++;
        yield return new WaitForSeconds(tallySpeed);



        if(returnToExitBonusBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Return to exit bonus";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + returnToExitBonus.ToString() + "</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if(randomRunesBonusBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Random runes";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + randomRunesBonus.ToString() + "</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if (phaseLengthBonusBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Phase Length bonus";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + phaseLengthBonus.ToString() + " Multiplyer</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if (monsterSpeedBonusBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Monster speed bonus";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + monsterSpeedBonus.ToString() + " Multiplyer</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if (regenHealthBonusBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Regen health bonus";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + regenHealthBonus.ToString() + " Multiplyer</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if (flashEffecBonusBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Flash effectivness bonus";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + flashEffecBonus.ToString() + " Multiplyer</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if (flashDelayBonusBool)
        {

            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Flash delay bonus";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + flashDelayBonus.ToString() + "</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }

        if (doubleDamageBool)
        {
            bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Double damage bonus</font>";
            yield return new WaitForSeconds(tallySpeed);
            bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" +doubleDamageBonus.ToString() + " Multiplyer</font>";
            currentBonusText++;
            yield return new WaitForSeconds(tallySpeed);
        }




        currentBonusText++;
        multiplyer = (phaseLengthBonus * monsterSpeedBonus * regenHealthBonus * flashEffecBonus * flashDelayBonus * doubleDamageBonus);
        totalScoreFloat = ((orbScore + healthScore + runeScore + returnToExitBonus + randomRunesBonus) * multiplyer);
        totalScoreInt = (int)totalScoreFloat;

        randomMinFloat = totalScoreInt * 0.01f;
        randomMaxFloat = totalScoreInt * 0.02f;
        if (tallySpeedUpBool)
        {
            randomMinFloat = 999999;
            randomMaxFloat = 999999;
        }
        randomMinInt = (int)randomMinFloat;
        randomMaxInt = (int)randomMaxFloat;
        bonusTextsLeft[currentBonusText].text = "<font=\"LiberationSans SDF\">Total Score: </font>";
        for (int i = 0; i <= totalScoreInt; i += Random.Range(randomMinInt, randomMaxInt))
        {

            bonusTextsRight[currentBonusText].text = i.ToString();
            yield return null;
        }

        bonusTextsRight[currentBonusText].text = "<font=\"LiberationSans SDF\">" + totalScoreInt.ToString() + "</font>";

        tallySpeed = 0.5f;
        tallySpeedUpBool = false;
    }
}
