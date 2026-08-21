using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Gems;


[Pool(typeof(TokenCardPool))]
public class Emerald() : AbstractGem(-1,
    CardType.Status, CardRarity.Status,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower <MetabolizingEmeraldPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(3),
        new PowerVar<MetabolizingEmeraldPower>(2)
    ];

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play){ }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card == this)
        {
            if(!causedByEthereal)
                await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            await PowerCmd.Apply<MetabolizingEmeraldPower>(choiceContext, Owner.Creature, DynamicVars["MetabolizingEmeraldPower"].BaseValue, Owner.Creature, this);
        }
    }
}