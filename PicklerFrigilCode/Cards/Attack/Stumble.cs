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


public class Stumble() : PicklerFrigilCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(0, ValueProp.Move),
        new PowerVar<VulnerablePower>( 1M),
        
        new CalculationBaseVar(0M),
        new ExtraDamageVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => card.Owner.Creature.GetPowerAmount<FlowPower>() * 2)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromPower<FlowPower>();
            yield return HoverTipFactory.FromPower<VulnerablePower>();
        }
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<VulnerablePower>(Owner.Creature, DynamicVars.Vulnerable.BaseValue, Owner.Creature, this);
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        //UpdateDamage();
        await PowerCmd.Remove<FlowPower>(Owner.Creature);
        
        
    }

    protected override void OnUpgrade()
    {
        //DynamicVars["Multiplier"].UpgradeValueBy(1M);
    }
    /*
    public override Task AfterPowerAmountChanged(PowerModel power, Decimal amount, Creature? applier, CardModel? cardSource)
    {
        UpdateDamage();
        return Task.CompletedTask;
    }

    
    public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        UpdateDamage();
        return Task.CompletedTask;
    }

    public override Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
    {
        UpdateDamage();
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        UpdateDamage();
        return Task.CompletedTask;
    }

    public void UpdateDamage()
    {
        decimal flow = Owner.Creature.GetPowerAmount<FlowPower>();
        DynamicVars.Damage.BaseValue = flow;
    }
    */
}