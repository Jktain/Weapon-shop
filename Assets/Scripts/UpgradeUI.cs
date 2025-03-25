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
    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI secondUpgradeLevelText;
    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private Button upgradeSecondButton;

    private WeaponCrafter currentCrafter;
    private ResourceProducer currentProducer;

    public void OpenForCrafter(WeaponCrafter crafter)
    {
        currentCrafter = crafter;
        currentProducer = null;
        panel.SetActive(true);

        if (crafter.GetSpeedLevel() == 0)
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
        OpenForCrafter(currentCrafter); // ��������� ������ �� ������ ����������
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
        speedLevelText.text = $"��������: {currentCrafter.GetSpeedLevel()} �����";
        secondUpgradeLevelText.text = $"ֳ�� �������: {currentCrafter.GetPriceLevel()} �����";
    }

    private void UpdateProducerUI()
    {
        speedLevelText.text = $"��������: {currentProducer.GetSpeedLevel()} �����";
        secondUpgradeLevelText.text = $"ʳ������ �� ��: {currentProducer.GetAmountLevel()} �����";
    }

    public void Close() => panel.SetActive(false);
}
