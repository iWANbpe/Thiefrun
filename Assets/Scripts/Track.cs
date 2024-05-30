using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private GameObject LinePoints;

    public float GetLinePosY(int lineIndex)
    {
        lineIndex = lineIndex > LinePoints.transform.childCount ? 0 : lineIndex;
        return LinePoints.transform.GetChild(lineIndex).transform.position.y;
    }
}
