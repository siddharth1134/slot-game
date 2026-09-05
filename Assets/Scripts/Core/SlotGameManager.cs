using UnityEngine;

/// <summary>
/// Manages the overall slot game, including player data and game flow.
/// </summary>
public class SlotGameManager : MonoBehaviour
{
    [Header("Game References")]
    [SerializeField]
    private ReelManager reelManager;

    [Header("Player Settings")]
    [SerializeField]
    private int startingBalance = 1000;

    [SerializeField]
    private int startingBet = 10;

    private PlayerData playerData;

    // Prevents multiple spins from happening at the same time.
    private bool isSpinning;

    // Public access for UI and other systems.
    public PlayerData PlayerData => playerData;

    public bool IsSpinning => isSpinning;


    private void Awake()
    {
        InitializePlayer();
    }


    /// <summary>
    /// Creates the player's data when the game starts.
    /// </summary>
    private void InitializePlayer()
    {
        playerData = new PlayerData(startingBalance, startingBet);
    }


    /// <summary>
    /// Main spin action called by the UI.
    /// </summary>
    public void Spin()
    {
        if (isSpinning)
        {
            return;
        }

        // Make sure ReelManager exists.
        if (reelManager == null)
        {
            Debug.LogError("ReelManager is not assigned in the SlotGameManager.");
            return;
        }

        // Make sure PlayerData exists.
        if (playerData == null)
        {
            Debug.LogError("PlayerData is not initialized.");
            return;
        }

        // Check whether the player can afford the bet.
        if (!playerData.CanPlaceBet())
        {
            Debug.LogWarning("Insufficient balance to place the bet.");
            return;
        }

        // Deduct the bet.
        if (!playerData.PlaceBet())
        {
            Debug.LogWarning("Failed to place the bet.");
            return;
        }

        isSpinning = true;

        // Spin all reels.
        reelManager.SpinAll();

        // Evaluate the result.
        int winAmount = reelManager.EvaluateWin(playerData.Bet);

        // Add winnings to the player's balance.
        if (winAmount > 0)
        {
            playerData.AddWinnings(winAmount);
        }

        isSpinning = false;

        Debug.Log(
            $"Spin completed. Win Amount: {winAmount}, New Balance: {playerData.Balance}"
        );
    }
}