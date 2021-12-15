using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]
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
    public float elaspedTime;
    public AnimationCurve speedDraw;

    [Header("gains")]
    public float p;
    public float i;
    public float d;

    Common.PID turnControl;

    public Common.positionAndTime desired;
    public Transform destination;

    public Vector2 dimensions;

    BoxCollider2D col;
    LineRenderer lr;
    void Start()
    {
        
        col = GetComponent<BoxCollider2D>();
        col.size = dimensions;

        lr = GetComponent<LineRenderer>();

        turnControl = new Common.PID(p, i, d);
    }

    public float localizeTime(float time) {
        return time + elaspedTime;
    }

    // Update is called once per frame
    void Update()
    {
        elaspedTime += Time.deltaTime;
        draw();
        if(charge <= 0f || sleep)
        {
            return;
        }

        float velocity = ((position() - desired.pos) / (desired.time - elaspedTime)).magnitude;
        turnControl.desired = Mathf.Atan2(desired.pos.y - position().y, desired.pos.x - position().x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, turnControl.desired); //this is not god
        //float a = turnControl.update(heading());
        
        

        transform.position += transform.right * velocity * Time.deltaTime;

        if(type == carType.special) {
            specialWare();
        }

        //charge -= speedDraw.Evaluate(velocity); disabled for testing
        
    }

    public void specialWare() {}

    public void draw() {
        Common.drawBox(position(), dimensions, lr, angle: heading());
    }

    public Vector2 position()
    {
        return transform.position;
    }

    public float heading()
    {
        return transform.rotation.eulerAngles.z;
    }

    private void OnDrawGizmosSelected()
    {
        if (!lr) { lr = GetComponent<LineRenderer>(); }
        draw();
    }
}
