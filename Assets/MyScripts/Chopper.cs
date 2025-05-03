using UnityEngine;
using System.Collections;

public class Chopper : MonoBehaviour
{
    [Tooltip("Rotation axis for the propeller.")]
    // Rotation axis for the propeller.
    public Vector3 axis = Vector3.up;

    [Tooltip("Speed at which the propeller rotates.")]
    // Speed at which the propeller rotates.
    public float rotationSpeed = 1000f;

    [Tooltip("Movement speed of the chopper.")]
    // Movement speed of the chopper.
    public float chopperSpeed = 5f;

    [Tooltip("GameObject representing the rotating propellers.")]
    // GameObject representing the rotating propellers.
    public GameObject propellers;

    [Tooltip("Starting point of the chopper's movement.")]
    // Starting point of the chopper's movement.
    public GameObject startPos;

    [Tooltip("Destination point of the chopper's movement.")]
    // Destination point of the chopper's movement.
    public GameObject destPos;

    private float normalAngle = 55f;
    private float turnAngle = 245f;

    private Coroutine fanCoroutine;
    private Vector3 currentTarget;
    public bool check, _check;
    public static Chopper Instance
    {
        get
        {
            return instance;
        }
    }
    private static Chopper instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindAnyObjectByType<Chopper>();
        }
        else
        {
            Destroy(gameObject);
        }
        currentTarget = destPos.transform.position;
        StartChopperFan();
    }


    /// <summary>
    /// Starts the coroutine that rotates the chopper's fan.
    /// </summary>
    public void StartChopperFan()
    {
        if (fanCoroutine == null)
        {
            fanCoroutine = StartCoroutine(StartFan());
        }
    }

    /// <summary>
    /// Coroutine to rotate the chopper's propeller.
    /// </summary>
    private IEnumerator StartFan()
    {
        while (true)
        {
            propellers.transform.Rotate(axis * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// Stops the propeller rotation.
    /// </summary>
    public void StopChopperFan()
    {
        if (fanCoroutine != null)
        {
            StopCoroutine(fanCoroutine);
            fanCoroutine = null;
        }
    }
    /// <summary>
    /// Moves the chopper between start and destination points.
    /// </summary>
    public void MoveChopper(GameObject destPos)
    {
        check = true;
        StartCoroutine(Move(destPos));
    }
    IEnumerator Move(GameObject destPosition) {
        while (check)
        {
            currentTarget = new Vector3(destPosition.transform.position.x, destPosition.transform.position.y, destPosition.transform.position.z-5);
            Vector3 direction = (currentTarget - transform.position).normalized;
            transform.position += direction * chopperSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, currentTarget) > 0.1f)
            {
                yield return null;
            }
            else
            {
                //Instantiate();
                check = false;
            }
        }
    }
    public void GoToParking()
    {
        _check = true;
        StartCoroutine(Park());
    }
    IEnumerator Park()
    {
        while (_check)
        {
            currentTarget = destPos.transform.position;
            Vector3 direction = (currentTarget - transform.position).normalized;
            transform.position += direction * chopperSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, currentTarget) > 0.1f)
            {
                yield return null;
            }
            else
            {
                transform.position = startPos.transform.position;
                _check = false;
            }
        }
    }
}
