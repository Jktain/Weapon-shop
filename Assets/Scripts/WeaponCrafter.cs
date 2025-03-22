using UnityEngine;

public class WeaponCrafter : MonoBehaviour
{
    [SerializeField] private GameObject spot;
    [SerializeField] private string weaponName = "sword";

    [SerializeField] private float craftTime = 6f;
    [SerializeField] private int sellPrice = 10;

    [SerializeField] private int woodCost = 1;
    [SerializeField] private int metalCost = 2;

    [SerializeField] private int speedUpgradeCost = 30;
    [SerializeField] private int priceUpgradeCost = 40;

    [SerializeField] private int maxLevel = 4;
    [SerializeField] private int speedLevel = 1;
    private int priceLevel = 1;

    private void Start()
    {
        InvokeRepeating(nameof(CraftWeapon), craftTime, craftTime);
    }

    private void CraftWeapon()
    {
        if (Inventory.wood >= woodCost && Inventory.metal >= metalCost)
        {
            Inventory.wood -= woodCost;
            Inventory.metal -= metalCost;

            switch (weaponName)
            {
                case "sword":
                    Inventory.swords += 1;
                    break;
                case "shield":
                    Inventory.shields += 1;
                    break;
            }

            Debug.Log($"{weaponName} створено!");
            Debug.Log($"Створено {Inventory.swords} мечів");
        }
        else
        {
            Debug.Log("Не вистачає ресурсів для крафту.");
        }
    }

    public void UpgradeSpeed()
    {
        if (speedLevel >= maxLevel) return;

        if (Inventory.gold >= speedUpgradeCost)
        {
            Inventory.gold -= speedUpgradeCost;
            craftTime -= 1f;
            speedUpgradeCost += 30;
            speedLevel++;

            ResetTimer();

            Debug.Log(craftTime);
        }
    }

    public void UpgradePrice()
    {
        if (priceLevel >= maxLevel) return;

        if (Inventory.gold >= priceUpgradeCost)
        {
            Inventory.gold -= priceUpgradeCost;
            sellPrice += 5;
            priceUpgradeCost += 35;
            priceLevel++;
        }
    }

    public void BuildSpot()
    {
        Instantiate(spot);
        speedLevel++;
    }

    private void ResetTimer()
    {
        CancelInvoke();
        InvokeRepeating(nameof(CraftWeapon), craftTime, craftTime);
    }

    public int GetSpeedLevel() => speedLevel;
    public int GetPriceLevel() => priceLevel;
    public int GetSellPrice() => sellPrice;

    public void SetLevels(int speed, int price)
    {
        speedLevel = Mathf.Clamp(speed, 1, maxLevel);
        priceLevel = Mathf.Clamp(price, 1, maxLevel);
    }
}
