using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour {

    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    public Transform circle;
    public Transform originDot;

    //let joystick have original place
    private Vector2 originPoint;
   // private Vector2 OffsetOriginDotToUFO;
    //calculate the offset vector2 between mouse click and joystick original place
    //private Vector2 offsetToOri;

    // Use this for initialization
    void Start () {
        //Make originDot to be originPosition
        originPoint = circle.position;
        // Know offset between UFO and originDot 
       // OffsetOriginDotToUFO = player.position - originDot.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
          if (Input.GetMouseButtonDown(0) && ClickOnRightButton(Input.mousePosition))
          {
              //Debug.Log("OffsetOriginDotToUFO is: " + OffsetOriginDotToUFO);
              //pointA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
              Debug.Log("Mouse position is: " + Input.mousePosition);
              Debug.Log("PointA position is " + pointA);
              Debug.Log("cirlcle position is " + circle.position);
             //circle.transform.position = pointA - OffsetOriginDotToUFO;
              Debug.Log("cirlcle position after offset is " + circle.position);
              //originDot.position = pointA - OffsetOriginDotToUFO;
              Debug.Log("Cirlcel position is: " + circle.transform.position);

              //offsetToOri = pointA - originPoint;
              //circle.transform.position = pointA;
              //Calculate the offest of pointA and orginPointCamera

              //Make circle stay at originPoint
              //circle.transform.position =  offsetToOri;
          }

          if (Input.GetMouseButton(0))
          {
              touchStart = true;
             // pointB = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.transform.position.z));
          }
          //else
          //{
          //    touchStart = false;
          //}
          //Let joystick be back to original place
          if (Input.GetMouseButtonUp(0))
          {
              touchStart = false;
              circle.position = originPoint;
          }
        
        /*
        //To test touch on Android     
        if (Input.touchCount > 0)
        {
            if (ClickOnRightButton(Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                         Debug.Log("touchCount is more than 1");
                         pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.transform.position.z));
            }

            if(ClickOnRightButton(Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                            Debug.Log("touchCount is more than 2");
                             touchStart = true;
                        pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.transform.position.z));
            }

            //if (ClickOnRightButton(Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Ended)
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("touchCount is more than 3");
                touchStart = false;
                circle.position = originPoint;
            }
            
        }
        */
    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 directionWithRadius = Vector2.ClampMagnitude(offset, 1f);
            //camera and GUI view are oppsite ??
            MoveCharacter(directionWithRadius);
            circle.position = new Vector2(pointA.x + directionWithRadius.x, pointA.y + directionWithRadius.y);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
   
    //To check whether the touch is on the joystick
    bool ClickOnRightButton(Vector2 pos)
    {
        Vector2 fingerPos = Camera.main.ScreenToWorldPoint(pos);
        LayerMask joystick = LayerMask.GetMask("Joystick");
        RaycastHit2D hit = Physics2D.Raycast(fingerPos, Vector2.zero, Mathf.Infinity, joystick);
        Debug.Log("It runs to Raycast to check button");
        if (hit.collider != null)// && hit.collider.gameObject == circle)
        {
            Debug.Log("Get button");
            return true;
        }
        else
            return false;
    }
}
