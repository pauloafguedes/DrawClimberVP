using UnityEngine;

public class RotateAround : MonoBehaviour
{    
        [SerializeField]float speedRot = -3; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -6f);        
    }
}
