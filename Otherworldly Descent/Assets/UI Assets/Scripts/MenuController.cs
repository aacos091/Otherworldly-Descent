using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public Animator anim;
    public Animator title;
    public GameObject buttons;
    public GameObject backButton;


    public void startGame()
    {
        StartCoroutine(StartGame());
    }

    public void Credits()
    {
        StartCoroutine(GoToCredits());
    }

    public void backToMain()
    {
        StartCoroutine(GoToMain());
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator StartGame()
    {
        buttons.SetActive(false);
        title.SetInteger("State", 1);
        anim.SetInteger("State", 1);
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene(1);
    }

    IEnumerator GoToCredits()
    {
        buttons.SetActive(false);
        anim.SetInteger("State", 2);
        yield return new WaitForSecondsRealtime(3.0f);
        backButton.SetActive(true);

    }

    IEnumerator GoToMain()
    {
        backButton.SetActive(false);
        anim.SetInteger("State", 3);
        yield return new WaitForSecondsRealtime(2.0f);
        buttons.SetActive(true);

    }

}
