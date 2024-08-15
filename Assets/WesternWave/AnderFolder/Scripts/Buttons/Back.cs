using UnityEngine;

public class Back : MonoBehaviour
{
    public GameObject buttonsFolder;   
    public GameObject controlsTextFolder;   
    public void OnBackButtonClick()
    {
        buttonsFolder.SetActive(true);
        controlsTextFolder.SetActive(false);
    }
}
