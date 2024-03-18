using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;

    // String that has next level name
    public string nextLevel;
    public Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER.";
        gameText.gameObject.SetActive(true);
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelWon()
    {
        isGameOver = true;
        gameText.text = "LEVEL COMPLETE.";
        gameText.gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
