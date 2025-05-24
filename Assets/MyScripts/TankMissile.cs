using UnityEngine;
using System.Collections;

public class TankMissile : MonoBehaviour
{
    public GameObject destinationPos;
    public GameObject explosion;
    public float missileSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToAction()
    {
        StartCoroutine(Go());
    }
    IEnumerator Go()
    {
        while (Vector3.Distance(transform.position, destinationPos.transform.position) > 0.001)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos.transform.position, missileSpeed* Time.deltaTime);
            yield return null;
        }
        Instantiate(explosion, destinationPos.transform.position, destinationPos.transform.rotation);
        gameObject.SetActive(false);
        Tank.Instance.check = false;
        Destroy(gameObject, 1.0f);
    }
}
