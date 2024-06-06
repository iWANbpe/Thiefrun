using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lineChangeSpeed = 1f;
    [SerializeField] private float gravityKoef = 3f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float dodgeTime = 1f;

    private InputActionsPlayer inputActions;
    private CharacterController characterContrloller;
    private GameObject currentTrack;

    private Vector3 curPosition;
    private Vector3 characterControllerNormalCenter;
    private Vector3 characterControllerDodgeCenter = new Vector3(0f, -0.48f, 0f);

    private float gravity = -9.81f;
    private float velocity;
    private float lineX;
    private float newX;
    private float normalHeight;
    private float dodgeHeight = 1f;
    private int curLine;

    private bool isJumping, isDoge;


    void Awake()
    {
        curLine = 2;
        isJumping = false;
        characterContrloller = GetComponent<CharacterController>();
        normalHeight = characterContrloller.height;
        characterControllerNormalCenter = characterContrloller.center;
        
        inputActions = new InputActionsPlayer();

        inputActions.CharacterControls.ChangeLine.performed += ChangeLine;
        inputActions.CharacterControls.Jump.performed += Jump;
        inputActions.CharacterControls.Dodge.performed += Dodge;
    }

    private void Start()
    {
        InvokeRepeating("FindTrack", 0f, 0.5f);
    }

    private void OnEnable()
    {
        inputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.CharacterControls.Disable();
    }

    private void FindTrack()
    {
        GameObject[] allTracks = GameObject.FindGameObjectsWithTag("Track");
        GameObject nearTrack = null;
        float distance = Mathf.Infinity;

        foreach (GameObject track in allTracks)
        {
            float distanceToTrack = Vector3.Distance(transform.position, track.transform.position);

            if (distanceToTrack < distance)
            {
                distance = distanceToTrack;
                nearTrack = track;
            }
        }

        currentTrack = nearTrack;
    }

    private void ChangeLine(InputAction.CallbackContext context)
    {
        int lineChange = (int)context.ReadValue<float>();
        lineChange = -lineChange;
        if (curLine + lineChange <= currentTrack.GetComponent<Track>().LineCount && curLine + lineChange > 0)
        {
            curLine += lineChange;
            GoToNewLine();
        }
    }

    private void GoToNewLine()
    {
        lineX = currentTrack.GetComponent<Track>().GetLinePosX(curLine);
        newX = lineX - transform.position.x;
    }
    private void Run()
    {
        curPosition.z += speed; 
    }

    private void MovingX()
    {
        if (Mathf.Abs(transform.position.x - lineX ) > 0.1f)
        {
            curPosition.x = newX * lineChangeSpeed;
        }
        else
        {
            curPosition.x = 0f;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (characterContrloller.isGrounded && !isJumping && !isDoge)
        {
            isJumping = true;
        }
    }

    private void Dodge(InputAction.CallbackContext context)
    {
        if(characterContrloller.isGrounded && !isJumping && !isDoge)
        {
            isDoge = true;
            characterContrloller.height = dodgeHeight;
            characterContrloller.center = characterControllerDodgeCenter;
            StartCoroutine(Dodging());
        }
    }

    private IEnumerator Dodging()
    {
        yield return new WaitForSeconds(dodgeTime);
        isDoge = false;
        characterContrloller.height = normalHeight;
        characterContrloller.center = characterControllerNormalCenter;
    }

    private void ApplyGravity()
    {
        if (characterContrloller.isGrounded && !isJumping)
        {
            velocity = -1f;
        }

        else if (isJumping)
        {
            velocity += jumpForce;
        }

        else
        {
            velocity += gravity * gravityKoef * Time.deltaTime;
        }

        curPosition.y = velocity;
    }

    private void CheckForJumping()
    {
        if (!characterContrloller.isGrounded)
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        Run();
        MovingX();
        ApplyGravity();
        CheckForJumping();

        characterContrloller.Move(curPosition * Time.fixedDeltaTime);
    }
}
