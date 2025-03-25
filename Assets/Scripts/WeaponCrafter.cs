using UnityEngine;

public class WeaponCrafter : MonoBehaviour
{
    [SerializeField] private GameObject spot;

    [SerializeField] private float craftTime = 6f;
    [SerializeField] private int sellPrice = 10;

    [SerializeField] private int woodCost = 1;
    [SerializeField] private int metalCost = 2;

    [SerializeField] private int maxLevel = 4;

    public int weaponIndex = 0;
    public int speedUpgradeCost;
    public int priceUpgradeCost = 40;

    private void Start()
    {
        if(UpgradeUI.speedUpgradeLevels[weaponIndex] > 0)
        {
            Inventory.gold += priceUpgradeCost;
            BuildSpot();
        }
    }

    private void CraftWeapon()
    {
        if (Inventory.resourceCounts[0] >= woodCost && Inventory.resourceCounts[1] >= metalCost)
        {
            Inventory.resourceCounts[0] -= woodCost;
            Inventory.resourceCounts[1] -= metalCost;
            Inventory.itemCounts[weaponIndex]++;
        }
        else
        {
            Debug.Log("Не вистачає ресурсів для крафту.");
        }
    }

    public void UpgradeSpeed()
    {
        if (UpgradeUI.speedUpgradeLevels[weaponIndex] >= maxLevel) return;

        if (Inventory.gold >= speedUpgradeCost)
        {
            Inventory.gold -= speedUpgradeCost;
            craftTime -= 1f;
            speedUpgradeCost += 30;
            UpgradeUI.speedUpgradeLevels[weaponIndex]++;

            ResetTimer();
        }
    }

    public void UpgradePrice()
    {
        if (UpgradeUI.secondUpgradeLevels[weaponIndex] >= maxLevel) return;

        if (Inventory.gold >= priceUpgradeCost)
        {
            Inventory.gold -= priceUpgradeCost;
            Inventory.itemPrices[weaponIndex] += 5;
            priceUpgradeCost += 35;
            UpgradeUI.secondUpgradeLevels[weaponIndex]++;
        }
    }

    public void BuildSpot()
    {
        if (Inventory.gold >= priceUpgradeCost)
        {
            Inventory.gold -= priceUpgradeCost;
            Instantiate(spot, transform);

            if(UpgradeUI.speedUpgradeLevels[weaponIndex] < 1)
            {
                UpgradeUI.speedUpgradeLevels[weaponIndex] = 1;
            }

            Inventory.buildedCrafters.Add(Inventory.itemNames[weaponIndex]);

            speedUpgradeCost = speedUpgradeCost * UpgradeUI.speedUpgradeLevels[weaponIndex];
            craftTime = craftTime - 1f * (UpgradeUI.speedUpgradeLevels[weaponIndex] - 1);
            priceUpgradeCost = priceUpgradeCost * UpgradeUI.secondUpgradeLevels[weaponIndex];

            if(Inventory.itemPrices[weaponIndex] <= 0)
            {
                Inventory.itemPrices[weaponIndex] = sellPrice;
            }

            InvokeRepeating(nameof(CraftWeapon), craftTime, craftTime);
        }
    }

    private void ResetTimer()
    {
        CancelInvoke();
        InvokeRepeating(nameof(CraftWeapon), craftTime, craftTime);
    }
}
