using UnityEngine;
using UnityEngine.UI;

public class ButtonClickLogger : MonoBehaviour
{
    // Reference to the buttons you want to track
    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        // Loop through each button and add a listener to log the button click
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => LogButtonClick(button));
        }
    }

    // Method to log button click
    private void LogButtonClick(Button clickedButton)
    {
        Debug.Log("Button clicked: " + clickedButton.name);
    }
}