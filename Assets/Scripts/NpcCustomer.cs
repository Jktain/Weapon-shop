using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcCustomer : MonoBehaviour
{
    [SerializeField] private string desiredItem = "sword";
    [SerializeField] private int price = 10;
    [SerializeField] private float checkInterval = 1f;

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Sprite swordSprite;
    [SerializeField] private Sprite shieldSprite;

    private GameObject bubbleUI;

    private Transform shopEntrance;

    private Animator animator;
    private NavMeshAgent agent;

    private bool isSatisfied = false;
    private bool hasArrived = false;



    private void Start()
    {
        SpawnBubble();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(shopEntrance.position);
    }

    public void SetShop(Transform target)
    {
        shopEntrance = target;
    }

    private void SpawnBubble()
    {
        bubbleUI = Instantiate(bubblePrefab, transform);
        bubbleUI.transform.localPosition = new Vector3(0, 2, 0);

        var icon = bubbleUI.transform.Find("Icon").GetComponent<Image>();

        switch (desiredItem)
        {
            case "sword":
                icon.sprite = swordSprite;
                break;
            case "shield":
                icon.sprite = shieldSprite;
                break;
        }
    }

    private void Update()
    {
        if (bubbleUI)
        {
            bubbleUI.transform.rotation = Quaternion.LookRotation(bubbleUI.transform.position - Camera.main.transform.position);
        }

        if (animator != null && agent != null)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }

        if (!hasArrived && !agent.pathPending && agent.remainingDistance < 0.2f)
        {
            hasArrived = true;
            InvokeRepeating(nameof(TryBuy), 0f, checkInterval);
        }
    }

    private void TryBuy()
    {
        if (isSatisfied) return;

        bool success = false;

        switch (desiredItem)
        {
            case "sword":
                if (Inventory.swords > 0)
                {
                    Inventory.swords--;
                    Inventory.gold += price;
                    success = true;
                }
                break;

            case "shield":
                if (Inventory.shields > 0)
                {
                    Inventory.shields--;
                    Inventory.gold += price;
                    success = true;
                }
                break;
        }

        if (success)
        {
            isSatisfied = true;
            CancelInvoke(nameof(TryBuy));
            Debug.Log($"NPC купив {desiredItem} за {price} золота. Кількість золота: {Inventory.gold}");
            Destroy(gameObject, 1f);
        }

        if (isSatisfied && bubbleUI)
        {
            Destroy(bubbleUI);
        }
    }
}
