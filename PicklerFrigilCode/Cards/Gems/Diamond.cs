using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Gems;


[Pool(typeof(TokenCardPool))]
public class Diamond() : AbstractGem(-1,
    CardType.Status, CardRarity.Token,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower <MetabolizingDiamondPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<MetabolizingDiamondPower>(16)
    ];

    protected override void OnUpgrade()
    {
        DynamicVars["MetabolizingDiamondPower"].UpgradeValueBy(8);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play){ }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card == this)
        {
            await PowerCmd.Apply<MetabolizingDiamondPower>(choiceContext, Owner.Creature, DynamicVars["MetabolizingDiamondPower"].BaseValue, Owner.Creature, this);
        }
    }
}