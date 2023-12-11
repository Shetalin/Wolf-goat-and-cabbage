using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public static List<BoatScript> moveableObjects = new List<BoatScript>();
    public ParticleSystem bubbles;
    public float speed;
    public float maxSpeed = 3f;
    public GameObject pickupCircle;
    public float circleRadius = 2f;

    private Vector3 target;
    private bool selected;


    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        moveableObjects.Add(this);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // ------------------------------------------------------------------------------------------------ перемещение кликами
        if (Input.GetMouseButtonDown(0) && selected)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            Vector2 direction = target - transform.position;
            transform.up = direction.normalized;
            bubbles.Play();
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // ------------------------------------------------------------------------------------------------ стоп пузырькам из под мотора
        if (target == transform.position)
        {
            bubbles.Stop();
        }

        // ------------------------------------------------------------------------------------------------ вкл выкл области пикапа у берега
        float yPosition = transform.position.y;

        if (yPosition < -2.7f || yPosition > 2.9f)
        {
            pickupCircle.SetActive(true);
        }
        else
        {
            pickupCircle.SetActive(false);
        }

    }

    // ------------------------------------------------------------------------------------------------ выделение для перемещения и отключенная подсветка (заменить на выделение фигурной дочеркой)
    private void OnMouseDown()
    {
        selected = true;
        //gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;

        foreach(BoatScript obj in moveableObjects)
        {
            if(obj != this)
            {
                obj.selected = false;
                //gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            }    
        }

    }

    // ------------------------------------------------------------------------------------------------ стоп-кран
    public void StopTheBoat()
    {
        Vector3 backPosition = transform.position - transform.up * 0.1f;
        target = backPosition;
    }
}
