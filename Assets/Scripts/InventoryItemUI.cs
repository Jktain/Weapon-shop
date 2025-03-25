using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Setup(int count, int price)
    {
        countText.text = count.ToString();
        priceText.text = price.ToString();
    }
}
