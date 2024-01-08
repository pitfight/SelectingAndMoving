using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.CharacterState;
using System.Linq;

public class TeamCoordinator : MonoBehaviour
{
    private Character currentLeader;
    private List<Character> group = new List<Character>();

    public void SetLeader(Character newLeader)
    {
        currentLeader = newLeader;
        currentLeader.SetState(new Leader(currentLeader));

        var followers = group.Where(element => !element.Equals(currentLeader));

        foreach (var follower in followers)
        {
            follower.SetState(new Follow(currentLeader));
        }
    }

    public void Add(Character unit)
    {
        group.Add(unit);
    }
}
