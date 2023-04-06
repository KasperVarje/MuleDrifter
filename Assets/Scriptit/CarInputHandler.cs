using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    //Componentit
    TopDownCarController topDownCarController;

   
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
    }

    void Start()
    {

    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        //hakee inputit unityn input systeemistä
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        
        topDownCarController.SetInputVector(inputVector);
    }
}
