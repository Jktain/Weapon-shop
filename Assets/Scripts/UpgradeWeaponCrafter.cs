using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeWeaponCrafter : MonoBehaviour
{
    [SerializeField] private UpgradeUI upgradeUI;
    [SerializeField] private GameObject otherButtons;
    private WeaponCrafter crafter;


    private void Start()
    {
        crafter = GetComponent<WeaponCrafter>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        otherButtons.gameObject.SetActive(false);
        upgradeUI.OpenForCrafter(crafter);
    }
}
