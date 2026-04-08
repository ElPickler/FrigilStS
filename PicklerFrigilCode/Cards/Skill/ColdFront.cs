using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;

public class ColdFront() : PicklerFrigilCard(-1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower < HypothermiaPower>();
        }
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<HypothermiaPower>( 2M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    { 
        int count = ResolveEnergyXValue();
        for (int i = 0; i < count; i++)
        {
            foreach (Creature enemy in CombatState.HittableEnemies)
            {
                await PowerCmd.Apply<HypothermiaPower>(enemy, DynamicVars["HypothermiaPower"].BaseValue , Owner.Creature, this);
            }
        }
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars["HypothermiaPower"].UpgradeValueBy(1);
    }
}