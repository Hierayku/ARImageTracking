using System;
using UnityEngine;

// Singleton (so that only one exists)
public class HelicopterController : MonoBehaviour
{
    public static HelicopterController Instance;
    
    [SerializeField] private float verticalSpeed = 1f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;
    
    [SerializeField] private FixedJoystick joystick;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        joystick = (FixedJoystick)FindFirstObjectByType(typeof(FixedJoystick));
    }

    private void Update()
    {
        MoveTowardsCameraYPosition();
        RotateTowardsCamera();
        
        Move();
    }
    
    private void RotateTowardsCamera()
    {
        Vector3 directionToCamera = (Camera.main.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    private void MoveTowardsCameraYPosition()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = Mathf.Lerp(transform.position.y, Camera.main.transform.position.y, Time.deltaTime * verticalSpeed);
        transform.position = targetPosition;
    }
    
    private void Move()
    {
        Vector3 rightDirection = transform.right;
        Vector3 forwardDirection = Vector3.Cross(Vector3.up, rightDirection);

        Vector3 moveDirection = (-rightDirection * joystick.Horizontal + forwardDirection * joystick.Vertical).normalized;
        transform.position += moveDirection * (moveSpeed * 0.1f) * Time.deltaTime;
    }
}