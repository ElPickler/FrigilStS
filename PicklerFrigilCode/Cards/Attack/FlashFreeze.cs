using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class FlashFreeze() : PicklerFrigilCard(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(14M, ValueProp.Move)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromPower <HypothermiaPower>(); 
        }
    }
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int hypothermia = play.Target.GetPowerAmount<HypothermiaPower>();
        
        bool shouldTriggerFatal = play.Target.Powers.All(p => p.ShouldOwnerDeathTriggerFatal()); //Do they have any powers that shouldn't trigger fatal (Minion)
        AttackCommand attack = await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        if (!shouldTriggerFatal || !attack.Results.Any(r => r.WasTargetKilled))
            return;
        if (CombatState!.HittableEnemies.Count == 0) //If no one is left alive, do nothing
            return;
        if (CombatState.HittableEnemies.Count == 1) //If only one is left alive, just give it all to them
        {
            await PowerCmd.Apply<HypothermiaPower>(choiceContext, CombatState.HittableEnemies[0], hypothermia, Owner.Creature, this);
            return;
        }

        //If multiple are left alive,
        int hypoDiv = hypothermia / CombatState.HittableEnemies.Count; //How much to give everyone
        int hypoModulo = hypothermia % CombatState.HittableEnemies.Count; //Probably feels bad if some of the Hypothermia is lost, so we give the remainder to the first target
        
        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            if (enemy == CombatState.HittableEnemies[0])
                await PowerCmd.Apply<HypothermiaPower>(choiceContext, enemy, hypoDiv + hypoModulo, Owner.Creature, this);
            else
                await PowerCmd.Apply<HypothermiaPower>(choiceContext, enemy, hypoDiv, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
}