using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Character
{
    private readonly float MoveSpeed;
    private readonly float MobilityPower;
    private readonly float Stamina;

    private Transform transform;
    private NavMeshAgent navMeshAgent;
    private IState<Character> currentState;

    public Character(float moveSpeed, float mobilityPower, float stamina, IState<Character> currentState, Transform transform, NavMeshAgent navMeshAgent)
    {
        MoveSpeed = moveSpeed;
        MobilityPower = mobilityPower;
        Stamina = stamina;
        this.currentState = currentState;
        this.transform = transform;
        this.navMeshAgent = navMeshAgent;

        SetParametersAgent(MoveSpeed, MobilityPower);
    }

    public void SetState(IState<Character> state)
    {
        currentState = state;
    }

    private void SetParametersAgent(float speed, float mobility)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = mobility;
    }
}
