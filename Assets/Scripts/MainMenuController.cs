using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject choseLevelPanel;
    [Space]
    [SerializeField] private List<Button> choseLevelbuttons = new List<Button>();
    [SerializeField] private List<Button> lastLevelbuttons = new List<Button>();
    [SerializeField] private List<Button> exitbuttons = new List<Button>();
    [SerializeField] private List<Button> levelbuttons = new List<Button>();
    [SerializeField] private List<Button> backButtons = new List<Button>();


    private void Awake()
    {
        mainPanel.SetActive(true);
        choseLevelPanel.SetActive(false);

        foreach (Button button in choseLevelbuttons) button.onClick.AddListener(OpenLevelLayout);

        foreach (Button button in lastLevelbuttons) button.onClick.AddListener(StartLevel);
       
        foreach (Button button in exitbuttons) button.onClick.AddListener(Exit);
        
        foreach (Button button in levelbuttons) button.onClick.AddListener(StartLevel);
        
        foreach (Button button in backButtons) button.onClick.AddListener(Back);
    }

    private void OpenLevelLayout()
    {
        mainPanel.SetActive(false);
        choseLevelPanel.SetActive(true);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Back()
    {
        mainPanel.SetActive(true);
        choseLevelPanel.SetActive(false);
    }

}

