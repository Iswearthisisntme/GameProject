using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOnClick : MonoBehaviour
{
    //loads the first scene in the game
    public void StartGame(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
        Debug.Log("" + nextLevel + " loaded!");
    }
}
