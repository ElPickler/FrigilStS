using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class SpinParry() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Accumulate", 7M),
        new BlockVar(6, ValueProp.Move)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await AccumulateCmd.Accumulate(choiceContext, DynamicVars["Accumulate"].BaseValue, Owner, this);
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Accumulate"].UpgradeValueBy(3);
        DynamicVars.Block.UpgradeValueBy(2);
    }
}