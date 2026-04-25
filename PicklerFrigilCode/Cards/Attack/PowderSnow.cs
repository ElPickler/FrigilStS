using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class PowderSnow() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(2, ValueProp.Move),
        new ("Repeat", 3M),
        new PowerVar<HypothermiaPower>( 3M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for (int i = 0; i < DynamicVars["Repeat"].BaseValue; i++)
        {
            await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        }
        if (play.Target != null) {await PowerCmd.Apply<HypothermiaPower>(choiceContext, play.Target, DynamicVars["HypothermiaPower"].BaseValue, Owner.Creature, this, false);}
    }

    protected override void OnUpgrade()
    {
        DynamicVars["HypothermiaPower"].UpgradeValueBy(1m);
        DynamicVars["Repeat"].UpgradeValueBy(1m);
    }
}