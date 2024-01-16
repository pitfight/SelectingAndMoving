using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelectionBar : MonoBehaviour
{
    private VisualElement bar;
    private List<Button> buttons = new List<Button>();

    private void Awake()
    {
        bar = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("CharacterBar");
    }

    public void CreateButton(TeamCoordinator teamCoordinator, int index, bool enable = true)
    {
        Button newButton = new Button();
        newButton.AddToClassList("character-selector-button");
        newButton.SetEnabled(enable);

        var text = index + 1;
        Label labelText = new Label(text.ToString());
        labelText.AddToClassList("character-selection-button-text");

        newButton.clicked += delegate ()
        {
            teamCoordinator.SetLeader(index);

            foreach (Button button in buttons)
            {
                if (!button.enabledSelf)
                    button.SetEnabled(true);
            }

            newButton.SetEnabled(false);
        };

        newButton.Add(labelText);
        buttons.Add(newButton);
        bar.Add(newButton);
    }
}
