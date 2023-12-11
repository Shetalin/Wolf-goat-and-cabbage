using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoastAnimalsSpawnNDestroy : MonoBehaviour
{
    public GameObject wolfSpawner;
    public GameObject goatSpawner;
    public GameObject cabbageSpawner;
    public GameObject wolf;
    public GameObject goat;
    public GameObject cabbage;
    public GameObject[] match3;
    public GameLogic logic;
    public int destroyCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //спавн стартовых персонажей
        wolfSpawn(1);
        goatSpawn(1);
        cabbageSpawn(1);

        //создание массива для контроля Match3 ситуации и удаления перевезенных персонажей
        match3 = new GameObject[3];
    }

    // Update is called once per frame
    void Update()
    {
        //поиск объектов в массив Match3
        match3[0] = GameObject.FindGameObjectWithTag("Wolf transported");
        if (match3[0] == null)
        {
            match3[0] = GameObject.FindGameObjectWithTag("Goat transported");
        }
        if (match3[0] == null)
        {
            match3[0] = GameObject.FindGameObjectWithTag("Cabbage transported");
        }
        if (match3[0] != null)
        {
            switch (match3[0].tag)
            {
                case "Wolf transported":
                    match3[1] = GameObject.FindGameObjectWithTag("Goat transported");
                    if (match3[1] == null)
                    {
                        match3[1] = GameObject.FindGameObjectWithTag("Cabbage transported");
                    }
                    break;

                case "Goat transported":
                    match3[1] = GameObject.FindGameObjectWithTag("Wolf transported");
                    if (match3[1] == null)
                    {
                        match3[1] = GameObject.FindGameObjectWithTag("Cabbage transported");
                    }
                    break;

                case "Cabbage transported":
                    match3[1] = GameObject.FindGameObjectWithTag("Wolf transported");
                    if (match3[1] == null)
                    {
                        match3[1] = GameObject.FindGameObjectWithTag("Goat transported");
                    }
                    break;
            }

            if (match3[1] != null)
                switch (match3[1].tag)
                {
                    case "Wolf transported":
                        if (match3[0].tag == "Goat transported")
                        {
                            match3[2] = GameObject.FindGameObjectWithTag("Cabbage transported");
                        }
                        else
                        {
                            match3[2] = GameObject.FindGameObjectWithTag("Goat transported");
                        }
                        break;

                    case "Goat transported":
                        if (match3[0].tag == "Wolf transported")
                        {
                            match3[2] = GameObject.FindGameObjectWithTag("Cabbage transported");
                        }
                        else
                        {
                            match3[2] = GameObject.FindGameObjectWithTag("Wolf transported");
                        }
                        break;

                    case "Cabbage transported":
                        if (match3[0].tag == "Wolf transported")
                        {
                            match3[2] = GameObject.FindGameObjectWithTag("Goat transported");
                        }
                        else
                        {
                            match3[2] = GameObject.FindGameObjectWithTag("Wolf transported");
                        }
                        break;
                }
        }

        if (match3[0] != null && match3[1] != null && match3[2] != null)
        {
            Destroy(match3[0]);
            Destroy(match3[1]);
            Destroy(match3[2]);
            logic.changeMoneyAmmount(1);
            destroyCounter++;
        }

        if (destroyCounter == logic.levelCount)
        {
            destroyCounter = 0;
            logic.levelCount++;
            wolfSpawn(logic.levelCount);
            goatSpawn(logic.levelCount);
            cabbageSpawn(logic.levelCount);
        }

    }

    void wolfSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(wolfSpawner.transform.position.x - 0.2f, wolfSpawner.transform.position.x + 0.2f);
            float randomY = Random.Range(wolfSpawner.transform.position.y - 0.45f, wolfSpawner.transform.position.y + 0.45f);

            Vector3 spawnPosition = new Vector3(randomX, randomY, wolfSpawner.transform.position.z);

            Instantiate(wolf, spawnPosition, Quaternion.identity);
        }
    }

    void goatSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(goatSpawner.transform.position.x - 0.2f, goatSpawner.transform.position.x + 0.2f);
            float randomY = Random.Range(goatSpawner.transform.position.y - 0.45f, goatSpawner.transform.position.y + 0.45f);

            Vector3 spawnPosition = new Vector3(randomX, randomY, goatSpawner.transform.position.z);

            Instantiate(goat, spawnPosition, Quaternion.identity);
        }
    }

    void cabbageSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(cabbageSpawner.transform.position.x - 0.6435f, cabbageSpawner.transform.position.x + 0.6435f);
            float randomY = Random.Range(cabbageSpawner.transform.position.y - 0.2205f, cabbageSpawner.transform.position.y + 0.2205f);

            Vector3 spawnPosition = new Vector3(randomX, randomY, cabbageSpawner.transform.position.z);

            Instantiate(cabbage, spawnPosition, Quaternion.identity);
        }
    }
}
