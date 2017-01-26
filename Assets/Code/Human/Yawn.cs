using UnityEngine;
using System.Collections;

public class Yawn : MonoBehaviour
{
    public float speed;
    public GameObject creator;
    public AudioClip[] yawnSounds;
    public GameObject yawnSprite;

    private Quaternion initialRotation;

    private void Start()
    {
       GameObject.Find("SoundEffects").GetComponent<AudioSource>().PlayOneShot(yawnSounds[Random.Range(0, yawnSounds.Length)]);
       initialRotation = yawnSprite.transform.rotation;
       Debug.Log(initialRotation);
       StartCoroutine(YawnGrow());
    }

    private void LateUpdate()
    {
        yawnSprite.transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, transform.position + transform.right * 10, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Human human = other.gameObject.GetComponent<Human>();
        if (human != null)
        {
            if (human.gameObject == creator)
            {
                return;
            }
            FindObjectOfType<InputController>().yawnController.currentYawn = human;
        }
        if (other.CompareTag("Wall") || other.CompareTag("Human"))
        {
            Destroy(gameObject, 0.3F);
            GetComponent<Collider2D>().enabled = false;
            ActionQueue actionQueue = FindObjectOfType<ActionQueue>();
            actionQueue.MarkComplete(this);
        }
    }

    private IEnumerator YawnGrow()
    {
        float duration = 0.6F;
        float elapsedTime = 0.0F;
        Vector2 originalSize = yawnSprite.transform.localScale;
        Vector2 endSize = new Vector2(1.5F, 1.5F);
        while(duration > elapsedTime)
        {
            yawnSprite.transform.localScale = Vector2.Lerp(originalSize, endSize, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
