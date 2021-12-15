using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    public Car car;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            car.desired = new Common.positionAndTime(pos, .2f);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        
    }
}
