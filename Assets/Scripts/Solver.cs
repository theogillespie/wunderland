using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver {
    Car car;
    Building building;
    Road road;

    public void Init() {}
    public void Update() {}

    public void Copy() {return this.MemberwiseClone(); } // shallow
}
public class ShallowDestinationSolver: Solver {
    int index;
    int desiredIndex;
    const float distanceBeforeNext = .1f; //assumes the car is decently close to things
    const float sleepTime = 5f; // chills after arriving at a destination
    float startSleepTime;
    public ShallowDestinationSolver(Car c, Building destination) {
        Init(c, destination);
    }

    override void Init(Car c, Building destination) {
        car = c;
        building = destination;
        car.destination = building.transform;
        building.comingCar = car;

        index = Common.closestToo(car.position(), road.Lanes[0].centerLine);
        desiredIndex = Common.closestToo(car.destination.position, road.Lanes[0].centerLine);
    }

    public override void Update() {
        if(index >= desiredIndex) {
            if(!car.sleep) {
                car.sleep = true;
                startSleepTime = car.elaspedTime;
            } 
            if(car.elaspedTime - startSleepTime >= sleepTime) {
                building.comingCar = null;
                Building newBuilding = registry.newDesination(car.position());
                if(newBuilding) {
                    Init(car, newBuilding);
                }
            }
            return;
        }
        if(Vector2.Distance(car.position(), car.desired.pos) <= distanceBeforeNext) {
            index++;
            car.desired = new Common.positionAndTime(road.Lanes[0].centerLine[index], .1f);
        }
    }
}


public class Solver : MonoBehaviour
{
    Registry registry;
    List<T> solvers = new List<T>();
    Car car;
    Building building;
    void Setup() {
        solvers.Add(new ShallowDestinationSolver(car, building));
    }

    void solverUpdate() {
        foreach(var solver in solvers) {
            solver.Update();
        }
    }

    void LateUpdate() {
        solverUpdate();
    }

}
