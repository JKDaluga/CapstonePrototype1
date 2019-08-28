using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ice3 : WaterState, IPooledObject
{
    private Transform target;
    private Rigidbody body;
    private bool beingHeld = false;

    // Start is called before the first frame update
    private void Awake()
    {
        target = null;
        beingHeld = false;
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (beingHeld)
        {
            Vector3 distance = target.position - transform.position;
            if (distance.magnitude > 5)
            {
                DropObj();
                body.velocity = Vector3.ClampMagnitude(body.velocity, 5);
            }
            else if (distance.magnitude > 0.05f)
            {
                Vector3 direction = Vector3.ClampMagnitude(distance * Mathf.Max(distance.sqrMagnitude, 10), 25);
                Vector3 velocity = Vector3.Lerp(body.velocity, direction, 0.125f);
                velocity.y = body.velocity.y;
                body.velocity = velocity;
            }
            else if (body.velocity.magnitude > 0)
            {
                body.velocity = body.velocity * 0.01f;
            }
        }
    }

    public override void Interact(GameObject player)
    {
        if (!beingHeld && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PickUpObj(Camera.main.transform.GetChild(0), player));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DropObj();
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.WATER_KEY, transform);
            obj.transform.localScale = new Vector3(2, 0.2f, 2);
            gameObject.SetActive(false);
        }
    }

    public void OnObjectSpawn()
    {
        if (!body) body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        Vector3 scale = transform.localScale;
        body.mass = (scale.x * scale.y * scale.z) * 5;
        beingHeld = false;
        target = null;
    }

    private IEnumerator PickUpObj(Transform targetPos, GameObject player)
    {
        beingHeld = true;
        target = targetPos;
        yield return new WaitForSeconds(0.05f);
        player.GetComponent<PlayerController>().holdingObj = true;
        EventCallbacks.EventSystem.Current.RegisterListener<EventCallbacks.dropEvent>(DropObjEvent);
        EventCallbacks.EventSystem.Current.RegisterListener<EventCallbacks.pushEvent>(PushObjEvent);
    }

    private void DropObjEvent(EventCallbacks.dropEvent e)
    {
        if (beingHeld)
        {
            DropObj();
            body.velocity = Vector3.ClampMagnitude(body.velocity, 5);
        }
    }

    private void PushObjEvent(EventCallbacks.pushEvent e)
    {
        if (beingHeld)
        {
            DropObj();
            Vector3 direction = Vector3.Normalize(transform.position - Camera.main.transform.position);
            body.AddForce(Vector3.ClampMagnitude(direction * body.mass * 10, 25), ForceMode.Impulse);
        }
    }

    private void DropObj()
    {
        beingHeld = false;
        target = null;
        EventCallbacks.EventSystem.Current.UnregisterListener<EventCallbacks.dropEvent>(DropObjEvent);
        EventCallbacks.EventSystem.Current.UnregisterListener<EventCallbacks.pushEvent>(PushObjEvent);
    }
}