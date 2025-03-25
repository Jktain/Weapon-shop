using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    [SerializeField] private int amountPerTick = 1;
    [SerializeField] private float tickRate = 5f;

    [SerializeField] private int maxLevel = 4;

    public int resourceIndex = 0;
    public int speedUpgradeCost;
    public int amountUpgradeCost = 40;

    private void Start()
    {
        speedUpgradeCost = speedUpgradeCost * UpgradeUI.speedUpgradeLevels[4 + resourceIndex];
        tickRate = tickRate - 1f * (UpgradeUI.speedUpgradeLevels[4 + resourceIndex] - 1);
        amountUpgradeCost = amountUpgradeCost * UpgradeUI.secondUpgradeLevels[4 + resourceIndex];
        amountPerTick = amountPerTick * UpgradeUI.secondUpgradeLevels[4 + resourceIndex];

        InvokeRepeating(nameof(ProduceResource), tickRate, tickRate);
    }

    private void ProduceResource()
    {
        Inventory.resourceCounts[resourceIndex] += amountPerTick;
    }

    public void UpgradeSpeed()
    {
        if (UpgradeUI.speedUpgradeLevels[4 + resourceIndex] >= maxLevel) return;

        if (Inventory.gold >= speedUpgradeCost)
        {
            Inventory.gold -= speedUpgradeCost;
            tickRate -= 1f;
            speedUpgradeCost += 15;
            UpgradeUI.speedUpgradeLevels[4 + resourceIndex]++;

            ResetTimer();
            Debug.Log(tickRate);
        }
    }

    public void UpgradeAmount()
    {
        if (UpgradeUI.secondUpgradeLevels[4 + resourceIndex] >= maxLevel) return;

        if (Inventory.gold >= amountUpgradeCost)
        {
            Inventory.gold -= amountUpgradeCost;
            amountPerTick += 1;
            amountUpgradeCost += 20;
            UpgradeUI.secondUpgradeLevels[4 + resourceIndex]++;
        }
    }

    private void ResetTimer()
    {
        CancelInvoke();
        InvokeRepeating(nameof(ProduceResource), tickRate, tickRate);
    }
}
