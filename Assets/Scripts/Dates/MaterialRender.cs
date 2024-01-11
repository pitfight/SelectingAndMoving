using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Materials")]
public class MaterialRender : ScriptableObject
{
    public Material Leader;
    public Material Follower;
}
