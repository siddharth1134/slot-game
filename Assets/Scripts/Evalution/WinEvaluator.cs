using UnityEngine;

public class WinEvaluator : MonoBehaviour
{
    // Reference to the Paytable ScriptableObject
    [SerializeField]
    public Paytable paytable;


public int Evaluate(SymbolData[] results, int bet)
{
    //validate input
    if(results == null || results.Length == 0)
    {
        Debug.LogWarning("Evulator recieves no results.");
        return 0;
    }
    if(paytable == null)
    {
        Debug.LogWarning("Evaluator has no paytable assigned.");
        return 0;
    }
    if(bet <= 0)
    {
        Debug.LogWarning("bet amount greater than zero.");
        return 0;
    }
    //check missing symbols
    foreach(SymbolData symbol in results)
    {
        if(symbol == null)
        {
            Debug.LogWarning("Evaluator recieved a null symbol in results.");
            return 0;
        }
    }
//check for winning combinations
    SymbolData WinningSymbol = results[0];

    foreach(SymbolData symbol in results)
    {
        if(symbol != WinningSymbol)
        {
            return 0;
        }
    }

//all symbols match, get the multiplier from the paytable

int matchCount = results.Length;

int multiplier = paytable.GetMultiplier(
    WinningSymbol, 
    matchCount
    );
//calculate last win

    int winAmount = bet * multiplier;
    return winAmount;
}
}