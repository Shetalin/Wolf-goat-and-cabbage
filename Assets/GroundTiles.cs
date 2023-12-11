using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTiles : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            BoatScript boatScript = collision.GetComponent<BoatScript>();
            if (boatScript != null)
            {
                boatScript.StopTheBoat();
            }
        }
    }
}
