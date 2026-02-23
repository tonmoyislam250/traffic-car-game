using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;

    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;

    [SerializeField] private float motorForce = 100f;
    [SerializeField] private float steerAngle = 30f;
    [SerializeField] private float brakeForce = 1000f;

    [SerializeField] private Transform carCentreOfMassTransform;
    [SerializeField] UIManager uiManager;

    private float verticalInput;
    private float horizontalInput;
    private Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass=carCentreOfMassTransform.localPosition;
    }

    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        UpdateWheels();
        Steering();
        ApplyBrakes();
        PowerSteering();
        Debug.Log(CarSpeed());
    }
    void ApplyBrakes()
{
    if (Keyboard.current.spaceKey.isPressed)
    {
        frontRightWheelCollider.brakeTorque = brakeForce;
        backRightWheelCollider.brakeTorque = brakeForce;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        backLeftWheelCollider.brakeTorque = brakeForce;
        rigidbody.linearDamping = 1f;
    }
    else
    {
        frontRightWheelCollider.brakeTorque = 0f;
        backRightWheelCollider.brakeTorque = 0f;
        frontLeftWheelCollider.brakeTorque = 0f;
        backLeftWheelCollider.brakeTorque = 0f;
        rigidbody.linearDamping = 0f;
    }
}

    void GetInput()
    {
        verticalInput =
            Keyboard.current.wKey.isPressed ? 1f :
            Keyboard.current.sKey.isPressed ? -1f : 0f;

        horizontalInput =
            Keyboard.current.dKey.isPressed ? 1f :
            Keyboard.current.aKey.isPressed ? -1f : 0f;
    }

    void MotorForce()
    {
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
        frontLeftWheelCollider.motorTorque = motorForce * verticalInput;
    }
   void PowerSteering()
    {
        if (horizontalInput == 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(0f, 0f, 0f),
                Time.deltaTime);
            }
    }
    
    void Steering()
    {
        frontRightWheelCollider.steerAngle=steerAngle*horizontalInput;
        frontLeftWheelCollider.steerAngle=steerAngle*horizontalInput;
    }
    void UpdateWheels()
    {
        RotateWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(backRightWheelCollider, backRightWheelTransform);
        RotateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        RotateWheel(backLeftWheelCollider, backLeftWheelTransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
    public float CarSpeed()
    {
        float speed = rigidbody.linearVelocity.magnitude * 2.23693629f;
        return speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Traffic")
        {
            uiManager.GameOver();
        }
    }
}
