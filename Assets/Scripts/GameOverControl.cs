using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameOverControl : MonoBehaviour
{
    public GameObject GameOverHolder;
    public TextMeshProUGUI TimeText;
    private float TotalTime;

    public bool GameOverCheck = false;

    // Start is called before the first frame update
    void Start()
    {
       GameOverHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOverCheck) TotalTime += Time.deltaTime;
    }

    public void GameOver()
    {
        TimeText.text = "Time: " + TotalTime.ToString("F0");
        GameOverHolder.SetActive(true);
        GameOverCheck = true;


    }

    public void MenuCall()
    {
        SceneManager.LoadScene(0);

    }


}
