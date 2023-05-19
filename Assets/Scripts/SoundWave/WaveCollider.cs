using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour
{
    public float speed = 1f;
    [HideInInspector]
    public WaveCollider next;
    public GameObject lineRenderer;
    public float splitThreshold = 1f;
    public Vector3 velocity;
    private Rigidbody rb;
    [SerializeField]
    private float timeLeft = 20f;
    private float totalTime;
    public float velocityReductionOnHit = 1f;
    

    private void Start()
    {
        totalTime = timeLeft;
        lineRenderer.GetComponent<LineRenderer>().enabled = false;
        //velocity = transform.forward;
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.right * speed, ForceMode.Impulse);
        
        //rb.velocity = transform.right * speed * Time.deltaTime;
    }
    private void Update()
    {
        
        rb.position += velocity * Time.deltaTime * speed;
        //transform.localPosition += velocity * Time.deltaTime * speed;
        if (next != null&&Vector3.Distance(this.transform.position,next.transform.position)<splitThreshold)
        {
            lineRenderer.GetComponent<LineRenderer>().enabled = true;
            LineRenderer rend = lineRenderer.GetComponent<LineRenderer>();
            rend.positionCount = 2;
            rend.SetPosition(0, Vector3.zero);
            rend.SetPosition(1, next.transform.position - transform.position);
            //Set Color opacity
            rend.startColor = new Color(rend.startColor.r, rend.startColor.g, rend.startColor.b, timeLeft / totalTime);
            

            rend.endColor = rend.startColor;
        }
        else
        {
            lineRenderer.GetComponent<LineRenderer>().enabled = false;
        }
        
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public class VectorUtils : MonoBehaviour
    {
        public static float AngleBetweenVectors(Vector3 vector1, Vector3 vector2)
        {
            float dotProduct = Vector3.Dot(vector1.normalized, vector2.normalized);
            float angleInRadians = Mathf.Acos(dotProduct);
            float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
            return angleInDegrees;
        }
        public static Vector2 RotateVector(Vector2 vector, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(radians);
            float sinAngle = Mathf.Sin(radians);
            float newX = vector.x * cosAngle - vector.y * sinAngle;
            float newY = vector.x * sinAngle + vector.y * cosAngle;
            return new Vector2(newX, newY);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>(), true);
        print("collided!");

        ContactPoint[] contacts = collision.contacts;
        if (contacts.Length > 0)
        {
            Vector3 averageNormal = Vector3.zero;
            foreach (ContactPoint contact in contacts)
            {
                averageNormal += contact.normal;
            }
            averageNormal /= contacts.Length;

            // Calculate the tangent vector to the collision surface
            Vector3 tangent = Vector3.Cross(Vector3.up, averageNormal).normalized;

            // Use the tangent vector for further calculations or processing
            Debug.Log("Tangent: " + tangent);
            tangent = Quaternion.Euler(0, -90, 0) * tangent;
            velocity += tangent * 2;
            velocity *= velocityReductionOnHit;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>(), false);
    }
}
