using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooks : MonoBehaviour
{
    LineRenderer line;
    HookRotation hookRotation;
    [SerializeField] public GameObject player;

    [SerializeField] LayerMask grappleMask;
    [SerializeField] public float maxDistance = 10f;
    [SerializeField] public float grappleSpeed = 10f;
    [SerializeField] public float grappleShootSpeed = 20f;

    //bool isRotating = true;
    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    private Vector2 emitDirection;

    Vector2 target;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        hookRotation = GetComponent<HookRotation>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (inputDirection != Vector2.zero)
        {
            emitDirection = inputDirection;
        }


        if (Input.GetKey(KeyCode.E) && !isGrappling)
        {
            hookRotation.PauseRotation();
            StartGrapple();
        }
        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            //player.transform.position = grapplePos;
            transform.position = grapplePos;
            line.SetPosition(0, transform.position);
            if (Vector2.Distance(transform.position, target) < 0.5f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
        }
    }



    private void StartGrapple()
    {
        Vector2 direction = emitDirection;

        // Use the player's facing direction based on the angle
        float facingAngle = transform.rotation.eulerAngles.z;
        direction = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleMask);

        
        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
        else // No hit detected
        {
             // Use the player's facing direction
            Vector2 maxDistancePoint = (Vector2)transform.position + direction * maxDistance;

            StartCoroutine(DrawAndEraseLine(transform.position, maxDistancePoint, 0.5f));
        }

        

    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, target, t / time);

            // Use the player's facing direction based on the angle
            float facingAngle = transform.rotation.eulerAngles.z;
            Vector2 newDirection = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));

            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
        hookRotation.ResumeRotation();
    }

    // Emit the ray if hits nothing, simulate "All-time" emitting.
    private IEnumerator DrawAndEraseLine(Vector2 startPos, Vector2 endPos, float duration)
    {
        line.enabled = true;
        line.positionCount = 2;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);

        yield return new WaitForSeconds(duration);
        
        line.enabled = false;
        hookRotation.ResumeRotation();
    }

}
