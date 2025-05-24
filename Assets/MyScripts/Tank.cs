using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{
    [Header("Position Variables")]
    public GameObject startPos;
    public GameObject MidPos;
    public GameObject endPos;

    public float headRotateSpeed, tankSpeed;
    public GameObject headObject;
    public GameObject missile, missilePosition, missileDestinationPosition;
    private Vector3 startPosition, MidPosition, endPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool check = false;
    private static Tank instance;
    public static Tank Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        startPosition = startPos.transform.position;
        MidPosition = MidPos.transform.position;
        endPosition = endPos.transform.position;
    }
    public void Attack()
    {
        check = true;
        StartCoroutine(ChangePositionA());
    }
    public void MissileAttack()
    {
        GameObject missileOBJ = Instantiate(missile, missilePosition.transform.position, missilePosition.transform.rotation);
        TankMissile missileScript = missileOBJ.GetComponent<TankMissile>();
        missileScript.destinationPos = missileDestinationPosition;
        missileScript.GoToAction();
    }
    IEnumerator ChangePositionA()
    {
        while(Vector3.Distance(transform.position, MidPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, MidPosition, Time.deltaTime*tankSpeed);
            yield return null;
        }
        StartCoroutine(RotateHead());
    }
    IEnumerator ChangePositionB()
    {
        while (Vector3.Distance(transform.position, endPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * tankSpeed);
            yield return null;
        }
        transform.position = startPosition;
    }
    IEnumerator RotateHead()
    {
       float angle= 0;
        while (angle >=-90)
        {
            angle -= Time.deltaTime * headRotateSpeed;
            headObject.transform.localEulerAngles = new Vector3(0, angle, 0);
            yield return null;
        }
        headObject.transform.localEulerAngles = new Vector3(0, -90f, 0);

        MissileAttack();
       
        angle = -90;
        while (angle <=0)
        {
            angle += Time.deltaTime * headRotateSpeed;
            headObject.transform.localEulerAngles = new Vector3(0,angle,0);
            yield return null;
        }
        headObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(ChangePositionB());
    }
}
