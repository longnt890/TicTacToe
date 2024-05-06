using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;

    public GameObject restartButton;

    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceButtons();
        playerSide = "X";
        moveCount = 0;
        restartButton.SetActive(false);
    }

    void SetGameControllerReferenceButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;

        for (int i = 0; i < 9; i += 3)
        {
            if (buttonList[i].text == playerSide && buttonList[i + 1].text == playerSide && buttonList[i + 2].text == playerSide)
            {
                GameOver();
                return; // Exit the function since the game is over
            }
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (buttonList[i].text == playerSide && buttonList[i + 3].text == playerSide && buttonList[i + 6].text == playerSide)
            {
                GameOver();
                return; // Exit the function since the game is over
            }
        }

        // Check diagonals
        if ((buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) ||
            (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide))
        {
            GameOver();
            return; // Exit the function since the game is over
        }

        if(moveCount >= 9)
        {
            SetGameOverText("DRAW!!!");
        }

        ChangeSide();
    }

    void GameOver()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }


        SetGameOverText(playerSide + " WIN!!");

        restartButton.SetActive(true);
    }

    void ChangeSide()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }
    public void RestartGame()
    {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = true;
            buttonList[i].text = "";
        }

        restartButton.SetActive(false);
    }
}
