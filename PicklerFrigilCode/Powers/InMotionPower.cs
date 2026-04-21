using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class InMotionPower: CustomPowerModel
{
    //private decimal prevAmount;
    private decimal applied;
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/inmotionpower.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/inmotionpower.png";

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if(cardPlay.Card.Type is CardType.Skill)
            Flash();
    }
}