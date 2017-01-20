using UnityEngine;

public class InputController : MonoBehaviour {
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public GameObject playerCharacter;
    public float speed;

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
        playerCharacter.GetComponent<Rigidbody2D>().AddForce(velocity.normalized * speed);
    }
}
