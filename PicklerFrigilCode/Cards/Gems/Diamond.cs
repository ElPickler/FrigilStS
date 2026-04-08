using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Gems;


[Pool(typeof(TokenCardPool))]
public class Diamond() : AbstractGem(-1,
    CardType.Status, CardRarity.Token,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(10, ValueProp.Move)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];

    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(4);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play){ }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card == this)
        {
            await CommonActions.CardBlock(this, null);
        }
    }
}