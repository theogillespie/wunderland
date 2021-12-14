using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequreComponent(typeof(LineRenderer))]
public class Car : MonoBehaviour
{
    public enum carType {
        normal,
        special
    }
    public string name = "Car";
    
    public carType type;
    public bool sleep;
    public float charge = 100f;
    float elaspedTime;
    public AnimationCurve speedDraw;

    Common.PID turnControl = new Common.PID(0, 0, 0);

    public Common.positionAndTime desired;
    public Transform destination;

    public Vector2 dimensions {
        get { return dimensions; }
        set {
            dimensions = value;
            draw();
        }
    }

    BoxCollider2D col;
    LineRenderer lr;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.size = dimensions;

        lr = GetComponent<LineRenderer>();
        draw();
    }

    // Update is called once per frame
    void Update()
    {
        if(charge <= 0f || sleep)
        {
            return;
        }

        float velocity = ((position() - desired.pos) / (desired.time - elaspedTime)).magnitude;
        turnControl.desired = Mathf.Atan2(desired.pos.y - position().y, desired.pos.x - position().x);
        transform.eulerAngles = new Vector3(0, 0, turnControl.update(heading()));

        if(type == carType.special) {
            specialWare();
        }

        //charge -= speedDraw.Evaluate(velocity); disabled for testing
        elaspedTime += Time.deltaTime;
    }

    public void specialWare() {}

    public void draw() {
        Common.drawBox(position(), dimensions, lr);
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
