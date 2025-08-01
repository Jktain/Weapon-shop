using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // wood, metal
    public static int[] resourceCounts = { 0, 0 };
    public static int gold = 300;

    public static string[] itemNames = {"sword", "shield", "bow", "axe"};
    public static int[] itemCounts = {0, 0, 0, 0};
    public static int[] itemPrices = {20, 0, 0, 0};
}
