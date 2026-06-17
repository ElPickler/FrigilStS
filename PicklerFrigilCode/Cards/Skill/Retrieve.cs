using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Retrieve() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new ("Accumulate", 0)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner)
            return;
        if (cardPlay.Card is Cryospear)
        {
            Cryospear cryospear = (cardPlay.Card as Cryospear)!;
            DynamicVars["Accumulate"].BaseValue = cryospear.GetDamage();
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await AccumulateCmd.Accumulate(choiceContext, DynamicVars["Accumulate"].BaseValue, Owner, this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}