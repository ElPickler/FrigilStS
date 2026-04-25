using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class WinterWindPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    //public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hypothermia_power.png";
    //public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hypothermia_power.png";

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is FlowPower && power.Owner == Owner)
        {
            //Creature enemy = Owner.Player.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
            //await PowerCmd.Apply<HypothermiaPower>(enemy, Amount, Owner, null);
            
            
            await PowerCmd.Apply<HypothermiaPower>(choiceContext, CombatState.HittableEnemies, Amount, Owner, null);
            
        }
    }
}