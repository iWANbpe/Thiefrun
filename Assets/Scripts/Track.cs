using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] public GameObject LinePoints;
    private int lineCount;
    public int LineCount
    {
        get { return lineCount; }
    }

    private void Awake()
    {
        lineCount = LinePoints.transform.childCount;
    }

    public float GetLinePosX(int lineIndex)
    {
        lineIndex -= 1;
        lineIndex = lineIndex > LinePoints.transform.childCount ? 0 : lineIndex;
        return LinePoints.transform.GetChild(lineIndex).transform.position.x;
    }
}
