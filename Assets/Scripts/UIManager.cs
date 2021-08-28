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
    public GameObject creditsScreen;

    void Start()
    {
        if (creditsScreen == null) creditsScreen = GameObject.Find("CreditsScreen");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        lvlUpWindow.SetActive(false);
        gameOverScreen.SetActive(false);
        creditsScreen.SetActive(false);
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

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
    }
}
