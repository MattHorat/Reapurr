using UnityEngine;

public class Human : MonoBehaviour {
    public float speed;

    private Vector2 targetPosition;
    public GameObject targetObject;

    private void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetMoveTarget(targetObject);
        }
    }

    public void SetMoveTarget(GameObject targetObject)
    {
        Vector3 start = transform.position;
        Vector3 direction = (targetObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(targetObject.transform.position, transform.position);

        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, distance);
        if (sightTest.collider != null)
        {
            if (sightTest.collider.gameObject != gameObject && targetObject == sightTest.collider.gameObject)
            {
                targetPosition = targetObject.transform.position;
            }
        }
    }
}
