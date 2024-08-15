using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject buttonsFolder;   
    public GameObject controlsTextFolder;   

    public void OnControlsButtonClick()
    {
        buttonsFolder.SetActive(false);
        controlsTextFolder.SetActive(true);
    }
}
