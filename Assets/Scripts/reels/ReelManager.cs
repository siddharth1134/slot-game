using UnityEngine;

public class ReelManager : MonoBehaviour
{
    // Array of Reel objects that represent the reels in the game
    [SerializeField]
    private Reel[] reels;

     [SerializeField]
    private WinEvaluator winEvaluator;

    // Exposes the reels for other scripts
    public Reel[] Reels => reels;

    /// <summary>
    /// Spins all reels in the game.
    /// </summary>
    public void SpinAll()
    {
        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning("No reels assigned to the ReelManager.");
            return;
        }

        foreach (Reel reel in reels)
        {
            if (reel == null)
            {
                Debug.LogWarning("ReelManager contains an empty Reel reference.");
                continue;
            }

            reel.Spin();
        }
    }

    /// <summary>
    /// Stops all reels in the game.
    /// </summary>
    public void StopAll()
    {
        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning("No reels assigned to the ReelManager.");
            return;
        }

        foreach (Reel reel in reels)
        {
            if (reel == null)
            {
                Debug.LogWarning("ReelManager contains an empty Reel reference.");
                continue;
            }

            reel.Stop();
        }
    }

    /// <summary>
    /// Gets the current symbol from every reel.
    /// </summary>
    public SymbolData[] GetResults()
    {
        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning("ReelManager contains no reels.");
            return new SymbolData[0];
        }

        SymbolData[] results = new SymbolData[reels.Length];

        for (int i = 0; i < reels.Length; i++)
        {
            if (reels[i] != null)
            {
                results[i] = reels[i].CurrentSymbol;
            }
            else
            {
                Debug.LogWarning($"Reel at index {i} is null.");
                results[i] = null;
            }
        }

        return results;
    }


public int EvaluateWin(int bet)
{
    if(winEvaluator == null)
    {
        Debug.LogWarning("WinEvaluator is not assigned in ReelManager.");
        return 0;
    }
SymbolData[] results = GetResults();
    return winEvaluator.Evaluate(results, bet);
}
    
}
