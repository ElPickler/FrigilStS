using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class FreeSkate() : PicklerFrigilCard(3,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    //FIXME: make cards not go below 0 cost
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new ("EnergyReduction", 1)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //I am too stupid to know how to do this to all the piles at once, so I iterate through them all manually
        //I could probably just make a big ienumerable but alas, I am lazy
        
        foreach (CardModel c in PileType.Hand.GetPile(Owner).Cards)
        {
            if (!c.EnergyCost.CostsX)
            {
                if (EnergyCost.Canonical < (int)DynamicVars["EnergyReduction"].BaseValue)
                    EnergyCost.SetThisTurn(0);
                else
                    c.EnergyCost.AddThisTurn((int)DynamicVars["EnergyReduction"].BaseValue * -1);
            }
        }
        
        foreach (CardModel c in PileType.Draw.GetPile(Owner).Cards)
        {
            if (!c.EnergyCost.CostsX)
            {
                if (EnergyCost.Canonical < (int)DynamicVars["EnergyReduction"].BaseValue)
                    EnergyCost.SetThisTurn(0);
                else
                    c.EnergyCost.AddThisTurn((int)DynamicVars["EnergyReduction"].BaseValue * -1);
            }
        }
        
        foreach (CardModel c in PileType.Discard.GetPile(Owner).Cards)
        {
            if (!c.EnergyCost.CostsX)
            {
                if (EnergyCost.Canonical < (int)DynamicVars["EnergyReduction"].BaseValue)
                    EnergyCost.SetThisTurn(0);
                else
                    c.EnergyCost.AddThisTurn((int)DynamicVars["EnergyReduction"].BaseValue * -1);
            }
        }

        PowerCmd.Apply<FreeSkatePower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["EnergyReduction"].UpgradeValueBy(1);
    }
}