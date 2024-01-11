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

    [SerializeField] private IsometricCamera isometricCamera;

    private Character[] followerGroup;

    public void SetLeader(Character newLeader)
    {
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

            foreach (var follower in followerGroup)
            {
                follower.SetNewDestination(currentLeader.transform.position);
            }

            foreach (var character in group)
            {
                character.Move();
            }
        }
    }

    private void LateUpdate()
    {
        if (currentLeader != null && currentLeader.navMeshAgent.hasPath)
        {
            foreach (var follower in followerGroup)
            {
                follower.SetNewDestination(currentLeader.transform.position);
                follower.Move();
            }
        }
    }
}
