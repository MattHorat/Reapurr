using UnityEngine;

public class HumanMovement : MonoBehaviour {
    public float speed;
    public Vector2 targetPosition;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetPosition) < 0.1)
        {
            FindObjectOfType<ActionQueue>().MarkComplete(this);
            Destroy(this);
        }
    }
}
