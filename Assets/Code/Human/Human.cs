using UnityEngine;

public class Human : Actionable {
    public float speed;
    public GameObject yawnPrefab;
    public bool asleep = false;
    
    private Vector2 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        //FindObjectOfType<ActionQueue>().AddYawnTarget(this);
    }

    private void Update ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    FindObjectOfType<ActionQueue>().NextAction();
        //}
    }

    private bool HasLineOfSight(GameObject targetObject)
    {
        Vector3 start = transform.position;
        Vector3 direction = (targetObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(targetObject.transform.position, transform.position);

        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, distance);
        if (sightTest.collider != null)
        {
            if (sightTest.collider.gameObject != gameObject && targetObject == sightTest.collider.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void SetMoveTarget(GameObject targetObject)
    {
        if (!asleep && HasLineOfSight(targetObject))
        {
            Vector2 localPositionTarget = targetObject.transform.position - gameObject.transform.position;
            Vector2 addedPosition;
            // We want to offset our position by an amount
            if (Mathf.Abs(localPositionTarget.x) > Mathf.Abs(localPositionTarget.y))
            {

                addedPosition = (localPositionTarget.x > 0) ? Vector2.left : Vector2.right;
            }
            else
            {
                addedPosition = (localPositionTarget.y > 0) ? Vector2.down : Vector2.up;
            }
            HumanMovement movement = gameObject.AddComponent<HumanMovement>();
            movement.targetPosition = (Vector2)targetObject.transform.position + addedPosition;
            movement.speed = speed;
            FindObjectOfType<ActionQueue>().AddActionable(movement);
            FaceTarget(targetObject);
        }
    }

    public void FaceTarget(GameObject targetObject)
    {
        if (!asleep && HasLineOfSight(targetObject))
        {
            Vector2 localPositionTarget = targetObject.transform.position - gameObject.transform.position;
            // If we are closer to the x axis, face based on x, otherwise y
            if (Mathf.Abs(localPositionTarget.x) > Mathf.Abs(localPositionTarget.y))
            {
                float rotation = (localPositionTarget.x > 0) ? 0 : 180;
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(rotation);
            }
            else
            {
                float rotation = (localPositionTarget.y > 0) ? 90 : 270;
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(rotation);
            }
        }
    }

    public override void Action()
    {
        if (!asleep)
        {
            Yawn yawn = Instantiate(yawnPrefab, transform.position, transform.rotation).GetComponent<Yawn>();
            Debug.Log("test");
            yawn.GetComponent<SpriteFader>().FadeInSprite();
            FindObjectOfType<ActionQueue>().AddActionable(yawn);
            yawn.creator = gameObject;
            asleep = true;
        }
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        asleep = false;
    }

    public void GhostInteracts()
    {
        //FindObjectOfType<ActionQueue>().AddYawnTarget(this);
    }
}
