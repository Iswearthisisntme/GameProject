using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;

    // String that has next level name
    public string nextLevel;

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
        //audio.clip = gameOverSFX;
        //audio.Play(); // Play the game over sound effect
        isGameOver = true;
        //gameText.text = "GAME OVER.";
        //gameText.gameObject.SetActive(true);
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelWon()
    {
        //audio.clip = gameWonSFX;
        //audio.Play(); // Play the game won sound effect
        isGameOver = true;
        //gameText.text = "YOU WIN!";
        //gameText.gameObject.SetActive(true);
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
