using UnityEngine;

[System.Serializable]
public class ShopItemSO
{
    public string itemName;    // ürün adı
    public Sprite icon;        // ürün resmi
    public int price;          // fiyat
    public int boughtAmount;   // kaç tane satın alındı
    public GameObject OnInspectorIcon;
}

[CreateAssetMenu(fileName = "ShopDatabase", menuName = "Shop/Database")]
public class ShopDatabaseSO : ScriptableObject
{
        public int money = 1000000000; // 1 milyar başlangıç parası
   
    public ShopItemSO[] items; // tüm itemler burada tutulur
}
