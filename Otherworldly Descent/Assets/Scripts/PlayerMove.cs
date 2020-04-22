using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //movemwent stuff
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fastMovementSpeed;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode runKey;
    private bool isJumping;

    //Player health stuff
    public int health;
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody rb;

    //Player progress stuff
    private Vector3 previousPlayerPosition;
    private float progression2;
    private bool playerMovement2;


    //Camera flash stuff
    public float cameraFlashTime;
    public float flashDelay;
    public bool canFlash = true;
    public Light cameraLightSource;
    public AudioSource flashSound;
    public float flashDelayMultiplyer;
    public GameObject flashArea;

    //Win/Lose stuff
    public GameController gameController;


    //Health regen
    public float healthRegenDelay = 120;
    public bool healthRegen;

    // Misc. Audio Stuff
    public bool isWalking;
    public bool isRunning;
    public bool templeChange = true;
    public AudioSource footstepsSound;
    public AudioClip walking;
    public AudioClip running;
    public AudioManager audioM;

    private void Awake()
    {
     
        charController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        AudioSource[] audios = GetComponents<AudioSource>();
        flashSound = audios[0];
        footstepsSound = audios[1];
        audioM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRegen)
        {
            StartCoroutine(RegenerateHealth());
        }


        if ((Input.GetMouseButtonDown(0)) && canFlash)
        {
            //You can put the code for the image here
            StartCoroutine(CameraFlash());
            flashSound.Play();
            canFlash = false;
        }



        for (int i = 0; i < hearts.Length; i++)
        {
            if (i > health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            gameController.PlayerLoses();
        }


        PlayFootsteps();
        PlayerMovement();

    }

    public IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(healthRegenDelay);
        if (healthRegen)
        {
            if (health < numberOfHearts - 1)
            {
                health++;
            }
        }
    }

    private void PlayerMovement()
    {
        float horizInput;
        float vertInput;

        if (Input.GetKey(runKey))
        {
            horizInput = Input.GetAxis(horizontalInputName) * fastMovementSpeed;
            vertInput = Input.GetAxis(verticalInputName) * fastMovementSpeed;
        }
        else
        {
            horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
            vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
        }

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        //JumpInput();
    }

    private void JumpInput()
    {
        if (Input.GetKey(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }


    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "healthUp")
        {
            health++;
            if (health > numberOfHearts - 1)
            {
                health = numberOfHearts - 1;
            }

        }

        if (other.tag == "templeChange") 
        {
            if (templeChange)
            {
                audioM.ChangeBGM();
                templeChange = false;
            }

        }
    }

    private IEnumerator CameraFlash()
    {
        flashArea.SetActive(true);
        for (float i = 0; i <= 5; i += (Time.deltaTime * 50))
        {
            // set color with i as alpha
            cameraLightSource.intensity = i;
            yield return null;
        }

        yield return new WaitForSeconds(cameraFlashTime);
        flashArea.SetActive(false);
        for (float i = 5; i >= 0; i -= (Time.deltaTime * 100))
        {
            // set color with i as alpha
            cameraLightSource.intensity = i;
            yield return null;
        }
        yield return new WaitForSeconds(flashDelay * flashDelayMultiplyer);
        canFlash = true;
    }

    public void PlayFootsteps()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            if (Input.GetKey("left shift"))
            {
                // Running
                isWalking = false;
                isRunning = true;
            }
            else
            {
                // Walking
                isWalking = true;
                isRunning = false;
            }
        }
        else
        {
            // Stopped
            isWalking = false;
            isRunning = false;
        }

        // Play Audio
        if (isWalking)
        {
            if (footstepsSound.clip != walking)
            {
                footstepsSound.enabled = false;
                footstepsSound.clip = walking;
            }
            footstepsSound.enabled = true;
            footstepsSound.loop = true;
            Debug.Log("Walking");
        }
        else if (isRunning)
        {
            if (footstepsSound.clip != running)
            {
                footstepsSound.enabled = false;
                footstepsSound.clip = running;
            }
            footstepsSound.enabled = true;
            footstepsSound.loop = true;
            Debug.Log("Running");
        }
        else
        {
            footstepsSound.enabled = false;
            footstepsSound.loop = false;
            Debug.Log("Stopped");
        }
    }
}


