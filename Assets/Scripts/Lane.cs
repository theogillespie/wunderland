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
        lr = GetComponent<LineRenderer>();
    }

    void Draw()
    {
        Common.drawLine(centerLine, lr);
        Common.drawLine(Common.offsetPoints(centerLine, new Vector2(laneWidth, 0)), lr); // draw right boundary
        Common.drawLine(Common.offsetPoints(centerLine, new Vector2(-laneWidth, 0)), lr);  // left boundary
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
