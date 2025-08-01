using System.Collections.Generic;
using UnityEngine;

public class ShopQueueManager : MonoBehaviour
{
    [SerializeField] private Transform[] queuePoints;

    private List<NpcCustomer> npcQueue = new();

    public Transform GetAvailableQueuePosition()
    {
        if (npcQueue.Count >= queuePoints.Length)
            return null;

        return queuePoints[npcQueue.Count];
    }
    public bool IsFirstInQueue(NpcCustomer npc)
    {
        return npcQueue.Count > 0 && npcQueue[0] == npc;
    }

    public void JoinQueue(NpcCustomer npc)
    {
        npcQueue.Add(npc);
        UpdateQueuePositions();
    }

    public void LeaveQueue(NpcCustomer npc)
    {
        if (npcQueue.Contains(npc))
        {
            npcQueue.Remove(npc);
            UpdateQueuePositions();
        }
    }

    private void UpdateQueuePositions()
    {
        for (int i = 0; i < npcQueue.Count; i++)
        {
            npcQueue[i].MoveTo(queuePoints[i].position);
        }
    }
}
