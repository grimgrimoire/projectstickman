using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public bool isPersistOnHit = true;
    public float speed = 10;
    public float height = 10;
    private Vector2 target;
    private Vector2 initialPosition;

    IEnumerator arrowMovement;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTargetPosition(Vector2 target)
    {
        this.target = target;
        if (isPersistOnHit)
        {
            arrowMovement = ArrowMovement();
            StartCoroutine(arrowMovement);
        }else
        {
            StartCoroutine(BulletMovement());
        }
    }

    IEnumerator ArrowMovement()
    {
        initialPosition = transform.position;
        Vector3 tempStart1 = transform.position;
        float hMovement = Mathf.Clamp(target.x - transform.position.x, -speed, speed);
        float compensation = Mathf.Abs((target.x - transform.position.x)) / Mathf.Abs(hMovement);
        float vMovement = compensation / 2 * height + (target.y - transform.position.y) / compensation;
        Vector2 diff;
        float angle;
        while (true)
        {
            transform.position = new Vector2(transform.position.x + (hMovement * Time.deltaTime), transform.position.y + (vMovement * Time.deltaTime));
            diff = transform.position - tempStart1;
            angle = Vector2.Angle(Vector2.right, diff);
            if (vMovement < 0)
                angle = -angle;
            transform.localEulerAngles = new Vector3(0, 0, angle);
            vMovement -= (Time.deltaTime * height);
            tempStart1 = transform.position;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator BulletMovement()
    {
        float delay = 0.5f;
        while (delay > 0)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target, 100 * Time.deltaTime);
            delay -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isPersistOnHit)
        {
            if (collider.gameObject.tag == "World")
            {
                StopCoroutine(arrowMovement);
                StartCoroutine(DestroySelf(3f));
            }
        }
    }

    IEnumerator DestroySelf(float dur)
    {
        yield return new WaitForSeconds(dur);
        Destroy(this.gameObject);
    }
}
