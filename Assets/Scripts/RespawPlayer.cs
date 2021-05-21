using UnityEngine;

public class RespawPlayer : MonoBehaviour
{
    [SerializeField] GameObject Player;
    DrawLine drawLine;
    Vector3 startPosition;
    
    void Start()
    {
        startPosition = Player.transform.position;
        drawLine = FindObjectOfType<DrawLine>();
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetComponent<MovementPlayer>().DisablePhysics();
            drawLine.CallDestruction();
            drawLine.SetBoolStarted();
            Player.transform.position = startPosition;
            drawLine.ChangeStateCanvas();
        }
    }
}
