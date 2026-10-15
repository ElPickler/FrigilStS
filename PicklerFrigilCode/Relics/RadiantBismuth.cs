using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class RadiantBismuth : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    private int _timer = 0;
    public override bool ShowCounter => _visibleCounter;
    public override int DisplayAmount => _timer;

    private bool _visibleCounter = false;
    
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;

        ActivateRelic();
        return Task.CompletedTask;
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<HypothermiaPower>();
        }
    }

    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side == CombatSide.Enemy)
            return Task.CompletedTask;
        
        if(_timer > 0)
            --_timer;
        
        if (_timer == 0)
        {
            ActivateRelic();
        }
        else
        {
            InvokeDisplayAmountChanged();
        }
        
        return Task.CompletedTask;
    }

    public override decimal ModifyPowerAmountGivenMultiplicative(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource) 
    {
        if (Status == RelicStatus.Normal)
            return 1;
        if (target == null)
            return 1;
        if (giver != Owner.Creature)
            return 1;
        if (power is not HypothermiaPower)
            return 1;
        
        return 2;
    }
    
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) //And another to disable the relic
    {
        if (Status == RelicStatus.Normal)
            return;
        if (applier != Owner.Creature)
            return;
        if (power is not HypothermiaPower)
            return;
        
        Flash();
        DeactivateRelic();
    }

    private void ActivateRelic()
    {
        Status = RelicStatus.Active;
        _visibleCounter = false;
        InvokeDisplayAmountChanged();
    }
    
    private void DeactivateRelic()
    {
        Status = RelicStatus.Normal;
        _timer = 2;
        _visibleCounter = true;
        InvokeDisplayAmountChanged();
    }
}