using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("Symbols")]
    [SerializeField]
    private SymbolData[] symbols;

    [Header("Visual")]
    [SerializeField]
    private Image symbolImage;

    [Header("Spin Settings")]
    [SerializeField]
    private float symbolChangeInterval = 0.1f;

    private SymbolData currentSymbol;
    private bool isSpinning;

    public SymbolData CurrentSymbol => currentSymbol;
    public bool IsSpinning => isSpinning;


    private void Start()
    {
        // Show a random symbol when the game starts.
        SelectRandomSymbol();
    }


    /// <summary>
    /// Starts spinning the reel.
    /// </summary>
    public void Spin()
    {
        if (isSpinning)
        {
            return;
        }

        if (symbols == null || symbols.Length == 0)
        {
            Debug.LogWarning($"{name}: No symbols assigned to the reel.");
            return;
        }

        isSpinning = true;

        StartCoroutine(SpinRoutine());
    }


    /// <summary>
    /// Changes the displayed symbol repeatedly while spinning.
    /// </summary>
    private IEnumerator SpinRoutine()
    {
        while (isSpinning)
        {
            SelectRandomSymbol();

            yield return new WaitForSeconds(symbolChangeInterval);
        }
    }


    /// <summary>
    /// Stops the reel and selects the final symbol.
    /// </summary>
    public void Stop()
    {
        if (!isSpinning)
        {
            return;
        }

        isSpinning = false;

        // Select the final result.
        SelectRandomSymbol();
    }


    /// <summary>
    /// Selects a random symbol from the symbol list.
    /// </summary>
    private void SelectRandomSymbol()
    {
        if (symbols == null || symbols.Length == 0)
        {
            Debug.LogWarning($"{name}: No symbols assigned to the reel.");
            return;
        }

        int randomIndex = Random.Range(0, symbols.Length);

        currentSymbol = symbols[randomIndex];

        UpdateVisual();
    }


    /// <summary>
    /// Displays the current symbol's sprite.
    /// </summary>
    private void UpdateVisual()
    {
        if (symbolImage == null)
        {
            Debug.LogWarning($"{name}: Symbol Image is not assigned.");
            return;
        }

        if (currentSymbol == null)
        {
            Debug.LogWarning($"{name}: Current symbol is null.");
            return;
        }

        if (currentSymbol.Sprite == null)
        {
            Debug.LogWarning(
                $"{name}: Symbol '{currentSymbol.DisplayName}' has no Sprite assigned."
            );
            return;
        }

        symbolImage.sprite = currentSymbol.Sprite;
    }
}