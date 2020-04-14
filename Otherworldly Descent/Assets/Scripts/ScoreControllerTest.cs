using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControllerTest : MonoBehaviour
{
    //Stuff for calculation 
    private Text currentScoreText;
    private Text currentVarText;
    public int currentScoreInt;
    public int currentVarInt;
    public string currentString;
    public int currentScoreSwitch;
    private bool tallyItUp;

    //Random ranges
    private float randomMinFloat;
    private float randomMaxFloat;
    public int randomMinInt;
    public int randomMaxInt;

    //Total health scoring
    public Text totalHealthText;
    public Text healthScoreText;
    public int totalHealth;
    private int healthScore;

    //Orb scoring
    public Text orbsCollectedText;
    public Text orbScoreText;
    public int orbsCollected;
    private int orbScore;

    //Rune scoring
    public Text runesCollectedText;
    public Text runeScoreText;
    public int runesCollected;
    private int runeScore;

    //Total score
    public Text scoreTallyText;
    public Text totalScoreText;
    public int totalScore;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(1)))
        {


            tallyItUp = true;
            currentScoreSwitch = 1;
        }
        
        
            switch (currentScoreSwitch)
            {
                case 1:
                    currentVarText = orbsCollectedText;
                    currentScoreText = orbScoreText;
                    currentScoreInt = (orbsCollected * 50);
                    currentVarInt = (orbsCollected);
                    currentString = "Orbs Collected: ";
                    StartCoroutine(TallyScore());
                    break;


                case 2:
                    currentVarText = totalHealthText;
                    currentScoreText = healthScoreText;
                    currentScoreInt = (totalHealth * 100);
                    currentVarInt = (totalHealth);
                    currentString = "Health Remaining: ";
                    StartCoroutine(TallyScore());
                    break;


                case 3:
                    currentVarText = runesCollectedText;
                    currentScoreText = runeScoreText;
                    currentScoreInt = (runesCollected * 100);
                    currentVarInt = (runesCollected);
                    currentString = "Runes Collected: ";
                    StartCoroutine(TallyScore());
                    break;

                case 4:

                    break;
            
        }

    }


    public IEnumerator TallyScore()
    {



        if (tallyItUp)
        {
            if (currentVarInt > 0)
            {



                for (int i = 0; i <= currentVarInt; i += (1))
                {
                    currentVarText.text = currentString + i.ToString();
                    yield return null;
                }

                currentVarText.text = currentString + currentVarInt.ToString();

                yield return new WaitForSeconds(0.5f);
                randomMinFloat = currentScoreInt * 0.01f;
                randomMaxFloat = currentScoreInt * 0.02f;
                randomMinInt = (int)randomMinFloat;
                randomMaxInt = (int)randomMaxFloat;

                for (int i = 0; i <= currentScoreInt; i += Random.Range(randomMinInt, randomMaxInt))
                {

                    currentScoreText.text = "Score: " + i.ToString();
                    yield return null;
                }
                currentScoreText.text = "Score: " + currentScoreInt.ToString();


            }
            else
            {
                currentScoreText.text = currentString + currentVarInt.ToString();
                yield return new WaitForSeconds(0.5f);
                currentScoreText.text = "Score: " + currentScoreInt.ToString();

            }
            currentScoreText.text = "Score: " + currentScoreInt.ToString();
            yield return new WaitForSeconds(0.5f);
            currentScoreSwitch = 2;
            tallyItUp = false;
        }
                

        /*for (int i = 0; i <= totalHealth; i += (1))
        {
            totalHealthText.text = "Score: " + i.ToString();
            yield return null;
        }

        totalHealthText.text = "Health Remaining: " + totalHealth.ToString();

        yield return new WaitForSeconds(0.5f);
        healthScore = totalHealth * 100;

        randomMinFloat = healthScore * 0.01f;
        randomMaxFloat = healthScore * 0.02f;
        randomMinInt = (int)randomMinFloat;
        randomMaxInt = (int)randomMaxFloat;
        for (int i = 0; i <= healthScore; i += Random.Range(randomMinInt, randomMaxInt))
        {

            healthScoreText.text = "Score: " + i.ToString();
            yield return null;
        }

        healthScoreText.text = "Score: " + healthScore.ToString();

        yield return new WaitForSeconds(0.5f);

        if (runesCollected > 0)
        {
            for (int i = 0; i <= runesCollected; i += (1))
            {
                runesCollectedText.text = "Score: " + i.ToString();
                yield return null;
            }

            runesCollectedText.text = "Runes Collected: " + runesCollected.ToString();

            yield return new WaitForSeconds(0.5f);
            runeScore = orbsCollected * 50;
            randomMinFloat = runeScore * 0.01f;
            randomMaxFloat = runeScore * 0.02f;
            randomMinInt = (int)randomMinFloat;
            randomMaxInt = (int)randomMaxFloat;

            for (int i = 0; i <= runeScore; i += Random.Range(randomMinInt, randomMaxInt))
            {

                runeScoreText.text = "Score: " + i.ToString();
                yield return null;
            }
            runeScoreText.text = "Score: " + runeScore.ToString();
        }
        else
        {
            runesCollectedText.text = "Runes Collected: " + runesCollected.ToString();
            yield return new WaitForSeconds(0.5f);
            runeScoreText.text = "Score: " + runeScore.ToString();
        }


        yield return new WaitForSeconds(0.5f);






        totalScore = (orbScore + healthScore + runeScore);
        randomMinFloat = totalScore * 0.01f;
        randomMaxFloat = totalScore * 0.02f;
        randomMinInt = (int)randomMinFloat;
        randomMaxInt = (int)randomMaxFloat;
        for (int i = 0; i <= totalScore; i += Random.Range(randomMinInt, randomMaxInt))
        {

            totalScoreText.text = "Total Score:\n" + i.ToString();
            yield return null;
        }
        totalScoreText.text = "Total Score:\n" + totalScore.ToString();*/
    }
}
