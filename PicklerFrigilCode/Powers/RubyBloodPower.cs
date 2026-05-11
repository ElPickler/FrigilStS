using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class RubyBloodPower : PicklerFrigilPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;

    private const int CardDraw = 2;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new ("UnplayableDrawsLeft", 1)
    ];
    
    public override int DisplayAmount => DynamicVars["UnplayableDrawsLeft"].IntValue;
    
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/ruby_blood.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/ruby_blood.png";
    

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player)
            return;
        DynamicVars["UnplayableDrawsLeft"].BaseValue = 1;
        InvokeDisplayAmountChanged();
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Keywords.Contains(CardKeyword.Unplayable))
        {
            if(DynamicVars["UnplayableDrawsLeft"].BaseValue <= 0)
                return;
            --DynamicVars["UnplayableDrawsLeft"].BaseValue;
            await CardPileCmd.Draw(choiceContext, CardDraw, Owner.Player!);
            InvokeDisplayAmountChanged();
        }
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power,
        decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is RubyBloodPower)
        {
            DynamicVars["UnplayableDrawsLeft"].BaseValue += amount;
            Flash();
            InvokeDisplayAmountChanged();
        }
    }
}