using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{



    public void FadeInSprite()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("test");
        float duration = 0.3F;
        float elapsedTime = 0.0F;
        Color initialColour = GetComponent<SpriteRenderer>().color;
        Color endColour = new Color(initialColour.r, initialColour.g, initialColour.b, 1.0F);
        while(duration > elapsedTime)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(initialColour, endColour, (elapsedTime / duration));
            Debug.Log(GetComponent<SpriteRenderer>().color);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
