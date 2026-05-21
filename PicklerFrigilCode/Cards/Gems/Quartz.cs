using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Commands;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Gems;


public class Quartz() : AbstractGem(-1,
    CardType.Status, CardRarity.Token,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
        }
    }
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Accumulate", 10),
        new PowerVar<MetabolizingQuartzPower>(4)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    { }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card == this)
        {
            await AccumulateCmd.Accumulate(DynamicVars["Accumulate"].BaseValue, card.Owner, this);
            await PowerCmd.Apply<MetabolizingQuartzPower>(choiceContext, Owner.Creature, DynamicVars["MetabolizingQuartzPower"].BaseValue, Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars["Accumulate"].UpgradeValueBy(6);
        DynamicVars["MetabolizingQuartzPower"].UpgradeValueBy(2);
    }
}