using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForPickups : MonoBehaviour
{
    public float removalRadius = 2f;
    public LayerMask objectLayer;
    public GameObject wolfOnBoard;
    public GameObject goatOnBoard;
    public GameObject cabbageOnBoard;
    public GameObject ammount2;
    public GameObject ammount3;
    public GameObject maxPassengers;
    public AudioSource pickUpSound;
    public string lastPickUpedObjectTag;
    public int passengers = 0;
    public int maxCurrentCapacity;
    public int maxCapacity = 3;

    private void Start()
    {
        maxCurrentCapacity = 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (passengers >= maxCurrentCapacity)
            {
                Debug.Log("Max capacity reached. Cannot remove more objects.");
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(mousePosition, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, objectLayer);
            if (hit.collider != null && Vector2.Distance(transform.position, hit.transform.position) <= removalRadius)
            {
                if (passengers == 0 || hit.collider.gameObject.CompareTag(lastPickUpedObjectTag))
                {
                    Destroy(hit.collider.gameObject);
                    passengers++;
                    pickUpSound.Play();
                    lastPickUpedObjectTag = hit.collider.gameObject.tag;

                    switch (lastPickUpedObjectTag)
                    {
                        case "Wolf":
                        case "Wolf transported":
                            {
                                wolfOnBoard.SetActive(true);
                                break;
                            }
                        case "Goat":
                        case "Goat transported":
                            {
                                goatOnBoard.SetActive(true);
                                break;
                            }
                        case "Cabbage":
                        case "Cabbage transported":
                            {
                                cabbageOnBoard.SetActive(true);
                                break;
                            }

                    }

                    if (passengers == 2)
                    {
                        ammount2.SetActive(true);
                    }
                    if (passengers == 3)
                    {
                        ammount2.SetActive(false);
                        ammount3.SetActive(true);
                    }
                    if (passengers == maxCurrentCapacity)
                    {
                        maxPassengers.SetActive(true);
                    }
                }
            }
        }
    }

    // Отрисовка радиуса удаления в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, removalRadius);
    }

}
