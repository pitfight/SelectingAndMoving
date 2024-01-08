using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamCoordinator : MonoBehaviour
{
    private Character currentLeader;
    private List<Character> group = new List<Character>();

    public void SetLeader(Character newLeader)
    {
        currentLeader = newLeader;
    }

    public void Add(Character unit)
    {
        group.Add(unit);
    }
}
