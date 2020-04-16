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
    public TextMeshProUGUI totalHealthText;
    public TextMeshProUGUI healthScoreText;
    public int totalHealth;
    private int healthScore;

    //Orb scoring
    public TextMeshProUGUI orbsCollectedText;
    public TextMeshProUGUI orbScoreText;
    public int orbsCollected;
    private int orbScore;

    //Rune scoring
    public TextMeshProUGUI runesCollectedText;
    public TextMeshProUGUI runeScoreText;
    public int runesCollected;
    private int runeScore;

    //Total score
    public TextMeshProUGUI scoreTallyText;
    public TextMeshProUGUI totalScoreText;
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
                orbsCollectedText.text = "<font=\"LiberationSans SDF\"> Orbs Collected: </font>" + i.ToString();
                yield return null;
            }

            orbsCollectedText.text = "<font=\"LiberationSans SDF\"> Orbs Collected: " + orbsCollected.ToString() + "</font>";

            yield return new WaitForSeconds(0.5f);
            orbScore = orbsCollected * 50;
            randomMinFloat = orbScore * 0.01f;
            randomMaxFloat = orbScore * 0.02f;
            randomMinInt = (int)randomMinFloat;
            randomMaxInt = (int)randomMaxFloat;

            for (int i = 0; i <= orbScore; i += Random.Range(randomMinInt, randomMaxInt))
            {

                orbScoreText.text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
                yield return null;
            }

            orbScoreText.text = "<font=\"LiberationSans SDF\">Score: " + orbScore.ToString() + "</font>";
        }
        else
        {
            orbsCollectedText.text = "<font=\"LiberationSans SDF\"> Orbs Collected: " + orbsCollected.ToString() + "</font>";
            yield return new WaitForSeconds(0.5f);
            orbScoreText.text = "<font=\"LiberationSans SDF\">Score: " + orbScore.ToString() + "</font>";
        }

        yield return new WaitForSeconds(0.5f);


        for (int i = 0; i <= totalHealth; i += (1))
        {
            totalHealthText.text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
            yield return null;
        }

        totalHealthText.text = "<font=\"LiberationSans SDF\">Health Remaining: " + totalHealth.ToString() + "</font>";

        yield return new WaitForSeconds(0.5f);
        healthScore = totalHealth * 100;

        randomMinFloat = healthScore * 0.01f;
        randomMaxFloat = healthScore * 0.02f;
        randomMinInt = (int)randomMinFloat;
        randomMaxInt = (int)randomMaxFloat;
        for (int i = 0; i <= healthScore; i += Random.Range(randomMinInt, randomMaxInt))
        {

            healthScoreText.text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
            yield return null;
        }

        healthScoreText.text = "<font=\"LiberationSans SDF\">Score: " + healthScore.ToString() + "</font>";

        yield return new WaitForSeconds(0.5f);

        if (runesCollected > 0)
        {
            for (int i = 0; i <= runesCollected; i += (1))
            {
                runesCollectedText.text = "<font=\"LiberationSans SDF\">Score: </font> " + i.ToString() + "</font>";
                yield return null;
            }

            runesCollectedText.text = "<font=\"LiberationSans SDF\">Runes Collected: " + runesCollected.ToString() + "</font>";

            yield return new WaitForSeconds(0.5f);
            runeScore = orbsCollected * 50;
            randomMinFloat = runeScore * 0.01f;
            randomMaxFloat = runeScore * 0.02f;
            randomMinInt = (int)randomMinFloat;
            randomMaxInt = (int)randomMaxFloat;

            for (int i = 0; i <= runeScore; i += Random.Range(randomMinInt, randomMaxInt))
            {

                runeScoreText.text = "<font=\"LiberationSans SDF\">Score: </font>" + i.ToString();
                yield return null;
            }
            runeScoreText.text = "<font=\"LiberationSans SDF\">Score: " + runeScore.ToString() + "</font>";
        }
        else
        {
            runesCollectedText.text = "<font=\"LiberationSans SDF\">Runes Collected: " + runesCollected.ToString() + "</font>";
            yield return new WaitForSeconds(0.5f);
            runeScoreText.text = "<font=\"LiberationSans SDF\">Score: " + runeScore.ToString() + "</font>";
        }


        yield return new WaitForSeconds(0.5f);






        totalScore = (orbScore + healthScore + runeScore);
        randomMinFloat = totalScore * 0.01f;
        randomMaxFloat = totalScore * 0.02f;
        randomMinInt = (int)randomMinFloat;
        randomMaxInt = (int)randomMaxFloat;
        for (int i = 0; i <= totalScore; i += Random.Range(randomMinInt, randomMaxInt))
        {

            totalScoreText.text = "<font=\"LiberationSans SDF\">Total Score:\n </font>" + i.ToString();
            yield return null;
        }
        totalScoreText.text = "<font=\"LiberationSans SDF\">Total Score:\n" + totalScore.ToString() + "</font>";
    }
}
