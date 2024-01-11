using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinLauncher : MonoBehaviour
{

    [SerializeField]
    private GameObject pinObject;
    private Pin currPin;

    // Start is called before the first frame update
    void Start()
    {
        PreparePin();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currPin != null
        && GameManager.instance.isGameOver == false) //check mouse click && null check && game over 
        {
            currPin.Launch();  //launch !
            currPin = null;
            Invoke("PreparePin", 0.1f); //wait time 
        }
    }

    void PreparePin() //multi pin
    {
        if (GameManager.instance.isGameOver == false )
        {
            GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
            currPin = pin.GetComponent<Pin>();
        }
        
    }

}
