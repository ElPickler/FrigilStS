using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class Cascade() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move),
        new DynamicVar("Falloff", 4M),
        
    ];
    
    protected override HashSet<CardTag> CanonicalTags => [IcyTag];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(IcyKeyword); 
            yield return HoverTipFactory.FromPower < HypothermiaPower>(); 
        }
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            if (enemy != play.Target)
            {
                await CommonActions.CardAttack(this, enemy, DynamicVars["Falloff"].BaseValue).Execute(choiceContext);
            }
            else
            {
                await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
            }
            
            if (enemy != null) {await PowerCmd.Apply<HypothermiaPower>(enemy, 1, Owner.Creature, this, false);}
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
        DynamicVars["Falloff"].UpgradeValueBy(2m);
    }
}