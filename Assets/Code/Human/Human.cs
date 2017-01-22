using UnityEngine;

public class Human : Actionable {
    public float speed;
    public GameObject yawnPrefab;
    public bool asleep = false;
    public AudioClip[] exclamationSounds;
    
    private Vector2 startPosition;
    private Quaternion startRotation;

    public GameObject[] directionSprites;

    private void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        //FindObjectOfType<ActionQueue>().AddYawnTarget(this);
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

    public void SetMoveDirectionSprite()
    {
        Debug.Log("test");
        if (Mathf.Abs(transform.right.x) < Mathf.Abs(transform.right.y)) //x > 0
        {
            if(transform.right.y > 0)
            {
                foreach(GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                }
                Debug.Log("left");
                directionSprites[1].SetActive(true);
                //left
            }
            else
            {
                foreach (GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                }
                Debug.Log("right");
                directionSprites[3].SetActive(true);
                //right
            }
        }
        else
        {
            if(transform.right.x > 0)
            {
                foreach (GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                }
                directionSprites[2].SetActive(true);
                Debug.Log("up");
                //up
            }
            else
            {
                foreach (GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                }
                directionSprites[0].SetActive(true);
                Debug.Log("down");
                //down
            }
        }
        Quaternion rotation = directionSprites[0].transform.parent.transform.parent.rotation;
        //Debug.Log(rotation);
        // Debug.Log(Quaternion.Inverse(rotation));
        directionSprites[0].transform.parent.transform.rotation = Quaternion.Inverse(rotation);
    }

    public void FaceTarget(GameObject targetObject)
    {
        if (!asleep && HasLineOfSight(targetObject))
        {
            GetComponent<AudioSource>().PlayOneShot(exclamationSounds[Random.Range(0, exclamationSounds.Length)]);
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
        SetMoveDirectionSprite();
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
        var emission = GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = false;
        asleep = false;
    }

    public void GhostInteracts()
    {
        //FindObjectOfType<ActionQueue>().AddYawnTarget(this);
    }
}
