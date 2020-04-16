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

    //Monster lin of sight stuff for despawning
    public bool canSeeMonster;
    public bool checkMonsterLOS;
    private Transform location;

    //Health regen
    public float healthRegenDelay;
    public bool healthRegen;

    private void Awake()
    {
     
        charController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        location = GetComponent<Transform>();
        flashSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {



        if ((Input.GetMouseButtonDown(0)) && canFlash)
        {
            //You can put the code for the image here
            StartCoroutine(CameraFlash());
            flashSound.Play();
            canFlash = false;
        }



        for (int i = 0; i < hearts.Length; i++)
        {
            if(i > health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if(i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(checkMonsterLOS)
        {
            RaycastHit hit;
            if (Physics.Raycast(location.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    canSeeMonster = true;
                }
            }
        }

        PlayerMovement();

    }


    public IEnumerator regenerateHealth()
    {
        yield return new WaitForSeconds(healthRegenDelay);
        if(health < numberOfHearts - 1)
        {
            health++;
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

        JumpInput();
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
    }

    private IEnumerator CameraFlash()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        for (float i = 0; i <= 5; i += (Time.deltaTime * 50))
        {
            // set color with i as alpha
            cameraLightSource.intensity = i;
            yield return null;
        }

        yield return new WaitForSeconds(cameraFlashTime);
        transform.GetChild(2).gameObject.SetActive(false);
        for (float i = 5; i >= 0; i -= (Time.deltaTime * 100))
        {
            // set color with i as alpha
            cameraLightSource.intensity = i;
            yield return null;
        }
        yield return new WaitForSeconds(flashDelay * flashDelayMultiplyer);
        canFlash = true;
    }
}


