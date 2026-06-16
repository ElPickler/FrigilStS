using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class IceSkates() : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    private int _cardCounter = 0;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FlowPower>(2M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<FlowPower>();
        }
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if(card.Owner != Owner)
            return;
        if (fromHandDraw)
            return;

        _cardCounter++;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        while (_cardCounter > 0)
        {
            Flash();
            await PowerCmd.Apply<FlowPower>(choiceContext, Owner.Creature, DynamicVars["FlowPower"].BaseValue, Owner.Creature, null);
            _cardCounter--;
        }
    }
}