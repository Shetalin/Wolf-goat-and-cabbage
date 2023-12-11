using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AnimalsScript : MonoBehaviour
{
    private const string WOLF_NAME = "Wolf(Clone)";
    private const string GOAT_NAME = "Goat(Clone)";
    private const string CABBAGE_NAME = "Cabbage(Clone)";
    private const float EATING_DISTANCE = 0.5f;

    public GameLogic gameLogic;
    public HealthAndDamageScript hpScript;
    public float speed = 2f;
    public int biteStrength = 1;

 //   public GameObject target1;
   // public GameObject target2;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        string objectName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        String dangerName = "";
        GameObject targetObject = null;
        switch (gameObject.name)
        {
            case WOLF_NAME:
                targetObject = gameLogic.rolesReversed ? GameObject.Find(CABBAGE_NAME) : GameObject.Find(GOAT_NAME);
                dangerName = gameLogic.rolesReversed ? GOAT_NAME : "";
                break;

            case GOAT_NAME:
                targetObject = gameLogic.rolesReversed ? GameObject.Find(WOLF_NAME) : GameObject.Find(CABBAGE_NAME);
                dangerName = gameLogic.rolesReversed ? "" : WOLF_NAME;
                break;
        }
        if (targetObject == null || targetObject.transform == null || Math.Abs(targetObject.transform.position.y - transform.position.y) > SpawnerScript.MAX_Y_DISTANCE)
        {
            return;
        }

        Vector3 direction = targetObject.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > EATING_DISTANCE && !existsDanger(dangerName))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            //Поворот спрайта в сторону движения
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            transform.position += velocity;

        }
        else
        {
            HealthAndDamageScript healthScript = targetObject.transform.GetComponent<HealthAndDamageScript>();
            if (healthScript != null)
            {
                healthScript.TakeDamage((int)(biteStrength * Time.deltaTime * 1000));

            }
        }
    }
    private Boolean existsDanger(String dangerName)
    {
        if (dangerName == null || dangerName.Trim().Equals(""))
        {
            return false;
        }
        GameObject danger = GameObject.Find(dangerName);
        if (danger == null)
        {
            return false;
        }

        Vector3 direction = danger.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= EATING_DISTANCE)
        {
            return true;
        }
        return false;
    }

}
