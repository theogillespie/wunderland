using System;
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
        
       /* Vector2[] left = Common.offsetPoints(centerLine, new Vector2(-laneWidth/2, 0));
        Vector2[] right = Common.offsetPoints(centerLine, new Vector2(laneWidth/2, 0));
        List<Vector2> lines = new List<Vector2>();

        Vector2[] cl = (Vector2[])centerLine.Clone();
        Array.Reverse(cl);

        lines.AddRange(left);
        lines.AddRange(cl);
        lines.AddRange(right);
        
        */
        Common.drawLine(centerLine, lr);
        
    }

    public Vector2 getDir(int index) {
        return centerLine[index] - centerLine[index-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        if(!lr)
        {
            lr = GetComponent<LineRenderer>();
        }
        Draw();
    }
}
