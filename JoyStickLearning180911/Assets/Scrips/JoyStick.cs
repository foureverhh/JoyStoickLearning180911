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

    //let joystick have original place
    private Vector2 originPoint;
    //calculate the offset vector2 between mouse click and joystick original place
    private Vector2 offsetToOri;
    //private Vector2 originPointCamera;

    // Use this for initialization
    void Start () {
        originPoint = circle.position;
        //originPointCamera = Camera.main.ScreenToWorldPoint(new Vector3(originPoint.x, originPoint.y, Camera.main.transform.position.z));
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));//Camera.main.transform.position.z));
            circle.transform.position = pointA;
            //Calculate the offest of pointA and orginPointCamera
            //offsetToOri = pointA - originPointCamera;
            //Make circle stay at originPoint
            //circle.transform.position =  offsetToOri;
        }

        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));// Camera.main.transform.position.z));
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
            Vector2 directionWithRadius = Vector2.ClampMagnitude(offset, 1.0f);
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
