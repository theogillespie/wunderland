using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Registry {
    public static List<Car> cars = new List<Car>();
    public static List<Building> buildings = new List<Building>();
    public static List<Road> roads = new List<Road>();

    public static List<notice> notices = new List<notice>();

    static Random random = new Random();

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


    static public Registry() {
        this.automaticallyFind();
    }

    static public void automaticallyFind()
    {
        cars.AddRange(GameObject.FindObjectsOfType<Car>());
        buildings.AddRange(GameObject.FindObjectsOfType<Building>());
        roads.AddRange(GameObject.FindObjectsOfType<Road>());
    }

    static public Building newDesination(Vector2 pos, float minDist=10f, int depth=0) {
        if(depth >= 5) {
            return null; // to prevent never-ending recursion 
        }
        Building potentialDestination = buildings[random.Next(0, buildings.Count)];
        if(potentialDestination.comingCar || Vector2.Distance(potentialDestination.position(), pos) < minDist) {
            newDesination(pos, minDist=minDist, depth=depth+1);
        }
    }
}
