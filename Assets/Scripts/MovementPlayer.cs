using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody rigidbodyPlayer;
    [SerializeField] float movespeed = 0.0f;
    [SerializeField] float maxSpeed = 15.0f;       

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();        
    }

    public void EnablePhysics()
    {
        rigidbodyPlayer.useGravity =  true;
        movespeed = 11.0f;        
    }

    public void DisablePhysics()
    {
        rigidbodyPlayer.useGravity = false;
        movespeed = 0;
    } 

    private void FixedUpdate()
    {
        rigidbodyPlayer.velocity = Vector3.right* movespeed;         
    }  
}

