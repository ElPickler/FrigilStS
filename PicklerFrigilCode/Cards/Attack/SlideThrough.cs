using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class SlideThrough() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)

{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromPower<VulnerablePower>();
            yield return HoverTipFactory.FromPower<FlowPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(6, ValueProp.Move),
        new PowerVar<FlowPower>(2M),
        new PowerVar<VulnerablePower>(1)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
    }

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (cardSource != this)
            return;
        
        if (result.WasFullyBlocked)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, dealer!, DynamicVars["VulnerablePower"].BaseValue, dealer, this);
            return;
        }
        
        await PowerCmd.Apply<FlowPower>(choiceContext, dealer!, DynamicVars["FlowPower"].BaseValue, dealer, this);
        await PowerCmd.Apply<VulnerablePower>(choiceContext, target,  DynamicVars["VulnerablePower"].BaseValue, dealer, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
        DynamicVars["FlowPower"].UpgradeValueBy(1m);
    }
}