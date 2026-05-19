using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class EverfrozenSpike() : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    private const int Hypothermia = 2;
    
    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (cardSource == null)
            return;
        if (!cardSource.Tags.Contains(PicklerFrigilCard.IcyTag))
            return;
        if (result.WasFullyBlocked)
            return;

        await PowerCmd.Apply<HypothermiaPower>(choiceContext, target, Hypothermia, dealer, null);
    }
}