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
    }
}
