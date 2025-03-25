using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int metal = 0;
    public static int wood = 0;
    public static int gold = 100;

    // [0] = sword, [1] = shield, [2] = bow, [3] =  
    public static string[] itemNames = {"sword", "shield", "bow", "axe"};
    public static int[] itemCounts = {0, 0, 0, 0};
    public static int[] itemPrices = { 0, 0, 0, 0};

    public static List<string> buildedCrafters = new List<string> {"sword"};
}
