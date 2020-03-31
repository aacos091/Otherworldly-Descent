using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{/*
    public Text downloadText;

    private bool playerLost;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey("escape"))
        //    Application.Quit();


        if(playerLost)
        {
            GameObject.Find("PauseMenuCanvas").GetComponent<pausemenu>().Pause();
        }

    }


    public void PlayerWins()
    {
        downloadText.text = "You succsessfully completed your mission!";
        GameObject playerLook = GameObject.Find("Camera");
        Destroy(playerLook.GetComponent<PlayerLook>());
        GameObject playerMove = GameObject.Find("Player");
        Destroy(playerMove.GetComponent<PlayerMove>());
    }


   public void PlayerLoses()
    {
        playerLost = true;
        GameObject playerLook = GameObject.Find("Camera");
        Destroy(playerLook.GetComponent<PlayerLook>());
        GameObject playerMove = GameObject.Find("Player");
        Destroy(playerMove.GetComponent<PlayerMove>());
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            transform.GetChild(0).gameObject.SetActive(false);

        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            transform.GetChild(0).gameObject.SetActive(true);

        }
    }

}
