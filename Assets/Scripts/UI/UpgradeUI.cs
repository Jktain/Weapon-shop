using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private GameObject buildSection;
    [SerializeField] private Button buildButton;

    [SerializeField] private GameObject upgradeSection;
    [SerializeField] private TextMeshProUGUI buildCostText;
    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI speedUpgradeCostText;
    [SerializeField] private TextMeshProUGUI secondUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI secondUpgradeCostText;
    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private Button upgradeSecondButton;

    private WeaponCrafter currentCrafter;
    private ResourceProducer currentProducer;

    public static int[] speedUpgradeLevels = { 1, 0, 0, 0, 1, 1};
    public static int[] secondUpgradeLevels = { 1, 1, 1, 1, 1, 1 };

    public void OpenForCrafter(WeaponCrafter crafter)
    {
        currentCrafter = crafter;
        currentProducer = null;
        panel.SetActive(true);

        if (speedUpgradeLevels[crafter.weaponIndex] == 0)
        {
            buildSection.SetActive(true);
            upgradeSection.SetActive(false);
        }
        else
        {
            buildSection.SetActive(false);
            upgradeSection.SetActive(true);
            UpdateCrafterUI();
        }

        titleText.text = crafter.name;
    }

    public void OpenForProducer(ResourceProducer producer)
    {
        currentProducer = producer;
        currentCrafter = null;
        panel.SetActive(true);

        buildSection.SetActive(false);
        upgradeSection.SetActive(true);

        UpdateProducerUI();
        titleText.text = producer.name;
    }

    public void Build()
    {
        currentCrafter?.BuildSpot();
        OpenForCrafter(currentCrafter);
    }

    public void UpgradeSpeed()
    {
        if (currentCrafter != null)
        {
            currentCrafter.UpgradeSpeed();
            UpdateCrafterUI();
        }
        else if (currentProducer != null)
        {
            currentProducer.UpgradeSpeed();
            UpdateProducerUI();
        }
    }

    public void UpgradeSecond()
    {
        if (currentCrafter != null)
        {
            currentCrafter.UpgradePrice();
            UpdateCrafterUI();
        }
        else if (currentProducer != null)
        {
            currentProducer.UpgradeAmount();
            UpdateProducerUI();
        }
    }

    private void UpdateCrafterUI()
    {
        speedLevelText.text = $"Produce speed\n{speedUpgradeLevels[currentCrafter.weaponIndex]} lvl";
        speedUpgradeCostText.text = $"{currentCrafter.speedUpgradeCost}";
        secondUpgradeLevelText.text = $"Sell level\n{secondUpgradeLevels[currentCrafter.weaponIndex]} lvl";
        secondUpgradeCostText.text = $"{currentCrafter.priceUpgradeCost}";
    }

    private void UpdateProducerUI()
    {
        speedLevelText.text = $"Produce speed\n{speedUpgradeLevels[4 + currentProducer.resourceIndex]} lvl";
        speedUpgradeCostText.text = $"{currentProducer.speedUpgradeCost}";
        secondUpgradeLevelText.text = $"Produce amount\n{secondUpgradeLevels[4 + currentProducer.resourceIndex]} lvl";
        secondUpgradeCostText.text = $"{currentProducer.amountUpgradeCost}";
    }
}
