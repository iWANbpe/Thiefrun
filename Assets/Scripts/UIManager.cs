using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxTrickScore;
    [Space]
    [SerializeField] private List<Button> restartButtons = new List<Button>(); 
    [SerializeField] private List<Button> toMenuButtons = new List<Button>();
    [SerializeField] private List<Button> nextLevelButtons = new List<Button>();
    [Space]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject trickScoreText;

    private int trickScore;
    public int TrickScore
    {
        set { trickScore = value; }
        get {return trickScore; }
    }

    void Awake()
    {
        foreach(Button button in restartButtons)
        {
            button.onClick.AddListener(Restart);
        }

        foreach (Button button in toMenuButtons)
        {
            button.onClick.AddListener(ToMenu);
        }

        foreach (Button button in nextLevelButtons)
        {
            button.onClick.AddListener(NextLevel);
        }
        
        trickScore = 0;
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void NextLevel()
    {

    }

    public void EnebleLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void EnebleWinPanel()
    {
        trickScoreText.GetComponent<TMP_Text>().text = $"{trickScore} / {maxTrickScore}";
        winPanel.SetActive(true);
    }
}
