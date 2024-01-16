using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI index;
    [SerializeField] private Button button;

    public void Bind(Character character, TeamCoordinator teamCoordinator)
    {
        button.onClick.AddListener( delegate {
            teamCoordinator.SetLeader(character);
        });
    }

    public void SetIndex(int index)
    {
        this.index.text = "" + index;
    }
}
