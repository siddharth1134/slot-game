using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Paytable",
    menuName = "Data/Paytable"
)]
public class Paytable : ScriptableObject
{
    [SerializeField]
    private List<SymbolPaytableEntry> entries = new List<SymbolPaytableEntry>();

    public int GetMultiplier(SymbolData symbol, int matchCount)
    {
        foreach (SymbolPaytableEntry entry in entries)
        {
            if (entry.Symbol == symbol)
            {
                return entry.GetMultiplier(matchCount);
            }
        }

        return 0;
    }
}

[Serializable]
public class SymbolPaytableEntry
{
    [SerializeField]
    private SymbolData symbol;

    [SerializeField]
    private List<Payout> payouts = new List<Payout>();

    public SymbolData Symbol => symbol;

    public int GetMultiplier(int matchCount)
    {
        foreach (Payout payout in payouts)
        {
            if (payout.MatchCount == matchCount)
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
    [SerializeField]
    private int matchCount;

    [SerializeField]
    private int multiplier;

    public int MatchCount => matchCount;
    public int Multiplier => multiplier;
}