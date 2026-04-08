using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Character;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Basic;


public class QuickSwipe() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(7, ValueProp.Move),
        new CardsVar(1),
        new DynamicVar("Repeat", 1M)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        for (int i = 0; i < DynamicVars["Repeat"].BaseValue; i++)
        {
            await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        }
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1M);
        DynamicVars.Cards.UpgradeValueBy(1M);
    }
}