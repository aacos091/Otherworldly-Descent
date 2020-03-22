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
    public int health = 5;
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody rb;


    //Player progress stuff
    private Vector3 previousPlayerPosition;

    private float progression2;

    private bool playerMovement2;





    private void Awake()
    {
     
        charController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {

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


        PlayerMovement();



           /* if (Time.time > keneticWaitTime + keneticDelay)
            {
            progression2 = Vector3.Distance(previousPlayerPosition, this.transform.position);
            previousPlayerPosition = this.transform.position;

            if (progression2 > 4)
            {
                playerMovement2 = true;
            }
            else
            {
                playerMovement2 = false;
            }
            keneticWaitTime = Time.time;

            }*/

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
       if (other.tag == "Enemy")
        {
            health --;

        }
        if (other.tag == "healthUp")
        {
            health++;
            if (health > numberOfHearts - 1)
            {
                health = numberOfHearts - 1;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}


