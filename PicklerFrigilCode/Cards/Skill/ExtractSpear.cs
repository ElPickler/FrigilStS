using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Commands;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class ExtractSpear() : PicklerFrigilCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<HypothermiaPower>();
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new ("Multiplier", 2)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if(play.Target == null)
            return;
        
        int hypothermia = play.Target.GetPowerAmount<HypothermiaPower>() * (int) DynamicVars["Multiplier"].BaseValue;

        if (hypothermia > 0)
        {
            await PowerCmd.Remove<HypothermiaPower>(play.Target);
            await AccumulateCmd.Accumulate(choiceContext, hypothermia, Owner, this);
        }
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Multiplier"].UpgradeValueBy(1);
    }
}