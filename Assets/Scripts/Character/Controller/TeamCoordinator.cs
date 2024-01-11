using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.CharacterState;
using System.Linq;
using UnityEngine.AI;

public class TeamCoordinator : MonoBehaviour
{
    private Character currentLeader;
    private List<Character> group = new List<Character>();
    private Character[] followerGroup;

    [Header("Group formation")]
    [SerializeField] private GroupFormationType groupFormationType = GroupFormationType.Liner;
    [SerializeField] private float spacing = 2f;
    [SerializeField] private float searchRadius = 2.0f;
    [Header("References")]
    [SerializeField] private IsometricCamera isometricCamera;

    public void SetLeader(Character newLeader = null)
    {
        if (newLeader == null)
            currentLeader = group[0];
        else
            currentLeader = newLeader;
        currentLeader.SetState(new Leader(currentLeader));
        isometricCamera.SetTarget(currentLeader.transform);

        followerGroup = group.Where(element => !element.Equals(currentLeader)).ToArray();

        foreach (var follower in followerGroup)
        {
            follower.SetState(new Follow(currentLeader));
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
                foreach (var follower in followerGroup)
                {
                    follower.Move();
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
        for (int i = 0; i < followerGroup.Length; i++)
        {
            Vector3 targetPosition = leaderPosition + leaderDirection * (spacing * (i + 1));
            followerGroup[i].SetNewDestination(targetPosition);
            followerGroup[i].Group();
        }
    }

    private void FormationCircle()
    {
        float angleStep = 360.0f / followerGroup.Length;
        for (int i = 0; i < followerGroup.Length; i++)
        {
            float angle = angleStep * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Vector3 position = currentLeader.transform.position + direction * spacing;
            followerGroup[i].SetNewDestination(position);
            followerGroup[i].Group();
        }
    }

    private void FormationRandomly()
    {
        float radius = spacing * 2;

        foreach (var agent in followerGroup)
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
