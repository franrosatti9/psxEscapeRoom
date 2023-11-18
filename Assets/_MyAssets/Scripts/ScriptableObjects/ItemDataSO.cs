using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Item", fileName = "New Item")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public int itemID;
}
