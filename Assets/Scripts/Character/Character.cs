using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Character
{
    private readonly float MoveSpeed;
    private readonly float MobilityPower;
    private readonly float Stamina;

    private readonly Transform transform;
    private readonly NavMeshAgent navMeshAgent;
    private IState<Character> currentState;

    public Character(float moveSpeed, float mobilityPower, float stamina, Transform transform, NavMeshAgent navMeshAgent)
    {
        MoveSpeed = moveSpeed;
        MobilityPower = mobilityPower;
        Stamina = stamina;
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
