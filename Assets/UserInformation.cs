using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UserInformation : MonoBehaviour
{

    int CreditBetAmount = 0;
    int AutoBetAmount = 0;
    int CreditWon = 0;
    bool AutoBet = false;

    public Text YourCredits;
    public Text CreditBet;
    public Text YourWinCredits;

    public ShopDatabaseSO shopDatabaseSO;
  
    public void QuitGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Start is called before the first frame update
    void Start()
    {

        YourCredits.text = "$" + shopDatabaseSO.money.ToString();
    }

    public void UpdateYourCreditsText()
    {
        YourCredits.text = "$" + shopDatabaseSO.money.ToString();
    }

    public GameObject[] CreditButton;
    public void btn_BetCredit(int creditAmount)
    {
        int buttonID = 0;
        if (creditAmount == 5) { buttonID = 0; }
        else if (creditAmount == 10) { buttonID = 1; }
        else { buttonID = 2; }
        if (FindObjectOfType<SpinnerController>().getActiveSpinStatus() == false)
        {
            CreditBetAmount = creditAmount;
            foreach (GameObject child in CreditButton)
            {
                child.GetComponent<Image>().color = new Color(185f / 255f, 80f / 255f, 80f / 255f);
            }
            CreditButton[buttonID].GetComponent<Image>().color = new Color(241f / 255f, 92f / 255f, 92f / 255f);
        }
    }

    public int getCreditBetAmoun()
    {
        return CreditBetAmount;
    }
    public void AddCredit(int amount)
    {
        CreditWon += amount;
        //InitialCredit += amount;
        //YourCredits.text = "$" + InitialCredit.ToString();
        YourWinCredits.text = "$" + CreditWon.ToString();
    }
    public void DeductCredit()
    {
        if (AutoBet == false)
        {
            DeductInitialAmount();
        }
        else
        {
            DeductAutoBetAmount();
        }
    }

    public GameObject AutoBetButton;
    public void btn_ActiveorDeactiveAutoBet()
    {
        FindObjectOfType<AutoBetPanelControl>().ActivePanel();
    }

    public void btn_StartAutoBet()
    {
        if (isAutoBetAmountEnough() == true)
        {
            AutoBet = true;

        }
        AutoBetButtonFX();
    }
    void AutoBetButtonFX()
    {
        if (AutoBet == true)
        {
            AutoBetButton.GetComponent<Image>().color = new Color(241f / 255f, 92f / 255f, 92f / 255f);
            SetAutoBetButtonInteractable(false);
        }
        else
        {
            AutoBetButton.GetComponent<Image>().color = new Color(185f / 255f, 80f / 255f, 80f / 255f);
            SetAutoBetButtonInteractable(true);
        }
    }

    public void SetAutoBetButtonInteractable(bool isActive)
    {
        AutoBetButton.GetComponent<Button>().interactable = isActive;
    }
    public bool getAutoBetValue()
    {
        if (AutoBetAmount >= CreditBetAmount)
        {
            AutoBet = true;
        }
        else
        {
            AutoBet = false;
        }

        return AutoBet;
    }
    public bool getManualBetValue()
    {
        if (shopDatabaseSO.money >= CreditBetAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int getTotalCredit()
    {
        return shopDatabaseSO.money;
    }

    public void setAutoBetAmount(int amount)
    {
        shopDatabaseSO.money -= amount;
        YourCredits.text = "$" + shopDatabaseSO.money.ToString();
        AutoBetAmount = amount;
        CreditBet.text = "$" + amount;
    }
    public void DeductInitialAmount()
    {
        shopDatabaseSO.money -= CreditBetAmount;
        YourCredits.text = "$" + shopDatabaseSO.money.ToString();
    }
    public void DeductAutoBetAmount()
    {
        AutoBetAmount -= CreditBetAmount;
        CreditBet.text = "$" + AutoBetAmount;
    }

    public bool isAutoBetAmountEnough()
    {
        if (AutoBetAmount >= CreditBetAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }





}
