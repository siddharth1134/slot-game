using Sysyem;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu{
    fileName = "Paytable",
    menuName = "Data/Paytable"
}]
public class Paytable : scriptableObject
{
    [SerializeField] private List<SymbolpaytableEntry> entries = new List<SymbolpaytableEntry>();

    public int GetMultiplier(SymbolData symbol, int matchCount)
    {
        foreach(SymbolpaytableEntry entry in entries)
        {
            if(entry.Symbol == symbol && entry.MatchCount == matchCount)
            {
                return entry.Multiplier;
            }
        }
        return 0;
    }
}
[Serializable]

public class SymbolPaytableEntry
{
     [serializeField] private SymbolData symbol;

     [serializeField] private List<Payout> payouts = new List<Payout>();

     public SymbolData Symbol => symbol;

    public int GetMultiplier(int matchCount)
    {
        foreach(Payout payout in payouts)
        {
            if(payout.MatchCount == matchCount)
            {
                return payout.Multiplier;
            }
        }
        return 0;
    }

}
[Serializable]
public class Payout
{
    [SerializeField] private int matchCount;
    

    [SerializeField] private int multiplier;

    public int MatchCount => matchCount;
    public int Multiplier => multiplier;
}
