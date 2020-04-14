using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
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


            StartCoroutine(TallyScore());

        }



    }


    public IEnumerator TallyScore()
    {

        if (orbsCollected > 0)
        {
            for (int i = 0; i <= orbsCollected; i += (1))
            {
                orbsCollectedText.text = "Score: " + i.ToString();
                yield return null;
            }

            orbsCollectedText.text = "Orbs Collected: " + orbsCollected.ToString();

            yield return new WaitForSeconds(0.5f);
            orbScore = orbsCollected * 50;
            randomMinFloat = orbScore * 0.01f;
            randomMaxFloat = orbScore * 0.02f;
            randomMinInt = (int)randomMinFloat;
            randomMaxInt = (int)randomMaxFloat;

            for (int i = 0; i <= orbScore; i += Random.Range(randomMinInt, randomMaxInt))
            {

                orbScoreText.text = "Score: " + i.ToString();
                yield return null;
            }

            orbScoreText.text = "Score: " + orbScore.ToString();
        }
        else
        {
            orbsCollectedText.text = "Orbs Collected: " + orbsCollected.ToString();
            yield return new WaitForSeconds(0.5f);
            orbScoreText.text = "Score: " + orbScore.ToString();
        }

        yield return new WaitForSeconds(0.5f);


        for (int i = 0; i <= totalHealth; i += (1))
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
        totalScoreText.text = "Total Score:\n" + totalScore.ToString();
    }
}
