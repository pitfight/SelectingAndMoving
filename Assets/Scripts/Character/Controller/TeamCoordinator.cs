using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StateMachine.CharacterState;

public class TeamCoordinator : MonoBehaviour
{
    private Character currentLeader;
    private List<Character> group = new List<Character>();

    [Header("Group formation")]
    [SerializeField] private GroupFormationType groupFormationType = GroupFormationType.Liner;
    [SerializeField] private float spacing = 2f;
    [SerializeField] private float searchRadius = 2.0f;
    [Header("References")]
    [SerializeField] private IsometricCamera isometricCamera;

    public void SetLeader(int index = 0)
    {
        currentLeader = group[index];

        currentLeader.SetState(new Leader(currentLeader));
        isometricCamera.SetTarget(currentLeader.transform);

        foreach (var follower in group)
        {
            if (follower.ID != currentLeader.ID)
            {
                follower.SetState(new Follow(currentLeader));
            }
        }
    }

    public void Add(Character unit)
    {
        group.Add(unit);
    }

    public void SetDestination(Vector3 destination)
    {
        if (currentLeader != null)
        {
            currentLeader.SetNewDestination(destination);
            currentLeader.Move();
        }
    }

    private void LateUpdate()
    {
        if (currentLeader != null && currentLeader.navMeshAgent.hasPath)
        {
            if (currentLeader.navMeshAgent.remainingDistance >= 0.5f)
            {
                foreach (var follower in group)
                {
                    if (follower.ID != currentLeader.ID)
                    {
                        follower.Move();
                    }
                }
            }
            else
            {
                currentLeader.navMeshAgent.ResetPath();
                switch (groupFormationType)
                {
                    case GroupFormationType.Liner:
                        FormationLiner();
                        break;
                    case GroupFormationType.Circle:
                        FormationCircle();
                        break;
                    case GroupFormationType.Randomly:
                        FormationRandomly();
                        break;
                }
            }
        }
    }

    private void FormationLiner()
    {
        Vector3 leaderPosition = currentLeader.transform.position;
        Vector3 leaderDirection = -currentLeader.transform.forward;
        int offset = 0;
        foreach (var follower in group)
        {
            if (follower.ID != currentLeader.ID)
            {
                Vector3 targetPosition = leaderPosition + leaderDirection * (spacing * (offset + 1));
                follower.SetNewDestination(targetPosition);
                follower.Group();
                offset++;
            }
        }
    }

    private void FormationCircle()
    {
        float angleStep = 360.0f / group.Count -1;
        int indexStep = 0;
        foreach (var follower in group)
        {
            if (follower.ID != currentLeader.ID)
            {
                float angle = angleStep * indexStep;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 position = currentLeader.transform.position + direction * spacing;
                follower.SetNewDestination(position);
                follower.Group();
                indexStep++;
            }
        }
    }

    private void FormationRandomly()
    {
        float radius = spacing * 2;
        foreach (var agent in group)
        {
            if (agent.ID != currentLeader.ID)
            {
                Vector3 randomDirection = Random.insideUnitSphere * radius;
                randomDirection += currentLeader.transform.position;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
                {
                    agent.SetNewDestination(hit.position);
                    agent.Group();
                }
            }
        }
    }
}
