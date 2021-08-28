using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public playerController playerController;
    public float healthIncrease;
    public float healthRegenDecrease;
    public float damageIncrease;
    public float firerateIncrease;
    public GameObject lvlUpWindow;
    public GameObject gameOverScreen;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        lvlUpWindow.SetActive(false);
        gameOverScreen.SetActive(false);
    }
    public void MaxHealthButton()
    {
        playerController.maxHealth += healthIncrease;
        lvlUpWindow.SetActive(false);
    }

    public void HealthRegenButton()
    {
        playerController.healthRegen -= healthRegenDecrease;
        lvlUpWindow.SetActive(false);
    }

    public void DamageButton()
    {
        playerController.weaponDamage += damageIncrease;
        lvlUpWindow.SetActive(false);
    }

    public void FireRateButton()
    {
        playerController.fireRate -= firerateIncrease;
        lvlUpWindow.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
