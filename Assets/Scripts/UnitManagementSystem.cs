using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitManagementSystem : MonoBehaviour
{
    private System.Random random = new System.Random();

    [Header("Unit stats")]
    [SerializeField] private float minModifier = 0.9f;
    [SerializeField] private float maxModifier = 1.1f;
    [SerializeField] private float unitsSpeed = 4f;
    [SerializeField] private float unitsMobility = 120f;
    [SerializeField] private float unitsStamina = 15f;

    [Header("Prefabs")]
    [Min(3)][SerializeField] private int countCharacters = 3;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject characterPrefab;

    [Header("Game references")]
    [SerializeField] private TeamCoordinator teamCoordinator;

    public void SpawnUnits()
    {
        int index = 0;
        while (index < 3)
        {
            var character = CreateUnit(characterPrefab);
            teamCoordinator.Add(character);
            if (index == 0)
                teamCoordinator.SetLeader(character);
            index++;
        }
    }

    private Character CreateUnit(GameObject prefab)
    {
        var unit = Instantiate(prefab);
        var agent = unit.GetComponent<NavMeshAgent>();

        float speed = GenerateStatModifier(unitsSpeed);
        float mobility = GenerateStatModifier(unitsMobility);
        float stamina = GenerateStatModifier(unitsStamina);

        var character = new Character(speed, mobility, stamina, unit.transform, agent);

        return character;
    }

    private float GenerateStatModifier(float stat)
    {
        return stat * (float)(random.NextDouble() * (maxModifier - minModifier) + minModifier);
    }
}
