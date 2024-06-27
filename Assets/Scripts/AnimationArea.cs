using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationArea : MonoBehaviour
{
    [SerializeField] private string triggerName;
    private Vector3 AnimEndPosition;
    public Vector3 AnimationEndPosition { get { return AnimEndPosition; } }
    private GameObject render;
    public GameObject Render {get { return render; } }

    private void Awake()
    {
        AnimEndPosition = transform.Find("AnimationEndPoint").position;
        render = transform.Find("Render").gameObject;
    }

    public string GetTriggerName()
    {
        return triggerName;
    }
}
