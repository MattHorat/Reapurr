using UnityEngine;

public class Yawn : MonoBehaviour
{
    public float speed;
    public GameObject creator;

    private void Update()
    {
        GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, transform.position + transform.forward * 10, speed * Time.deltaTime);
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
        }
        if (other.CompareTag("Wall") || other.CompareTag("Human"))
        {
            Destroy(gameObject);
            ActionQueue actionQueue = FindObjectOfType<ActionQueue>();
            actionQueue.AddYawnTarget(human);
            actionQueue.MarkComplete(this);
        }
    }
}
