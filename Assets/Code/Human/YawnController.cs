using UnityEngine;

public class YawnController : Actionable
{
    public Human currentYawn;
    public AudioSource yawnSound;

    public override void Action()
    {
        if (!currentYawn.asleep)
        {
            yawnSound.Play();
            Yawn yawn = Instantiate(currentYawn.yawnPrefab, currentYawn.transform.position, currentYawn.transform.rotation).GetComponent<Yawn>();
            yawn.GetComponentInChildren<SpriteFader>().FadeInSprite();
            FindObjectOfType<ActionQueue>().AddActionable(yawn);
            yawn.creator = currentYawn.gameObject;
            currentYawn.asleep = true;
            var emission = currentYawn.GetComponentInChildren<ParticleSystem>().emission;
            emission.enabled = true;
        }
    }
}
