using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry {
    public List<Car> cars = new List<Car>();
    public List<Building> buildings = new List<Building>();
    public List<Road> roads = new List<Road>();

    public void automaticallyFind()
    {
        cars.AddRange(GameObject.FindObjectsOfType<Car>());
        buildings.AddRange(GameObject.FindObjectsOfType<Building>());
        roads.AddRange(GameObject.FindObjectsOfType<Road>());
    }
}
