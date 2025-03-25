using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcCustomer : MonoBehaviour
{
    [SerializeField] private int desiredItemIndex = 0;
    [SerializeField] private int price = 10;
    [SerializeField] private float checkInterval = 1f;

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Sprite[] itemsSprite;
    private Vector3 spawnPoint;
    private bool returning = false;
    private GameObject bubbleUI;

    private Transform shopEntrance;

    private Animator animator;
    private NavMeshAgent agent;

    private bool isSatisfied = false;
    private bool hasArrived = false;



    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        ShopQueueManager queue = FindObjectOfType<ShopQueueManager>();
        var position = queue.GetAvailableQueuePosition();

        if (position != null)
        {
            queue.JoinQueue(this);
            agent.SetDestination(position.position);
        }
        else
        {
            Debug.Log("Черга повна, NPC не буде обслуговуватись.");
            Destroy(gameObject);
        }

        desiredItemIndex = Random.Range(0, Inventory.buildedCrafters.Count);
        SpawnBubble();
    }

    public void SetSpawnPoint(Vector3 point)
    {
        spawnPoint = point;
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
        icon.sprite = itemsSprite[desiredItemIndex];
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

        if (returning && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 target)
    {
        if (agent != null)
            agent.SetDestination(target);
    }

    private void TryBuy()
    {
        if (isSatisfied) return;

        var queue = FindObjectOfType<ShopQueueManager>();
        if (!queue.IsFirstInQueue(this)) return;

        bool success = false;

        if (Inventory.itemCounts[desiredItemIndex] > 0)
        {
            Inventory.itemCounts[desiredItemIndex]--;
            Inventory.gold += price;
            success = true;
        }

        if (success)
        {
            isSatisfied = true;
            CancelInvoke(nameof(TryBuy));

            queue.LeaveQueue(this);

            Destroy(bubbleUI);

            returning = true;
            agent.SetDestination(spawnPoint);
            NpcSpawner.currentNpcCount--;
        }
    }
}
