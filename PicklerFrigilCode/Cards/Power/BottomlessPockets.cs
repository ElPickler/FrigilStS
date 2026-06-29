using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Power;



public class BottomlessPockets() : PicklerFrigilCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(GemstoneKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BottomlessPocketsPower>(1)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (IsUpgraded)
        {
            await PowerCmd.Apply<BottomlessPocketsPowerPlus>(choiceContext, Owner.Creature, DynamicVars["BottomlessPocketsPower"].BaseValue, Owner.Creature, this);
            return;
        }
        await PowerCmd.Apply<BottomlessPocketsPower>(choiceContext, Owner.Creature, DynamicVars["BottomlessPocketsPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        //Handled in OnPlay
    }
}