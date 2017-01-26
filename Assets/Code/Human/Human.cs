using UnityEngine;

public class Human : Actionable {
    public float speed;
    public GameObject yawnPrefab;
    public bool asleep = false;
    public AudioClip[] exclamationSounds;
    public int startingDirection;
    
    private Vector2 startPosition;
    private Quaternion startRotation;

    public GameObject[] directionSprites;
    public GameObject yawnObject;
    private Quaternion initialRotation;

    public Sprite originalSprite;
    public GameObject hair;
    public GameObject eyes;

    private void LateUpdate()
    {
        directionSprites[0].transform.parent.transform.rotation = initialRotation;
    }

    private void Start()
    {
        SetMoveDirectionSprite(startingDirection);
        startRotation = transform.rotation;
        startPosition = transform.position;
        initialRotation = directionSprites[0].transform.parent.transform.rotation;
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

    public void SetMoveDirectionSprite(int direction)
    {
        switch (direction) {
            case 1:
                foreach(GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                    sprites.GetComponent<Animator>().enabled = false;
                }
                directionSprites[3].SetActive(true);
                break;
                //left
            case 2:
                foreach (GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                    sprites.GetComponent<Animator>().enabled = false;
                }
                directionSprites[1].SetActive(true);
                //right
                break;
            case 0:
            case 4:
                foreach (GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                    sprites.GetComponent<Animator>().enabled = false;
                }
                directionSprites[0].SetActive(true);
                //up
                break;
            case 3:
                foreach (GameObject sprites in directionSprites)
                {
                    sprites.SetActive(false);
                    sprites.GetComponent<Animator>().enabled = false;
                }
                directionSprites[2].SetActive(true);
                //down
                break;
            }
    }

    public void FaceTarget(GameObject targetObject)
    {
        if (!asleep && HasLineOfSight(targetObject))
        {
            GetComponent<AudioSource>().PlayOneShot(exclamationSounds[Random.Range(0, exclamationSounds.Length)]);
            Vector2 localPositionTarget = targetObject.transform.position - gameObject.transform.position;
            // If we are closer to the x axis, face based on x, otherwise y
            float rotation = 0;
            if (Mathf.Abs(localPositionTarget.x) > Mathf.Abs(localPositionTarget.y))
            {
                if (localPositionTarget.x > 0)
                {
                    rotation = 0;
                    SetMoveDirectionSprite(1);
                }
                else
                {
                    rotation = 180;
                    SetMoveDirectionSprite(2);
                }
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(rotation);
            }
            else
            {
                if (localPositionTarget.y > 0)
                {
                    rotation = 90;
                    SetMoveDirectionSprite(3);
                }
                else
                {
                    rotation = 270;
                    SetMoveDirectionSprite(4);
                }
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(rotation);
            }
        }
    }

    public override void Action()
    {
        if (!asleep)
        {
            Yawn yawn = Instantiate(yawnPrefab, transform.position, transform.rotation).GetComponent<Yawn>();
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
        SetMoveDirectionSprite(startingDirection);
        yawnObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
        hair.SetActive(true);
        eyes.SetActive(true);
        yawnObject.SetActive(false);
    }

    public void GhostInteracts()
    {
        //FindObjectOfType<ActionQueue>().AddYawnTarget(this);
    }

    public void PlayYawn()
    {
        foreach (GameObject sprites in directionSprites)
        {
            sprites.SetActive(false);
            sprites.GetComponent<Animator>().enabled = false;
        }
        yawnObject.SetActive(true);
    }
}
