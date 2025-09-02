using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopDatabaseSO database;   // ScriptableObject Database
    public Transform contentParent;   // ScrollView -> Viewport -> Content
    public GameObject itemPrefab;     // Senin gönderdiğin prefab
    public Transform WorldParent;

    public AudioSource MoneySpendAuSo;
    public GameObject SpendMoneyPanel;

    void Start()
    {
        GenerateShop();
        SpawnAlreadyBoughtItems(); // 🔥 oyun başında satın alınmışları yükle
    }

    void GenerateShop()
    {
        foreach (var item in database.items)
        {
            
            GameObject newItem = Instantiate(itemPrefab, contentParent);

            // Çocuklarını bul
            Image icon = newItem.transform.Find("Icon").GetComponent<Image>();
            TMP_Text nameTMP = newItem.transform.Find("ItemNameTMP").GetComponent<TMP_Text>();
            TMP_Text priceTMP = newItem.transform.Find("ItemPriceTMP").GetComponent<TMP_Text>();
            TMP_Text amountTMP = newItem.transform.Find("WasBuyedAmount").GetComponent<TMP_Text>();
            Button buyBtn = newItem.transform.Find("BuyBtn").GetComponent<Button>();
            Button sellBtn = newItem.transform.Find("SellBtn").GetComponent<Button>();

            // Verileri doldur
            icon.sprite = item.icon;
            nameTMP.text = item.itemName;
            priceTMP.text = "$" + item.price.ToString("N0");
            amountTMP.text = "x" + item.boughtAmount;

            // Butonlara event bağla
            buyBtn.onClick.AddListener(() =>
            {
                BuyItem(item, amountTMP);
            });

            sellBtn.onClick.AddListener(() =>
            {
                SellItem(item, amountTMP);
            });
        }
        AdjustContentHeight();
    }

    void BuyItem(ShopItemSO item, TMP_Text amountTMP)
    {
        if (database.money >= item.price)
        {
            database.money -= item.price;
            item.boughtAmount++;
            amountTMP.text = "x" + item.boughtAmount;
            Instantiate(item.OnInspectorIcon, WorldParent);
            MoneySpendAuSo.Play();
        }
    }

    void SellItem(ShopItemSO item, TMP_Text amountTMP)
    {
        if (item.boughtAmount > 0)
        {
            item.boughtAmount--;
            database.money += item.price;
            amountTMP.text = "x" + item.boughtAmount;
        }
    }

    void AdjustContentHeight()
    {
        float itemHeight = itemPrefab.GetComponent<RectTransform>().sizeDelta.y;
        float spacing = 110f;
        int totalItems = database.items.Length;

        float newHeight = (itemHeight + spacing) * totalItems;
        contentParent.GetComponent<RectTransform>().sizeDelta =
            new Vector2(contentParent.GetComponent<RectTransform>().sizeDelta.x, newHeight);
    }

    void SpawnAlreadyBoughtItems()
    {
        foreach (var item in database.items)
        {
            if (item.boughtAmount > 0)
            {
                for (int i = 0; i < item.boughtAmount; i++)
                {
                    if (item.OnInspectorIcon == null)
                        return;
                    Instantiate(item.OnInspectorIcon, WorldParent);
                }
            }
        }
    }

    public void CloseSpendMPanel()
    {
        SpendMoneyPanel.SetActive(false);
    }

    public void OpenSpendMoneyPanel()
    {
        SpendMoneyPanel.SetActive(true);
    }
}
