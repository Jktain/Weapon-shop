using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeWeaponCrafter : MonoBehaviour
{
    [SerializeField] private UpgradeUI upgradeUI;
    [SerializeField] private GameObject panel;
    private WeaponCrafter crafter;


    private void Start()
    {
        crafter = GetComponent<WeaponCrafter>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        upgradeUI.OpenForCrafter(crafter);
    }
}
