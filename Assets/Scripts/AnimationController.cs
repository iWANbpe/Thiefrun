using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private GameObject parentObject;
    private CharacterController parentCharacterController;
    private Vector3 newPosition;
    public Vector3 AnimationEndPosition
    {
        set { newPosition = value; }
    }

    void Awake()
    {
        parentObject = GetComponentsInParent<Transform>()[1].gameObject;
        parentCharacterController = GetComponentInParent<CharacterController>();
    }
    
    public void AnimationTrickEnd()
    {
        parentObject.transform.position = new Vector3(parentObject.transform.position.x, parentObject.transform.position.y, parentObject.transform.position.z + Mathf.Abs(newPosition.z - parentObject.transform.position.z));
        parentCharacterController.enabled = true;
    }
}
