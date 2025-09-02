using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ShopDatabaseSO shopDatabaseSO;

    public TMP_Text moneyText;

    private void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (moneyText != null)
            moneyText.text = "$" + shopDatabaseSO.money.ToString("N0");
    }
    
     // Bu fonksiyonu butona atayabilirsin
    public void LoadGamblingScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
