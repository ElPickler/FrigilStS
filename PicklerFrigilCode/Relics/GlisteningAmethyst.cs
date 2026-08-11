using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Relics;

[Pool(typeof(PicklerFrigilRelicPool))]
public class GlisteningAmethyst : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower <HypothermiaPower>();
        }
    }
    
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;

        Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }

    
    public override decimal ModifyPowerAmountGivenMultiplicative(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource) //One function to multiply the power amount when calculating the hypothermia amount
    {
        FrigilMainFile.Logger.Info("Applying "+ power + " to " + target + " from " + cardSource);

        if (target == null)
            return 1;
        if (Status != RelicStatus.Normal)
            return 1;
        if (giver != Owner.Creature)
            return 1;
        if (power is not HypothermiaPower)
            return 1;

        FrigilMainFile.Logger.Info("Applied "+ amount * 2 + " Hypothermia to " + target + ". Disabling Relic.");
        return 2;
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) //And another to disable the relic
    {
        if (Status != RelicStatus.Normal)
            return;
        if (applier != Owner.Creature)
            return;
        if (power is not HypothermiaPower)
            return;
        
        Status = RelicStatus.Disabled;
    }
}