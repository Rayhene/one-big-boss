using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currentlyRunningState;

    public void ReciveState(State state)
    {

        if (currentlyRunningState != null)
        {
            currentlyRunningState.onStateOver();
        }

        currentlyRunningState = state;
        currentlyRunningState.onStateBegin();
    }
    public void ExecuteState()
    {

        if (currentlyRunningState == null)
            Debug.LogError("State not created");

        if (currentlyRunningState.stateOver)
        {
            currentlyRunningState.onStateOver();
            currentlyRunningState = null;

        }
        else
        {
            currentlyRunningState.onStateUpdate();
        }



    }

}

public class State
{
    public bool stateOver;

    public virtual void onStateBegin()
    {

    }

    public virtual void onStateUpdate()
    {

    }

    public virtual void onStateOver()
    {

    }
}
public class ChasePlayer : State
{
    private Boss self;
    private Transform target;
    private float spd;
    private GameObject projectile;

    private int shootCount = 0;

    public ChasePlayer(Boss self, Transform target, float spd, GameObject projectile)
    {
        this.self = self;
        this.target = target;
        this.spd = spd;
        this.projectile = projectile;

    }
    public override void onStateBegin()
    {

    }
    public override void onStateUpdate()
    {


        Vector2 dir = (target.position - self.transform.position).normalized;

        self.transform.position = self.transform.position + new Vector3(dir.x * spd, dir.y * spd, 0);

        if (self.Shoot(projectile, dir))
            stateOver = true;


    }
    public override void onStateOver()
    {

    }


}

public class IdleState : State
{
    private float stateDuration;

    public IdleState(float stateDuration)
    {
        this.stateDuration = stateDuration;

    }
    public override void onStateBegin()
    {
        base.onStateBegin();
    }
    public override void onStateUpdate()
    {
        stateDuration -= Time.deltaTime;
        if (stateDuration <= 0)
            stateOver = true;

    }
    public override void onStateOver()
    {
        base.onStateOver();
    }

}
public class ChasePlayer2 : State
{
    private Boss self;
    private Transform target;
    private float spd;
    private GameObject projectile;

    public ChasePlayer2(Boss self, Transform target, float spd, GameObject projectile)
    {
        this.self = self;
        this.target = target;
        this.spd = spd;
        this.projectile = projectile;

    }
    public override void onStateBegin()
    {

    }
    public override void onStateUpdate()
    {


        Vector2 dir = (target.position - self.transform.position).normalized;

        self.transform.position = self.transform.position + new Vector3(dir.x * spd, dir.y * spd, 0);

        if (self.Shoot(projectile, dir))
            stateOver = true;

    }
    public override void onStateOver()
    {

    }


}



