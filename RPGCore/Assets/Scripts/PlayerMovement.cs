using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;

    Vector3 currentDestination, clickPoint;

    [SerializeField]float targetDeadzone = 0.2f;
    [SerializeField] float walkMoveStopRadius = 0.1f;
    public float attackMoveStopRadius = 0.5f;
    bool GamepadEnabled;


    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GamepadEnabled = !GamepadEnabled;
            currentDestination = transform.position;
        }

        if (GamepadEnabled)
        {
            ProcessDirectMovement();
        }
        else
        {
             ProcessIndirectMovement();
        }
    }

    private void ProcessDirectMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;
        m_Character.Move(m_Move, false, false);

    }

    private void ProcessIndirectMovement()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);

                    break;

                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
                    break;

                case Layer.Interactable:

                    break;

                case Layer.RaycastEndStop:
                    break;

                default:
                    Debug.LogError("Invalid layerHit");
                    break;

            }



        }
        WalkToDestination();
    }

    private void WalkToDestination()
    {
        Vector3 playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude > 0)
        {
            m_Character.Move(currentDestination - transform.position, false, false);

        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);

        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }


    private void OnDrawGizmos()
    {
        //Draw movement Gizmos.
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawLine(currentDestination, clickPoint);
        Gizmos.DrawSphere(clickPoint, 0.2f);


    }
}

