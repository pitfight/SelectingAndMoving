using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Character
{
    private readonly float MoveSpeed;
    private readonly float MobilityPower;
    private readonly float Stamina;

    private readonly MeshRenderer meshRenderer;
    private readonly MaterialRender materialRender;

    private IState<Character> currentState;

    public readonly int ID;
    public readonly Transform transform;
    public readonly NavMeshAgent navMeshAgent;

    public Vector3 CurrentDestination { get; private set; }

    public Character(float moveSpeed, float mobilityPower, float stamina, Transform transform, NavMeshAgent navMeshAgent, int index)
    {
        ID = index;

        MoveSpeed = moveSpeed;
        MobilityPower = mobilityPower;
        Stamina = stamina;
        this.transform = transform;
        this.navMeshAgent = navMeshAgent;

        SetParametersAgent(MoveSpeed, MobilityPower);

        meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
        materialRender = Resources.Load<MaterialRender>("MaterialData");
    }

    public void Move()
    {
        currentState.Update(this);
    }

    public void Group()
    {
        navMeshAgent.SetDestination(CurrentDestination);
    }

    public void SetNewDestination(Vector3 destination)
    {
        CurrentDestination = destination;
    }

    public void SetState(IState<Character> state)
    {
        if (currentState != null)
            currentState.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }

    public void SetLeaderMaterial()
    {
        meshRenderer.material = materialRender.Leader;
    }

    public void SetFollowerMaterial()
    {
        meshRenderer.material = materialRender.Follower;
    }

    private void SetParametersAgent(float speed, float mobility)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = mobility;
    }
}
