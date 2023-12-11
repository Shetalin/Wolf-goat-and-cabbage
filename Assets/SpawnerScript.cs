using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SpawnerScript : MonoBehaviour
{
    public const float MAX_Y_DISTANCE = 1;
    public GameObject wolf;
    public GameObject goat;
    public GameObject cabbage;
    public ScriptForPickups pickUpScript;
    public GameLogic logic;
    public AudioSource offTheBoatSound;
    public float minY = 0.7f;
    public float maxY = 1.6f;
    public float minXOffset = -0.5f;
    public float maxXOffset = 0.5f;
    private bool justSpawn = false;

    
    public void Update()
    {

        // ------------------------------------------------------------------------------------------------ Условие спавна высадки на берегу 2
        float yPosition = transform.position.y;

        if (yPosition > 2.9f && !justSpawn)
        {
            spawnPassengers();
            justSpawn = true;
        }
        else if (yPosition <= 2.9f)
        {
            justSpawn = false;
        }
    }

    // ------------------------------------------------------------------------------------------------ Скрипт спавна высадки на берегу 2

    public void spawnPassengers()
    {
        int passengers = pickUpScript.passengers;
        string tagToAssign = "";
        GameObject objectToSpawn = null;

        StartCoroutine(SpawnPassengersWithDelay(passengers, objectToSpawn, tagToAssign));
    }

    private IEnumerator SpawnPassengersWithDelay(int passengerCount, GameObject objToSpawn, string tagToAssign)
    {
        for (int i = 0; i < passengerCount; i++)
        {
            float randomY = transform.position.y + Random.Range(minY, maxY);
            float randomX = transform.position.x + Random.Range(minXOffset, maxXOffset);

            if (pickUpScript.lastPickUpedObjectTag == "Wolf" || pickUpScript.lastPickUpedObjectTag == "Goat" || pickUpScript.lastPickUpedObjectTag == "Cabbage")
            {
                logic.addScore(1);
            }

            switch (pickUpScript.lastPickUpedObjectTag)
            {
                case "Wolf":
                case "Wolf transported":
                    {
                        objToSpawn = wolf;
                        tagToAssign = "Wolf transported";
                        break;
                    }
                case "Goat":
                case "Goat transported":
                    {
                        objToSpawn = goat;
                        tagToAssign = "Goat transported";
                        break;
                    }
                case "Cabbage":
                case "Cabbage transported":
                    {
                        objToSpawn = cabbage;
                        tagToAssign = "Cabbage transported";
                        break;
                    }
            }

            GameObject pickup = Instantiate(objToSpawn, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            pickup.tag = tagToAssign;
            offTheBoatSound.Play();

            yield return new WaitForSeconds(0.2f);
        }

        pickUpScript.passengers = 0;
        pickUpScript.wolfOnBoard.SetActive(false);
        pickUpScript.goatOnBoard.SetActive(false);
        pickUpScript.cabbageOnBoard.SetActive(false);
        pickUpScript.ammount2.SetActive(false);
        pickUpScript.ammount3.SetActive(false);
        pickUpScript.maxPassengers.SetActive(false);
    }

    // сохраненный метод без курутина
    //--------------------------------
    //public void spawnPassengers()
    //{
    //    int passengers = pickUpScript.passengers;
    //    string tagToAssign = "";
    //    GameObject objectToSpawn = null;

    //    for (int i = 0; i < passengers; i++)
    //    {
    //        float randomY = transform.position.y + Random.Range(minY, maxY);
    //        float randomX = transform.position.x + Random.Range(minXOffset, maxXOffset);

    //        if (pickUpScript.lastPickUpedObjectTag == "Wolf" || pickUpScript.lastPickUpedObjectTag == "Goat" || pickUpScript.lastPickUpedObjectTag == "Cabbage")
    //        {
    //            logic.addScore(1);
    //        }

    //        switch (pickUpScript.lastPickUpedObjectTag)
    //        {
    //            case "Wolf":
    //            case "Wolf transported":
    //                {
    //                    objectToSpawn = wolf;
    //                    tagToAssign = "Wolf transported";
    //                    break;
    //                }
    //            case "Goat":
    //            case "Goat transported":
    //                {
    //                    objectToSpawn = goat;
    //                    tagToAssign = "Goat transported";
    //                    break;
    //                }
    //            case "Cabbage":
    //            case "Cabbage transported":
    //                {
    //                    objectToSpawn = cabbage;
    //                    tagToAssign = "Cabbage transported";
    //                    break;
    //                }
    //        }

    //        GameObject pickup = Instantiate(objectToSpawn, new Vector3(randomX, randomY, 0f), Quaternion.identity);
    //        pickup.tag = tagToAssign;

    //    }
    //    pickUpScript.passengers = 0;
    //    pickUpScript.wolfOnBoard.SetActive(false);
    //    pickUpScript.goatOnBoard.SetActive(false);
    //    pickUpScript.cabbageOnBoard.SetActive(false);
    //    pickUpScript.ammount2.SetActive(false);
    //    pickUpScript.ammount3.SetActive(false);
    //    pickUpScript.maxPassengers.SetActive(false);

    //}
}
