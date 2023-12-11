using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameLogic : MonoBehaviour
{
    public bool rolesReversed;
    public EatSlider eatSlider1;
    public EatSlider eatSlider2;
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public int playerMoney;
    public TextMeshProUGUI moneyText;
    public AudioSource moneyChangeSound;
    public int levelCount;
    public GameObject shop;
    public AudioSource shopMusic;
    public AudioSource mouseOnButtonSound;
    public BoatScript boatScript;
    public ScriptForPickups scriptForPickups;
    //переменные апгрейдов скорости
    [Header("Speed")]
    public TextMeshProUGUI upgradeSpeedLvlTxt;
    public int speedLvl;
    public TextMeshProUGUI speedLvlTxt;
    public int speedUpgradeCost;
    public TextMeshProUGUI speedUpgradeCostTxt;
    //переменные апгрейдов вместимости
    [Header("Capacity")]
    public TextMeshProUGUI upgradeCapacityLvlTxt;
    public int capacityLvl;
    public TextMeshProUGUI capacityLvlTxt;
    public int capacityUpgradeCost;
    public TextMeshProUGUI capacityUpgradeCostTxt;
    //переменные апгрейдов кол-ва лодок
    [Header("Boat amount")]
    public TextMeshProUGUI upgradeAmmountLvlTxt;
    public int boatAmount;
    public TextMeshProUGUI boatAmountTxt;
    public int newBoatCost;
    public TextMeshProUGUI newBoatCostTxt;
    


    // Start is called before the first frame update
    void Start()
    {
        levelCount = 1;
        speedLvl = 1;
        capacityLvl = 1;
        boatAmount = 1;
        speedUpgradeCost = 1;
        capacityUpgradeCost = 1;
        newBoatCost = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speedLvl < 3)
        {
            speedLvlTxt.text = $"{speedLvl}/3";
            speedUpgradeCostTxt.text = speedUpgradeCost.ToString();
        }
        else
        {
            speedLvlTxt.text = "maxed";
            speedUpgradeCostTxt.text = "-";
            upgradeSpeedLvlTxt.text = "<s>increase speed</s>";
        }
            
        if(capacityLvl < 3)
        {
            capacityLvlTxt.text = $"{capacityLvl}/3";
            capacityUpgradeCostTxt.text = capacityUpgradeCost.ToString();
        }
        else
        {
            capacityLvlTxt.text = "maxed";
            capacityUpgradeCostTxt.text = "-";
            upgradeCapacityLvlTxt.text = "<s>increase capacity</s>";
        }

        boatAmountTxt.text = $"{boatAmount}/3";
        newBoatCostTxt.text = newBoatCost.ToString();
    }

    [ContextMenu("Reverse Roles")]
    public void reverseRoles()
    {
        if (!rolesReversed)
        {
            rolesReversed = true;
            eatSlider1.RolesReversed();
            eatSlider2.RolesReversed();

        }
        else
        {
            rolesReversed = false;
            eatSlider1.RolesNormal();
            eatSlider2.RolesNormal();
        }
    }

    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = $"Score: {playerScore}";
    }

    public void changeMoneyAmmount(int moneyToChange)
    {
        playerMoney = playerMoney + moneyToChange;
        moneyText.text = playerMoney.ToString();
        moneyChangeSound.Play();
    }

    public void openShop()
    {
        shop.SetActive(true);
        Time.timeScale = 0;
        shopMusic.Play();
    }

    public void closeShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1;
        shopMusic.Pause();
    }

    public void mouseOnButton()
    {
        mouseOnButtonSound.Play();
    }

    public void upgradeSpeed()
    {
        if (boatScript.speed < boatScript.maxSpeed)
        {
            boatScript.speed = boatScript.speed + 1f;
            speedLvl++;
            changeMoneyAmmount(-speedUpgradeCost);
            speedUpgradeCost++;
        }
    }

    public void upgradeCapacity()
    {
        if (scriptForPickups.maxCurrentCapacity < scriptForPickups.maxCapacity)
        {
            scriptForPickups.maxCurrentCapacity = scriptForPickups.maxCurrentCapacity + 1;
            capacityLvl++;
            changeMoneyAmmount(-capacityUpgradeCost);
            capacityUpgradeCost++;
        }
    }
}
