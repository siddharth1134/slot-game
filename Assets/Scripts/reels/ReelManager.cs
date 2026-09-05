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

// temporary testing method to simulate a spin and evaluate the result
[ContextMenu("Test Spin And Evaluate")]
public void TestSpinAndEvaluate()
{
    int testBet = 10;

    SpinAll();

    SymbolData[] results = GetResults();

    Debug.Log("=== SLOT TEST ===");

    for (int i = 0; i < results.Length; i++)
    {
        if (results[i] != null)
        {
            Debug.Log($"Reel {i + 1}: {results[i].name}");
        }
        else
        {
            Debug.Log($"Reel {i + 1}: NULL");
        }
    }

    int winAmount = EvaluateWin(testBet);

    Debug.Log($"Test Bet: {testBet}");
    Debug.Log($"Win Amount: {winAmount}");
}

// temporary testing method to simulate a winning result and evaluate it
[ContextMenu("Test Winning Result")]
public void TestWinningResult()
{
    int testBet = 10;

    // Validate reels
    if (reels == null || reels.Length < 3)
    {
        Debug.LogWarning("At least 3 reels are required for the winning test.");
        return;
    }

    // Validate WinEvaluator
    if (winEvaluator == null)
    {
        Debug.LogWarning("WinEvaluator is not assigned in ReelManager.");
        return;
    }

    // Get the first reel's current symbol
    SymbolData winningSymbol = reels[0].CurrentSymbol;

    if (winningSymbol == null)
    {
        Debug.LogWarning("Reel 1 has no current symbol. Run Test Spin And Evaluate first.");
        return;
    }

    // Create a controlled winning result
    SymbolData[] testResults = new SymbolData[]
    {
        winningSymbol,
        winningSymbol,
        winningSymbol
    };

    // Evaluate the controlled result
    int winAmount = winEvaluator.Evaluate(testResults, testBet);

    Debug.Log("=== WINNING TEST ===");
    Debug.Log($"Symbol: {winningSymbol.name}");
    Debug.Log($"Match Count: {testResults.Length}");
    Debug.Log($"Test Bet: {testBet}");
    Debug.Log($"Win Amount: {winAmount}");
}


    
}
