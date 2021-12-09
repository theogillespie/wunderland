using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry {
    public List<Car> cars = new List<Car>();
    public List<Building> buildings = new List<Building>();
    public List<Road> roads = new List<Road>();

    public List<notice> notices = new List<notice>();
    public List<notices> displayNotices = new List<notices>();
    public struct notice {
        public enum logLevel {
            info,
            warning,
            error
        }
        logLevel level;
        string message;

        public notice(string message, logLevel level=logLevel.info) {
            message = message;
            level = level;
        }
    }

    public void automaticallyFind()
    {
        cars.AddRange(GameObject.FindObjectsOfType<Car>());
        buildings.AddRange(GameObject.FindObjectsOfType<Building>());
        roads.AddRange(GameObject.FindObjectsOfType<Road>());
    }
}
