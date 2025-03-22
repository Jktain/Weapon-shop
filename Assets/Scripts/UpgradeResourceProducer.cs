using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeResourseProducer : MonoBehaviour
{
    [SerializeField] private UpgradeUI upgradeUI;
    private ResourceProducer producer;

    private void Start()
    {
        producer = GetComponent<ResourceProducer>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        upgradeUI.OpenForProducer(producer);
    }
}
