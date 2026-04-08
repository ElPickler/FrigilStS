using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class Icebreaker() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Repeat", 2M),
        new CalculationBaseVar(0M),
        new ExtraDamageVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((Func<CardModel, Creature, Decimal>) ((_, target) => target != null ? target.GetPowerAmount<HypothermiaPower>() : 0))
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for (int i = 0; i < DynamicVars["Repeat"].BaseValue; i++)
        {
            await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
            
        }
        if (play.Target != null) await PowerCmd.Remove<HypothermiaPower>(play.Target);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Repeat"].UpgradeValueBy(1);
    }
}