using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitManagementSystem : MonoBehaviour
{
    private System.Random random = new System.Random();

    [Header("Prototype settings")]
    [SerializeField] private float minModifier = 0.9f;
    [SerializeField] private float maxModifier = 1.1f;
    [SerializeField] private float unitsSpeed = 4f;
    [SerializeField] private float unitsMobility = 120f;
    [SerializeField] private float unitsStamina = 15f;
    [Range(3, 9)][SerializeField] private int countCharacters = 3;

    [Header("Prefabs")]
    [SerializeField] private GameObject characterPrefab;

    [Header("Game references")]
    [SerializeField] private ButtonBar buttonBar;
    [SerializeField] private TeamCoordinator teamCoordinator;

    [Header("Position references")]
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        SpawnUnits();
    }

    public void SpawnUnits()
    {
        int index = 0;
        Vector3 spawnPosition = spawnPoint.position;

        while (index < countCharacters)
        {
            var character = CreateUnit(characterPrefab, spawnPosition);
            teamCoordinator.Add(character);
            index++;
            buttonBar.CreateButton(character, teamCoordinator, index);

            spawnPosition.x += 2f;
        }
    }

    private Character CreateUnit(GameObject prefab, Vector3 position)
    {
        var unit = Instantiate(prefab);
        unit.transform.position = position;

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
