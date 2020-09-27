using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    float TrustInPreviousPlayer = Random.Range(0, 1);
    float BluffBehaviour = Random.Range(0, 1);
    float SelfConfidence = Random.Range(0, 1);
    int previousBetValue, previousBetHowMany, numberOfDicesInGame, numberOfPlayers, HowManyPerValue;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    bool Turn()
    {
        if(previousBetValue==1) HowManyPerValue = numberOfDicesInGame/ 6;
        else HowManyPerValue = numberOfDicesInGame / 3;

        bool end = EvaluatePreviousBet();
        if (!end) end=Doubt();
        return end;
    }
    bool EvaluatePreviousBet()
    {
        bool wantToBet = TrustPreviousP();
        if (wantToBet) wantToBet = DecideKindOfBet();
        return wantToBet;

    }
    bool TrustPreviousP()
    {
        return TrustInPreviousPlayer * (HowManyPerValue-previousBetHowMany) >.5f;
    }
    bool DecideKindOfBet()
    {
        bool end = Casta();
        if (!end) end = BetOnMyDice();
        return end;
    }
    bool BetOnMyDice()
    {
        bool wantToBet = CanIBet();
        if (wantToBet) Bet();
        return wantToBet;
    }

    bool Casta()
    {
        return false;
    }
    bool CanIBet()
    {
        return false;

    }
    bool Bet()
    {
        return false;

    }
    bool Doubt()
    {
        //doubt
        return false;
    }
}
