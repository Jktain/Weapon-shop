using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    [SerializeField] private string resourceName = "wood";

    [SerializeField] private int amountPerTick = 1;
    [SerializeField] private float tickRate = 5f;

    [SerializeField] private int speedUpgradeCost = 20;
    [SerializeField] private int amountUpgradeCost = 25;

    [SerializeField] private int maxLevel = 4;
    private int speedLevel = 1;
    private int amountLevel = 1;

    private void Start()
    {
        InvokeRepeating(nameof(ProduceResource), tickRate, tickRate);
    }

    private void ProduceResource()
    {
        switch (resourceName)
        {
            case "wood":
                Inventory.wood += amountPerTick;
                break;
            case "metal":
                Inventory.metal += amountPerTick;
                break;
        }
    }

    public void UpgradeSpeed()
    {
        if (speedLevel >= maxLevel) return;

        if (Inventory.gold >= speedUpgradeCost)
        {
            Inventory.gold -= speedUpgradeCost;
            tickRate -= 1f;
            speedUpgradeCost += 15;
            speedLevel++;

            ResetTimer();
            Debug.Log(tickRate);
        }
    }

    public void UpgradeAmount()
    {
        if (amountLevel >= maxLevel) return;

        if (Inventory.gold >= amountUpgradeCost)
        {
            Inventory.gold -= amountUpgradeCost;
            amountPerTick += 1;
            amountUpgradeCost += 20;
            amountLevel++;
        }
    }

    private void ResetTimer()
    {
        CancelInvoke();
        InvokeRepeating(nameof(ProduceResource), tickRate, tickRate);
    }

    // Методи для збереження
    public int GetSpeedLevel() => speedLevel;
    public int GetAmountLevel() => amountLevel;
    public void SetLevels(int speed, int amount)
    {
        speedLevel = Mathf.Clamp(speed, 1, maxLevel);
        amountLevel = Mathf.Clamp(amount, 1, maxLevel);
    }
}
