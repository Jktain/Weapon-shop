using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 6f;
    [SerializeField] private Transform shopEntrance; // точка, куди йде NPC

    private void Start()
    {
        InvokeRepeating(nameof(SpawnNpc), 2f, spawnInterval);
    }

    private void SpawnNpc()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        npc.GetComponent<NpcCustomer>().SetShop(shopEntrance);
    }
}
