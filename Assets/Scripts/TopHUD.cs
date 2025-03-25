using UnityEngine;
using TMPro;

public class TopHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI metalText;

    private void Update()
    {
        goldText.text = FormatUtils.FormatNumber(Inventory.gold);
        woodText.text = FormatUtils.FormatNumber(Inventory.wood);
        metalText.text = FormatUtils.FormatNumber(Inventory.metal);
    }
}
