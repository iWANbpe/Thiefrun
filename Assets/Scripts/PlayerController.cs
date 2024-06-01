using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lineChangeSpeed = 1f;
    [SerializeField] private float gravityKoef = 3f;

    private InputActionsPlayer inputActions;
    private CharacterController characterContrloller;
    private GameObject currentTrack;

    private Vector3 curPosition;
    private float gravity = -9.81f;
    private float velocity;
    private int curLine;
    private float lineX;
    private float newX;

    void Awake()
    {
        curLine = 2;
        characterContrloller = GetComponent<CharacterController>();
        inputActions = new InputActionsPlayer();

        inputActions.CharacterControls.ChangeLine.performed += ChangeLine;
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

    private void ApplyGravity()
    {
        if (characterContrloller.isGrounded)
        {
            velocity = -1f;
        }

        else
        {
            velocity += gravity * gravityKoef * Time.deltaTime;
        }

        curPosition.y = velocity;
    }

    private void FixedUpdate()
    {
        Run();
        MovingX();
        ApplyGravity();

        characterContrloller.Move(curPosition * Time.fixedDeltaTime);
    }
}
