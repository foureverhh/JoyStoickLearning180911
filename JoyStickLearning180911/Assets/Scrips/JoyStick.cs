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
    private Vector2 OffsetOriginDotToUFO;
    //calculate the offset vector2 between mouse click and joystick original place
    //private Vector2 offsetToOri;

    // Use this for initialization
    void Start () {
        //Make originDot to be originPosition
        originPoint = originDot.position;
        // Know offset between UFO and originDot 
        OffsetOriginDotToUFO = player.position - originDot.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("OffsetOriginDotToUFO is: " + OffsetOriginDotToUFO);
            //pointA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            Debug.Log("Mouse position is: " + Input.mousePosition);
            Debug.Log("PointA position is " + pointA);
            Debug.Log("cirlcle position is " + circle.position);
            circle.transform.position = pointA - OffsetOriginDotToUFO;
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
	}
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 directionWithRadius = Vector2.ClampMagnitude(offset, 0.5f);
            //camera and GUI view are oppsite ??
            MoveCharacter(directionWithRadius);
            circle.transform.position = new Vector2(pointA.x + directionWithRadius.x, pointA.y + directionWithRadius.y);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
}
