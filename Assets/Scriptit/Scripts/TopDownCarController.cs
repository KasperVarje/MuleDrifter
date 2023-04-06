using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    //muuttujat
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

   // componentit
    Rigidbody2D carRigidbody2D;

    
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

   
    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
       
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

       
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

       
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //auto ei voi k‰‰nty‰ paikallaan
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 2);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //K‰‰ntyvyys ·uto objektille
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //eteen ja taakse liikkjmis voima
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }


    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }
}
