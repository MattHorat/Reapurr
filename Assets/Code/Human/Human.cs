using UnityEngine;

public class Human : MonoBehaviour {
    public float speed;
    public GameObject yawnPrefab;

    private Vector2 targetPosition;
    private bool asleep = false;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update ()
    {
        GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Yawn();
        }
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
        if (HasLineOfSight(targetObject))
        {
            targetPosition = targetObject.transform.position;
        }
    }

    public void FaceTarget(GameObject targetObject)
    {
        if (HasLineOfSight(targetObject))
        {
            Vector2 localPositionTarget = targetObject.transform.position - gameObject.transform.position;
            // If we are closer to the x axis, face based on x, otherwise y
            if (Mathf.Abs(localPositionTarget.x) > Mathf.Abs(localPositionTarget.y))
            {
                float rotation = (localPositionTarget.x > 0) ? 90 : -90;
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(rotation);
            }
            else
            {
                float rotation = (localPositionTarget.y > 0) ? 180 : -180;
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(rotation);
            }
        }
    }

    public void Yawn()
    {
        if (!asleep)
        {
            Yawn yawn = Instantiate(yawnPrefab, transform.position, transform.rotation).GetComponent<Yawn>();
            yawn.creator = gameObject;
            asleep = true;
        }
    }
}
