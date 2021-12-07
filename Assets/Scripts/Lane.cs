using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lane : MonoBehaviour
{
    public enum laneType {
        normal,
        shoulder
    }

    public Vector2[] centerLine;
    public float laneWidth = 2f;
    public laneType type;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Draw()
    {
        lr = GetComponent<LineRenderer>();
        Common.DrawLine(centerLine, lr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
