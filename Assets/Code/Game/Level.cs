using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private bool isRunning = true;

    private void Update()
    {
        // This might display game over or whatever when level not running
    }

    public void EndLevel()
    {
        isRunning = false;
        ResetLevel();
    }

    public void ResetLevel()
    {
        new List<Human>(FindObjectsOfType<Human>()).ForEach(human => human.ResetToStart());
    }
}
