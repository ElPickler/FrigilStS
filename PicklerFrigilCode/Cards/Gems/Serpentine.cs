using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Gems;


public class Serpentine() : AbstractGem(-1,
    CardType.Status, CardRarity.Token,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<PoisonPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PoisonPower>( 4M),
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
    }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card,
        bool causedByEthereal)
    {
        if (card == this)
        {
            foreach (Creature enemy in CombatState!.HittableEnemies)
                await PowerCmd.Apply<PoisonPower>(choiceContext, enemy, DynamicVars["PoisonPower"].BaseValue,
                    Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["PoisonPower"].UpgradeValueBy(3);
    }
}