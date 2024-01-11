using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI textGoal;  //goal text

    public int goal; //goal value 

    [SerializeField]
    private GameObject btnRetry;

    [SerializeField]
    private Color green;

    [SerializeField]
    private Color red;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textGoal.SetText(goal.ToString()); // Update total goal number
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseGoal() //goal value -1 
    {
        goal -= 1;
        textGoal.SetText(goal.ToString());

        if (goal <= 0)
        {
            SetGameOver(true);
        }
    }

    public void SetGameOver(bool success) //game over check (background color)
    {
        if (isGameOver == false)
        {
            isGameOver = true;

            Camera.main.backgroundColor = success ? green : red;
            // btnRetry.SetActive(true);
            Invoke("ShowRetryButton", 1f); // after 1 sec show btn
        }
    }

    void ShowRetryButton()
    {
        btnRetry.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
