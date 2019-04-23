using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame1 : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
