// This class is used to store the player's data, such as balance and bet amount.
public class PlayerData
{
    public int Balance{get; private set;}
    public int Bet {get; private set;}
// Constructor to initialize the player's data with starting balance and bet amount.
    public PlayerData(int startingBalance, int startingBet)
    {
        Balance = startingBalance;
        Bet = startingBet;
    }
     public bool CanPlaceBet()
    {
        return Balance >= Bet;
    }
    public bool Placebet()
    {
        if(!CanPlaceBet())
        {
            return false;
        }
        Balance -= Bet;
        return true;
    }
    public void AddWinnings(int amount)
    {
        Balance += amount;
    }

}
