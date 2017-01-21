using UnityEngine;

public class InputController : MonoBehaviour {
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;
    public GameObject playerCharacter;
    public float speed;
    private float interactRadius = 0.5F;
    public YawnController yawnController;
    private Vector2 velocity = Vector2.zero;

    private void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
        velocity = Vector2.zero;
		if (Input.GetKey(up))
        {
            velocity += Vector2.up;
        }
        if (Input.GetKey(down))
        {
            velocity += Vector2.down;
        }
        if (Input.GetKey(left))
        {
            velocity += Vector2.left;
        }
        if (Input.GetKey(right))
        {
            velocity += Vector2.right;
        }

        if (Input.GetKeyDown(interact))
        {
            bool foundAttractor = false;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
            foreach(Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject != this.gameObject)
                {
                    if (hitCollider.CompareTag("Attractor"))
                    {
                        foundAttractor = true;
                        hitCollider.gameObject.GetComponent<AttractorController>().InteractAttractor();
                    }
                }
            }
            if (!foundAttractor)
            {
                foreach (Collider2D hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Human"))
                    {
                        hitCollider.gameObject.GetComponent<Human>().GhostInteracts();
                        GetComponent<SpriteRenderer>().enabled = false;
                        yawnController.currentYawn = hitCollider.gameObject.GetComponent<Human>();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<ActionQueue>().AddAction(yawnController);
            FindObjectOfType<GameUI>().AssignYawn();
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(velocity.normalized * speed);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}
