using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] GameObject puck;
    [SerializeField] float aimMoveSpeed;
    [SerializeField] float pooshForce;

    [SerializeField] GameObject arrow;
    [Space]
    [SerializeField] Transform moveLimits;
    [SerializeField] Transform moveLimits2;

    Rigidbody puckRigidbody;

    Vector3 startPuckPosition;
    Vector3 touch, targetPos, touchPos;

    float x, y;
    float distance;

    private bool isPuckMoving;

    private void Start()
    {
        arrow.SetActive(false);
        puckRigidbody = puck.GetComponent<Rigidbody>();
        startPuckPosition = puck.transform.position;
    }

    private void Update()
    {
        if (Game.gameStart && !Game.gamePause)
        {
#if UNITY_EDITOR
            CheckIfMouseOnPuck();
#else
            CheckIfTouchPuck();
#endif
            if (Game.isMoving && !Game.puckPooshing)
            {
                MovePuck();
                isPuckMoving = true;
                arrow.SetActive(true);               
                ArrowLengthChange();
            }
            else if (!Game.isMoving && isPuckMoving && !Game.gamePause)
            {
                PooshPuck();
                Game.puckPooshing = true;
                arrow.SetActive(false);
            }
            else if (Game.respawnPuck)
            {
                Respawn();
                Game.puckPooshing = false;
            }
        }
    }
    void ArrowLengthChange()
    {
        distance = Vector3.Distance(transform.position, puck.transform.position)/3.4f;
        Vector3 arrowLength = new Vector3(distance, arrow.transform.localScale.y, arrow.transform.localScale.z);
        arrow.transform.localScale = arrowLength;
    }
    void Respawn()
    {
        puck.transform.position = startPuckPosition;
        puckRigidbody.velocity = new Vector3(0, 0, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        Game.respawnPuck = false;
    }
    void MovePuck()
    {
        isPuckMoving = true;

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(puck.transform.position,
            puck.transform.position + new Vector3(x, 0, y),
            aimMoveSpeed * Time.deltaTime);

        touchPos = new Vector3(
            Mathf.Clamp(touch.x, moveLimits.position.x, moveLimits2.position.x),
            touch.y,
            Mathf.Clamp(touch.z, moveLimits2.position.z, moveLimits.position.z));

        puck.transform.position = touchPos;

        targetPos = new Vector3(gameObject.transform.position.x,
                                puck.transform.position.y,
                                gameObject.transform.position.z);

        puck.transform.LookAt(targetPos, Vector3.up);
        gameObject.transform.forward = new Vector3(puck.transform.forward.x, transform.forward.y, puck.transform.forward.z);
    }
    void PooshPuck()
    {
        isPuckMoving = false;
        puckRigidbody.velocity += puck.transform.forward * PuckPooshingForce();
    }
    float PuckPooshingForce()
    {
        distance = Vector3.Distance(puck.transform.position, gameObject.transform.position);
        return distance * 5;
    }
    void CheckIfMouseOnPuck()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puck" && Input.GetMouseButton(0))
                {
                    Game.isMoving = true;
                }
            }
            if (!Input.GetMouseButton(0))
            {
                Game.isMoving = false;
            }
                   
    }
    void CheckIfTouchPuck()
    {
        
        RaycastHit hit;

        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puck" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Game.isMoving = true;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Game.isMoving = false;
            }
        }
    }
}
