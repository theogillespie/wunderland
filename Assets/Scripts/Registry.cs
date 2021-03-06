using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry {
    public List<Car> cars = new List<Car>();
    public List<Building> buildings = new List<Building>();
    public List<Road> roads = new List<Road>();

    public List<notice> notices = new List<notice>();

    System.Random random = new System.Random();

    public struct notice {
        public enum logLevel {
            info,
            warning,
            error
        }
        logLevel level;
        string message;

        public notice(string msg, logLevel lvl=logLevel.info) {
            message = msg;
            level = lvl;
        }
    }


    public Registry() {
        this.automaticallyFind();
    }

    public void automaticallyFind()
    {
        cars.AddRange(GameObject.FindObjectsOfType<Car>());
        buildings.AddRange(GameObject.FindObjectsOfType<Building>());
        roads.AddRange(GameObject.FindObjectsOfType<Road>());
    }

    public Building newDesination(Vector2 pos, float minDist=10f, int depth=0) {
        if(depth >= 5) {
            return null; // to prevent never-ending recursion 
        }
        Building potentialDestination = buildings[random.Next(0, buildings.Count)];
        if(potentialDestination.comingCar || Vector2.Distance(potentialDestination.position(), pos) < minDist) {
            potentialDestination = newDesination(pos, minDist=minDist, depth=depth+1);
        }
        
        return potentialDestination;
    }
}
