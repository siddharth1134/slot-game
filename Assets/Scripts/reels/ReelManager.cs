using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages all reels in the slot game.
/// Responsible for starting, stopping, and collecting reel results.
/// </summary>
public class ReelManager : MonoBehaviour
{
    [Header("Reel References")]
    [SerializeField]
    private Reel[] reels;

    [Header("Evaluation")]
    [SerializeField]
    private WinEvaluator winEvaluator;

    [Header("Spin Settings")]
    [SerializeField]
    private float spinDuration = 2f;

    private bool isSpinning;

    /// <summary>
    /// Exposes the reels for other systems.
    /// </summary>
    public Reel[] Reels => reels;

    /// <summary>
    /// Returns true while the reels are spinning.
    /// </summary>
    public bool IsSpinning => isSpinning;


    /// <summary>
    /// Starts a spin of all reels.
    /// </summary>
    public void SpinAll()
    {
        if (isSpinning)
        {
            return;
        }

        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning("No reels assigned to the ReelManager.");
            return;
        }

        isSpinning = true;

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
    /// Spins all reels, waits for the spin duration,
    /// stops the reels, and then notifies the caller.
    /// </summary>
    public void SpinAllAndWait(Action onComplete)
    {
        if (isSpinning)
        {
            return;
        }

        StartCoroutine(SpinRoutine(onComplete));
    }


    /// <summary>
    /// Controls the complete reel spin lifecycle.
    /// </summary>
    private IEnumerator SpinRoutine(Action onComplete)
    {
        SpinAll();

        yield return new WaitForSeconds(spinDuration);

        StopAll();

        isSpinning = false;

        onComplete?.Invoke();
    }


    /// <summary>
    /// Stops all reels.
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
            return Array.Empty<SymbolData>();
        }

        SymbolData[] results = new SymbolData[reels.Length];

        for (int i = 0; i < reels.Length; i++)
        {
            if (reels[i] == null)
            {
                Debug.LogWarning($"Reel at index {i} is null.");
                continue;
            }

            results[i] = reels[i].CurrentSymbol;
        }

        return results;
    }


    /// <summary>
    /// Evaluates the current reel result.
    /// </summary>
    public int EvaluateWin(int bet)
    {
        if (winEvaluator == null)
        {
            Debug.LogWarning("WinEvaluator is not assigned in ReelManager.");
            return 0;
        }

        SymbolData[] results = GetResults();

        return winEvaluator.Evaluate(results, bet);
    }
}