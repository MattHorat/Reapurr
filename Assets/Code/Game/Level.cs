using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevel;

    public void EndLevel()
    {
        foreach (Human human in FindObjectsOfType<Human>())
        {
            if (!human.asleep && FindObjectOfType<YawnController>().currentYawn != human)
            {
                StartCoroutine(Timer());
                return;
            }
        }
        FindObjectOfType<GameUI>().ShowWinScreen();
    }

    public void ResetLevel()
    {
        new List<Human>(FindObjectsOfType<Human>()).ForEach(human => human.ResetToStart());
        FindObjectOfType<InputController>().GetComponent<SpriteRenderer>().enabled = true;
        FindObjectOfType<InputController>().yawnController.currentYawn = null;
        FindObjectOfType<GameUI>().ResetUI();
    }

    public void PlayNextLevel()
    {
        levels[currentLevel].SetActive(false);
        currentLevel++;
        levels[currentLevel].SetActive(true);
        FindObjectOfType<ActionQueue>().ResetActionQueues();
        FindObjectOfType<GameUI>().ResetUI();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.0F);
        ResetLevel();
    }
}
