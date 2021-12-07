using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Car : MonoBehaviour
{
    public string name = "Car";
    public float charge = 100f;
    float elaspedTime;
    public AnimationCurve speedDraw;

    Common.PID turnControl = new Common.PID(0, 0, 0);

    public Common.positionAndTime desired;

    public Vector2 dimensions;
    BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.size = dimensions;
    }

    // Update is called once per frame
    void Update()
    {
        if(charge <= 0)
        {
            return;
        }

        float velocity = ((position() - desired.pos) / (desired.time - elaspedTime)).magnitude;
        turnControl.desired = Mathf.Atan2(desired.pos.y - position().y, desired.pos.x - position().x);
        transform.eulerAngles = new Vector3(0, 0, turnControl.update(heading()));

        charge -= speedDraw.Evaluate(velocity);
        elaspedTime += Time.deltaTime;
    }

    public Vector2 position()
    {
        return transform.position;
    }

    public float heading()
    {
        return transform.rotation.eulerAngles.z;
    }
}
