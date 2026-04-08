using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Commands;


namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;

public class ConjureSpear() : PicklerFrigilCard(1
    ,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Accumulate", 13M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
            yield return HoverTipFactory.FromCard<Cryospear>();
        }
    }
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await AccumulateCmd.Accumulate(DynamicVars["Accumulate"].BaseValue, Owner, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Accumulate"].UpgradeValueBy(5m);
    }
}