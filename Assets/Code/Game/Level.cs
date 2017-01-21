using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public void EndLevel()
    {
        foreach (Human human in FindObjectsOfType<Human>())
        {
            if (!human.asleep && FindObjectOfType<YawnController>().currentYawn != human)
            {
                ResetLevel();
                return;
            }
        }
        // TODO Display a YOU WIN MESSAGE!
        FindObjectOfType<GameUI>().ShowWinScreen();
    }

    public void ResetLevel()
    {
        new List<Human>(FindObjectsOfType<Human>()).ForEach(human => human.ResetToStart());
        FindObjectOfType<InputController>().GetComponent<SpriteRenderer>().enabled = true;
        FindObjectOfType<InputController>().yawnController.currentYawn = null;
        FindObjectOfType<GameUI>().ResetUI();
    }
}
