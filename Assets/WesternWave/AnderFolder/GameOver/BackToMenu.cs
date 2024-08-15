using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Add this script to your button GameObject

    // Specify the scene name you want to go back to
    public string SampleScene;

    // Function to handle button click
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnBackButtonClick()
    {
        // Check if the target scene name is not empty
        if (!string.IsNullOrEmpty(SampleScene))
        {
            // Load the target scene
            SceneManager.LoadScene(SampleScene);
        }
        else
        {
            Debug.LogWarning("Target scene name is not set!");
        }
    }
}
