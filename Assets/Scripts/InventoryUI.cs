using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI[] itemRows;


    private void Start()
    {
        SetInventoryItemTxts();
    }

    private void Update()
    {
        SetInventoryItemTxts();
    }

    private void SetInventoryItemTxts()
    {
        for (int i = 0; i < itemRows.Length; i++)
        {
            int count = Inventory.itemCounts[i];
            int price = Inventory.itemPrices[i];

            itemRows[i].Setup(count, price);
        }
    }
}
