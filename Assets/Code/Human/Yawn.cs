using UnityEngine;

public class Yawn : MonoBehaviour
{
    public float speed;
    public GameObject creator;
    public AudioClip[] yawnSounds;

    private void Start()
    {
       GameObject.Find("SoundEffects").GetComponent<AudioSource>().PlayOneShot(yawnSounds[Random.Range(0, yawnSounds.Length)]);
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
}
