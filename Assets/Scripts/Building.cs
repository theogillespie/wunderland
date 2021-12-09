using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Building : MonoBehaviour
{
    public enum buildingType {
        normal, 
        special
    }
    public string name;
    public buildingType type;
    public Vector2 dimensions {
        get { return dimensions; }
        set {
            dimensions = value;
            draw();
        }
    }
    BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
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
        Common.drawBox(position(), dimensions, lr);
    }
    public Vector2 position() {
        return transform.postion;
    }
}
