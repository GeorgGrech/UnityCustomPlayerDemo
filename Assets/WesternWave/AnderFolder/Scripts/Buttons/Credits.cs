using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    // Drag and drop the folders in the Inspector
    public GameObject Buttons;
    public GameObject ControlsText;
    public GameObject CreditsText;
    public GameObject TrainSounds;

    public void OnCreditsButtonClick()
    {
        // Check if folders are assigned
        if (Buttons != null && ControlsText != null && CreditsText != null && TrainSounds != null)
        {
            // Deactivate folders and train sounds
            Buttons.SetActive(false);
            ControlsText.SetActive(false);
            TrainSounds.SetActive(false);

            // Activate the target folder
            CreditsText.SetActive(true);
        }
        else
        {
            Debug.LogWarning("One or more folders are not assigned!");
        }
    }

    // Function to handle "Back" button click
    public void OnBackButtonClick()
    {
        // Check if folders are assigned
        if (Buttons != null && ControlsText != null && CreditsText != null && TrainSounds != null)
        {
            // Deactivate current folder (CreditsText)
            CreditsText.SetActive(false);

            // Activate folders and train sounds
            Buttons.SetActive(true);
            ControlsText.SetActive(false);
            TrainSounds.SetActive(true);
        }
        else
        {
            Debug.LogWarning("One or more folders are not assigned!");
        }
    }
}
