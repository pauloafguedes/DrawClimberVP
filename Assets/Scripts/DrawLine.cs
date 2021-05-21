using System.Collections.Generic;
using UnityEngine;


public class DrawLine : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject leftArm;
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameObject currentLine;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] List<Vector2> fingerPos;    
    [SerializeField] Camera cam;

    [SerializeField] BoxCollider boxColliderPart;

    [SerializeField] bool started = false;
    [SerializeField] GameObject canvas;    
    
    void Update()
    {        
        if(Input.GetMouseButtonDown(0))
        {
            CallDestruction();
            
            CreateLine();            
            
        }
        
        if(Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = cam.ScreenToWorldPoint(Input.mousePosition);           
            if (Vector2.Distance(tempFingerPos, fingerPos[fingerPos.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }        
        
        if(Input.GetMouseButtonUp(0))
        {
            if(lineRenderer.positionCount >3)
            { 
                CreateBoxArms();
            }
        }
    }
    
    void CreateLine()
    {
        if(started)
        {            
        }
        else
        {
            SetBoolStarted();
            Player.GetComponent<MovementPlayer>().EnablePhysics();
            canvas.SetActive(false);
        }
        
        fingerPos.Clear();
        
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPos.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);
    }
    
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPos.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }

    void CreateBoxArms()
    {
        int currentPoint=lineRenderer.positionCount - 2;
        int lastPoint= (lineRenderer.positionCount - 3);        
        Instantiate(boxColliderPart, leftArm.transform.position, Quaternion.identity).transform.parent = leftArm.transform;
        Instantiate(boxColliderPart, rightArm.transform.position, Quaternion.identity).transform.parent = rightArm.transform;

        for (int i = lineRenderer.positionCount - 1; i >0 ; i--)
        {
            if(Vector3.Distance(lineRenderer.GetPosition(currentPoint), lineRenderer.GetPosition(lastPoint)) >0.5f)
            {
                Vector3 temp = lineRenderer.GetPosition(lineRenderer.positionCount - 1) - lineRenderer.GetPosition(lastPoint);
                Vector3 leftSide = new Vector3(leftArm.transform.position.x - temp.x, leftArm.transform.position.y + temp.y, 0.8f);
                Vector3 rightSide = new Vector3(leftArm.transform.position.x + temp.x, leftArm.transform.position.y - temp.y, -0.8f);
                
                if(lastPoint >= 0)
                {                    
                    Instantiate(boxColliderPart, leftSide, Quaternion.Euler(lineRenderer.GetPosition(currentPoint).normalized)).transform.parent = leftArm.transform;
                    Instantiate(boxColliderPart, rightSide, Quaternion.Euler(lineRenderer.GetPosition(currentPoint).normalized)).transform.parent = rightArm.transform;
                }
                           
                currentPoint = lastPoint;
                i = currentPoint;
                lastPoint--;
            }
            else
            {
                lastPoint--;                
            }            
        }        
    }

    public void CallDestruction()
    {
        if(leftArm.transform.childCount>0 || rightArm.transform.childCount>0)
        {
            GameObject g = FindObjectOfType<LineRenderer>().gameObject;
            Destroy(g);
            foreach(Transform child in leftArm.transform)
            {               
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in rightArm.transform)
            {
                GameObject.Destroy(child.gameObject);
            }            
        }                
    }
    
    public void SetBoolStarted()
    {
        started = !started;
    }

    public void ChangeStateCanvas()
    {
        canvas.SetActive(true);
    }
}



