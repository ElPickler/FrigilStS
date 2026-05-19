using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class FrostLilyPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-frost_lily_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-frost_lily_power.png";
    

    public override async Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, Creature? applier,
        CardModel? cardSource)
    {
        if (target == Owner && cardSource != null)
        { 
            if (power is HypothermiaPower) 
            {
                foreach (Creature enemy in CombatState.HittableEnemies) { 
                    if(enemy != Owner) 
                        await PowerCmd.Apply<HypothermiaPower>(new ThrowingPlayerChoiceContext(), enemy, amount, null, null);
                }
            } 
        }
    }
}