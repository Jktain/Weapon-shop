using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeResourseProducer : MonoBehaviour
{
    [SerializeField] private UpgradeUI upgradeUI;
    [SerializeField] private GameObject otherButtons;
    private ResourceProducer producer;

    private void Start()
    {
        producer = GetComponent<ResourceProducer>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        otherButtons.gameObject.SetActive(false);
        upgradeUI.OpenForProducer(producer);
    }
}
