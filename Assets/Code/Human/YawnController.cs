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
