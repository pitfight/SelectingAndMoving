using UnityEngine;

public class ButtonBar : MonoBehaviour
{
    [SerializeField] private CharacterSelectionButton selectionButtonPrefab;

    public void CreateButton(Character character, TeamCoordinator teamCoordinator, int index)
    {
        var button = Instantiate(selectionButtonPrefab, transform);
        button.SetIndex(index);
        button.Bind(character, teamCoordinator);
    }
}
