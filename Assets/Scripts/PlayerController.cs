using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private InputActionsPlayer inputActions;
    private int curLine;

    void Awake()
    {
        curLine = 2;
        inputActions = new InputActionsPlayer();

        inputActions.CharacterControls.ChangeLine.started += ChangeLine;
        inputActions.CharacterControls.ChangeLine.canceled += ChangeLine;
    }

    private void OnEnable()
    {
        inputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.CharacterControls.Disable();
    }

    private void ChangeLine(InputAction.CallbackContext context)
    {
       curLine += (int)context.ReadValue<float>();
    }

    void Update()
    {
        print(curLine);
    }
    private void FixedUpdate()
    {
        
    }


}
