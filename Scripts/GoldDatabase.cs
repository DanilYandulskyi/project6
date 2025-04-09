using UnityEngine;
using System.Collections.Generic;

public class GoldDatabase : MonoBehaviour
{
    private List<Gold> _takenGold = new List<Gold>();

    public List<Gold> GetFreeGold(List<Gold> goldList)
    {
        List<Gold> returningList = new List<Gold>();

        foreach (var gold in goldList)
        {
            if (_takenGold.Contains(gold) == false)
            {
                returningList.Add(gold);
            }
        }

        return returningList;
    }

    public void ReserveGold(Gold gold)
    {
        _takenGold.Add(gold);
    }
}
