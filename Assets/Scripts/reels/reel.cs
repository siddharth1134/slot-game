using UnityEngine;

public class Reel : MonoBehaviour
{
    // The symbols that can appear on this reel
    [SerializeField]
    private SymbolData[] symbols;

    // The current symbol displayed on the reel
    private SymbolData currentSymbol;

    // Whether this reel is currently spinning
    private bool isSpinning;

    // Public access to the current symbol
    public SymbolData CurrentSymbol => currentSymbol;

    // Public access to the spinning state
    public bool IsSpinning => isSpinning;

    /// <summary>
    /// Spins the reel and selects a random symbol.
    /// </summary>
    public void Spin()
    {
        if (symbols == null || symbols.Length == 0)
        {
            Debug.LogWarning("No symbols assigned to the reel.");
            return;
        }

        isSpinning = true;

        int randomIndex = Random.Range(0, symbols.Length);
        currentSymbol = symbols[randomIndex];

        isSpinning = false;
    }

    /// <summary>
    /// Stops the reel.
    /// </summary>
    public void Stop()
    {
        isSpinning = false;
    }
}