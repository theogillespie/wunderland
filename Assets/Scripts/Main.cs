using System.Collections;
using System.Collections.Generic;
using UnityEngine;



static class Global
{
    public static Registry registry;
    public static Road road;
}

public class ShallowDestinationSolver {
    Car car;
    Building building;
    Road road;
    int index;
    int desiredIndex;
    const float distanceBeforeNext = .1f; //assumes the car is decently close to things
    const float sleepTime = 5f; // chills after arriving at a destination
    float startSleepTime;
    public ShallowDestinationSolver(Car c, Building destination) {

        Init(c, destination);
    }

   public void Init(Car c, Building destination) {
       
        
        car = c;
        building = destination;
        car.destination = building.transform;
        building.comingCar = car;

        index = Common.closestToo(car.position(), Global.road.Lanes[0].centerLine);
        car.desired = new Common.positionAndTime(Global.road.Lanes[0].centerLine[index], car.localizeTime(1f));
        desiredIndex = Common.closestToo(car.destination.position, Global.road.Lanes[0].centerLine);
    }

   public void Update() {

        Debug.Log(car.desired.pos.ToString()); 
        
        if(false) {
            if(!car.sleep) {
                car.sleep = true;
                startSleepTime = car.elaspedTime;
            } 
            if(car.elaspedTime - startSleepTime >= sleepTime) {
                building.comingCar = null;
                Building newBuilding = Global.registry.newDesination(car.position());
                if(newBuilding) {
                    Init(car, newBuilding);
                }
            }
            return;
        }
        
        if(Vector2.Distance(car.position(), car.desired.pos) <= distanceBeforeNext) {
            index++;
            car.desired = new Common.positionAndTime(Global.road.Lanes[0].centerLine[index], car.localizeTime(0.1f));
        }
   }

   public object Copy() { return this.MemberwiseClone(); }
}


public class Main : MonoBehaviour
{
    
    List<ShallowDestinationSolver> solvers = new List<ShallowDestinationSolver>();
    public Car car;
    public Building building;
    Registry registry;
    
    void Start() {
        registry = new Registry();
        Global.registry = registry;
        Global.road = Global.registry.roads[0];
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
