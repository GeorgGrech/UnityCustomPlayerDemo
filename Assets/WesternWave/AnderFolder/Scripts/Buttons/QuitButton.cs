using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Function to be called when the quit button is clicked
    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
