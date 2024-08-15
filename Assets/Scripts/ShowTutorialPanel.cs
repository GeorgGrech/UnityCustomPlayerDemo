using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialPanel : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;

    public void ShowPanel(bool show)
    {
        tutorialPanel.SetActive(show);
    }
}
