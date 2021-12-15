using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class Building : MonoBehaviour
{
    public enum buildingType {
        normal, 
        special
    }
    public string name;
    public buildingType type;
    public Vector2 dimensions;
    
    public Car comingCar;

    BoxCollider2D col;
    LineRenderer lr;
    // Start is called before the first frame update
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
        if(type == buildingType.special) {
            specialWare();
        }
    }

    public void specialWare() {}

    public void draw() {
        Common.drawBox(position(), dimensions, lr, angle:heading()) ;
    }
    public Vector2 position() {
        return transform.position;
    }

    public float heading()
    {
        return transform.rotation.eulerAngles.z;
    }

    private void OnDrawGizmosSelected()
    {
        if(!lr) { lr = GetComponent<LineRenderer>(); }
        draw();
    }
}
