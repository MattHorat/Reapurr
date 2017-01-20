using UnityEngine;

public class InputController : MonoBehaviour {
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;
    public GameObject playerCharacter;
    public float speed;
    private float interactRadius = 1F;

    // Update is called once per frame
    void Update () {
        Vector2 velocity = Vector2.zero;
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
        GetComponent<Rigidbody2D>().AddForce(velocity.normalized * speed);

        if (Input.GetKey(interact))
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
            foreach(Collider2D hitCollider in hitColliders)
            {
                if(hitCollider.gameObject != this.gameObject)
                {
                    hitCollider.gameObject.GetComponent<AttractorController>().InteractAttractor();
                }
            }
        }
        playerCharacter.GetComponent<Rigidbody2D>().AddForce(velocity.normalized * speed);
    }
}
