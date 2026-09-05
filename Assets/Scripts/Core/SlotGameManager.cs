using UnityEngine;

/// <summary>
/// Manages the overall slot game, including player data,
/// reel spinning, win evaluation, and UI updates.
/// </summary>
public class SlotGameManager : MonoBehaviour
{
    [Header("Game References")]
    [SerializeField]
    private ReelManager reelManager;

    [SerializeField]
    private UIManager uiManager;

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


    private void Start()
    {
        if (reelManager == null)
        {
            Debug.LogError("ReelManager is not assigned in SlotGameManager.");
        }

        if (uiManager == null)
        {
            Debug.LogError("UIManager is not assigned in SlotGameManager.");
        }

        UpdateUI();
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
        // Prevent another spin while one is already running.
        if (isSpinning)
        {
            return;
        }

        // Make sure ReelManager exists.
        if (reelManager == null)
        {
            Debug.LogError("ReelManager is not assigned in SlotGameManager.");
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

            if (uiManager != null)
            {
                uiManager.SetSpinButtonInteractable(false);
            }

            return;
        }

        // Deduct the bet.
        if (!playerData.PlaceBet())
        {
            Debug.LogWarning("Failed to place the bet.");
            return;
        }

        // Mark the game as spinning.
        isSpinning = true;

        // Disable the Spin button while reels are spinning.
        if (uiManager != null)
        {
            uiManager.SetSpinButtonInteractable(false);
            uiManager.UpdateUI();
        }

        // Start the reel spin.
        // The result will be evaluated AFTER the reels stop.
        reelManager.SpinAllAndWait(OnSpinComplete);
    }


    /// <summary>
    /// Called by ReelManager after the reels finish spinning.
    /// </summary>
    private void OnSpinComplete()
    {
        // Make sure PlayerData still exists.
        if (playerData == null)
        {
            Debug.LogError("PlayerData is missing after spin.");

            isSpinning = false;
            return;
        }

        // Evaluate the result AFTER the reels have stopped.
        int winAmount = reelManager.EvaluateWin(playerData.Bet);

        // Add winnings to the player's balance.
        if (winAmount > 0)
        {
            playerData.AddWinnings(winAmount);
        }

        // Update UI.
        if (uiManager != null)
        {
            uiManager.UpdateUI();
            uiManager.ShowWin(winAmount);
        }

        // Spin is now finished.
        isSpinning = false;

        // Enable Spin button if the player can afford another bet.
        if (uiManager != null)
        {
            uiManager.SetSpinButtonInteractable(
                playerData.CanPlaceBet()
            );
        }

        Debug.Log(
            $"Spin completed. Win Amount: {winAmount}, New Balance: {playerData.Balance}"
        );
    }


    /// <summary>
    /// Updates the UI with the current player data.
    /// </summary>
    private void UpdateUI()
    {
        if (uiManager == null || playerData == null)
        {
            return;
        }

        uiManager.UpdateUI();
        uiManager.ShowWin(0);

        uiManager.SetSpinButtonInteractable(
            playerData.CanPlaceBet()
        );
    }
}