using UnityEngine;

/// <summary>
/// Manages the overall slot game.
/// Handles player balance, betting, reel spinning,
/// win evaluation, and UI updates.
/// </summary>
public class SlotGameManager : MonoBehaviour
{
    [Header("Game References")]
    [SerializeField]
    private ReelManager reelManager;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private Animator spinButtonAnimator;

    [Header("Player Settings")]
    [SerializeField]
    private int startingBalance = 1000;

    [SerializeField]
    private int startingBet = 10;

    private PlayerData playerData;

    private bool isSpinning;

    public PlayerData PlayerData => playerData;

    public bool IsSpinning => isSpinning;


    private void Awake()
    {
        InitializePlayer();
    }


    /// <summary>
    /// Initializes the game by setting up the player data
    /// and updating the UI.
    /// </summary>
    private void Start()
    {
        if (reelManager == null)
        {
            Debug.LogError(
                "ReelManager is not assigned in SlotGameManager."
            );
        }

        if (uiManager == null)
        {
            Debug.LogError(
                "UIManager is not assigned in SlotGameManager."
            );
        }

        if (spinButtonAnimator == null)
        {
            Debug.LogError(
                "Spin Button Animator is not assigned in SlotGameManager."
            );
        }

        UpdateUI();
    }


    /// <summary>
    /// Creates the player's data.
    /// </summary>
    private void InitializePlayer()
    {
        playerData = new PlayerData(
            startingBalance,
            startingBet
        );
    }


    /// <summary>
    /// Called by the Spin button.
    /// </summary>
    public void Spin()
    {
        if (isSpinning)
        {
            return;
        }

        if (reelManager == null)
        {
            Debug.LogError(
                "ReelManager is not assigned."
            );
            return;
        }

        if (playerData == null)
        {
            Debug.LogError(
                "PlayerData is not initialized."
            );
            return;
        }

        // Check if player can afford the bet.
        if (!playerData.CanPlaceBet())
        {
            Debug.LogWarning(
                "Insufficient balance to place the bet."
            );

            if (uiManager != null)
            {
                uiManager.SetSpinButtonInteractable(false);
            }

            return;
        }

        // Deduct the bet.
        if (!playerData.PlaceBet())
        {
            Debug.LogWarning(
                "Failed to place the bet."
            );
            return;
        }

        isSpinning = true;

        // Play the spin button press animation.
        if (spinButtonAnimator != null)
        {
            spinButtonAnimator.SetTrigger("Press");
        }

        // Disable Spin button while reels are spinning.
        if (uiManager != null)
        {
            uiManager.SetSpinButtonInteractable(false);
            uiManager.UpdateUI();
        }

        // Start spinning and wait until reels stop.
        reelManager.SpinAllAndWait(OnSpinComplete);
    }


    /// <summary>
    /// Called after all reels have finished spinning.
    /// </summary>
    private void OnSpinComplete()
    {
        // Evaluate the final reel result.
        int winAmount =
            reelManager.EvaluateWin(playerData.Bet);

        // Add winnings to balance.
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

        isSpinning = false;

        // Allow another spin if player can afford it.
        if (uiManager != null)
        {
            uiManager.SetSpinButtonInteractable(
                playerData.CanPlaceBet()
            );
        }

        Debug.Log(
            $"Spin completed. " +
            $"Win Amount: {winAmount}, " +
            $"New Balance: {playerData.Balance}"
        );
    }


    /// <summary>
    /// Updates all UI elements.
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