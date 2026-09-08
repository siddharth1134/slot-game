using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages all reels in the slot game.
///
/// Responsibilities:
/// - Starting all reels.
/// - Stopping reels sequentially.
/// - Collecting the final reel results.
/// - Passing results to WinEvaluator.
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

    [SerializeField]
    private float reelStopDelay = 0.25f;

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
    /// Starts all reels spinning.
    /// </summary>
    public void SpinAll()
    {
        if (isSpinning)
        {
            return;
        }

        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning(
                "No reels assigned to the ReelManager."
            );

            return;
        }

        isSpinning = true;

        foreach (Reel reel in reels)
        {
            if (reel == null)
            {
                Debug.LogWarning(
                    "ReelManager contains an empty Reel reference."
                );

                continue;
            }

            reel.Spin();
        }
    }


    /// <summary>
    /// Starts all reels, waits for the spin duration,
    /// stops each reel one after another,
    /// and finally notifies the caller.
    /// </summary>
    public void SpinAllAndWait(Action onComplete)
    {
        if (isSpinning)
        {
            return;
        }

        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning(
                "No reels assigned to the ReelManager."
            );

            return;
        }

        StartCoroutine(
            SpinRoutine(onComplete)
        );
    }


    /// <summary>
    /// Controls the complete spin lifecycle.
    ///
    /// All reels start together.
    /// After the main spin duration, each reel stops
    /// sequentially with a small delay between them.
    /// </summary>
    private IEnumerator SpinRoutine(Action onComplete)
    {
        // Start all reels.
        SpinAll();

        // Allow the reels to spin normally.
        yield return new WaitForSeconds(spinDuration);

        // Stop reels one by one.
        for (int i = 0; i < reels.Length; i++)
        {
            if (reels[i] == null)
            {
                continue;
            }

            // Stop this reel.
            reels[i].Stop();

            // Wait before stopping the next reel.
            if (i < reels.Length - 1)
            {
                yield return new WaitForSeconds(
                    reelStopDelay
                );
            }
        }

        // The complete reel sequence has finished.
        isSpinning = false;

        // Tell SlotGameManager that the spin is complete.
        onComplete?.Invoke();
    }


    /// <summary>
    /// Stops all reels immediately.
    ///
    /// This is useful for emergency/manual stopping.
    /// Normal gameplay uses the sequential stopping
    /// inside SpinRoutine().
    /// </summary>
    public void StopAll()
    {
        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning(
                "No reels assigned to the ReelManager."
            );

            return;
        }

        foreach (Reel reel in reels)
        {
            if (reel == null)
            {
                Debug.LogWarning(
                    "ReelManager contains an empty Reel reference."
                );

                continue;
            }

            reel.Stop();
        }

        isSpinning = false;
    }


    /// <summary>
    /// Gets the current symbol from every reel.
    /// </summary>
    public SymbolData[] GetResults()
    {
        if (reels == null || reels.Length == 0)
        {
            Debug.LogWarning(
                "ReelManager contains no reels."
            );

            return Array.Empty<SymbolData>();
        }

        SymbolData[] results =
            new SymbolData[reels.Length];

        for (int i = 0; i < reels.Length; i++)
        {
            if (reels[i] == null)
            {
                Debug.LogWarning(
                    $"Reel at index {i} is null."
                );

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
            Debug.LogWarning(
                "WinEvaluator is not assigned in ReelManager."
            );

            return 0;
        }

        SymbolData[] results = GetResults();

        return winEvaluator.Evaluate(
            results,
            bet
        );
    }
}