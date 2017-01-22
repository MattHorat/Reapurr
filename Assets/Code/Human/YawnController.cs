using System.Collections;
using UnityEngine;

public class YawnController : Actionable
{
    public Human currentYawn;
    public Yawn yawn;

    public override void Action()
    {
        if (!currentYawn.asleep)
        {
            yawn = Instantiate(currentYawn.yawnPrefab, currentYawn.transform.position, currentYawn.transform.rotation).GetComponent<Yawn>();
            FindObjectOfType<ActionQueue>().AddActionable(yawn);
            foreach (GameObject sprites in currentYawn.directionSprites)
            {
                SpriteRenderer[] eyeSprites = sprites.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer eye in eyeSprites)
                {
                    if (eye.tag == "Face")
                    {
                        eye.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            currentYawn.yawnSprite.GetComponent<SpriteRenderer>().enabled = true;
            currentYawn.yawnSprite.GetComponent<Animator>().SetTrigger("MakeYawn");
            yawn.creator = currentYawn.gameObject;
            StartCoroutine(StartYawn());
        }
    }

    private IEnumerator StartYawn()
    {
        yield return new WaitForSeconds(0.8F);
        Debug.Log(yawn.gameObject);
        yawn.gameObject.SetActive(true);
        yawn.GetComponentInChildren<SpriteFader>().FadeInSprite();
        currentYawn.asleep = true;
        var emission = currentYawn.GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = true;
    }
}
