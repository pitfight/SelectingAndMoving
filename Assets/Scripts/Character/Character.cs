using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Character
{
    private readonly float MoveSpeed;
    private readonly float MobilityPower;
    private readonly float Stamina;

    private IState<Character> currentState;

    private Vector3 currentDestination;

    public readonly Transform transform;
    public readonly NavMeshAgent navMeshAgent;

    public Character(float moveSpeed, float mobilityPower, float stamina, Transform transform, NavMeshAgent navMeshAgent)
    {
        MoveSpeed = moveSpeed;
        MobilityPower = mobilityPower;
        Stamina = stamina;
        this.transform = transform;
        this.navMeshAgent = navMeshAgent;

        SetParametersAgent(MoveSpeed, MobilityPower);
    }

    public void Move()
    {
        navMeshAgent.SetDestination(currentDestination);
    }

    public void SetNewDestination(Vector3 destination)
    {
        currentDestination = destination;
    }

    public void SetState(IState<Character> state)
    {
        if (currentState != null)
            currentState.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }

    private void SetParametersAgent(float speed, float mobility)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = mobility;
    }
}
