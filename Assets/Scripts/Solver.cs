using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver {
    Car car;
    Building building;
    Road road;
    public void Update() {}
}
public class ShallowDestinationSolver: Solver {
    int index;
    int desiredIndex;
    float distanceBeforeNext = .1f;
    public ShallowDestinationSolver(Car c, Building destination) {
        car = car;
        building = destination;
        car.destination = building.transform;

        index = Common.closestToo(car.position(), road.Lanes[0].centerLine);
        desiredIndex = Common.closestToo(car.destination.position, road.Lanes[0].centerLine);
    }

    public override void Update() {
        if(index >= desiredIndex) {
            if(!car.sleep) {
                car.sleep = true;
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
        
    }

    

}
