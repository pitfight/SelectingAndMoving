using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class UnitManagementSystem : MonoBehaviour
{
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
    [SerializeField] private CharacterSelectionBar selectionBar;
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
            var character = CreateUnit(characterPrefab, spawnPosition, index);
            teamCoordinator.Add(character);

            if (index == 0)
                selectionBar.CreateButton(teamCoordinator, index, false);
            else
                selectionBar.CreateButton(teamCoordinator, index);

            index++;
            spawnPosition.x += 2f;
        }

        teamCoordinator.SetLeader(0);
    }

    private Character CreateUnit(GameObject prefab, Vector3 position, int index)
    {
        var unit = Instantiate(prefab);
        unit.transform.position = position;

        int characterIndex = index + 1;
        var text = Utils.CreateWorldText(unit.transform, characterIndex.ToString(), new Vector3(0f, 1.5f, 0f), 15, Color.cyan, TMPro.TextAlignmentOptions.Midline, 999);
        text.transform.localScale = new Vector3(-1f, 1f, 1f);
        SetObjectLookToCamera(text.gameObject);

        var agent = unit.GetComponent<NavMeshAgent>();
        agent.avoidancePriority = index * 10;

        System.Random random = new System.Random();

        float speed = GenerateStatModifier(unitsSpeed, random);
        float mobility = GenerateStatModifier(unitsMobility, random);
        float stamina = GenerateStatModifier(unitsStamina, random);

        var character = new Character(speed, mobility, stamina, unit.transform, agent, index);

        return character;
    }

    private float GenerateStatModifier(float stat, System.Random random)
    {
        return stat * (float)(random.NextDouble() * (maxModifier - minModifier) + minModifier);
    }

    private void SetObjectLookToCamera(GameObject obj)
    {
        var lookAtConstraint = obj.AddComponent<LookAtConstraint>();
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1;
        lookAtConstraint.AddSource(source);
        lookAtConstraint.constraintActive = true;
    }
}
