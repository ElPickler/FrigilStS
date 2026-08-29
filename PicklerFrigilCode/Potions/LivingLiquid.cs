using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Potions;


public class LivingLiquid : PicklerFrigilPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyPlayer;
    
    public override string CustomPackedImagePath => "res://PicklerFrigil/images/potions/living_liquid.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<LivingLiquidPower>(6)
    ];
    
    public override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower <FlowPower>();
        }
    }

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target == null) return;
        await PowerCmd.Apply<LivingLiquidPower>(choiceContext, target, DynamicVars["LivingLiquidPower"].BaseValue,
            Owner.Creature, null);
    }
}