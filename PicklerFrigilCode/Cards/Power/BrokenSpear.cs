using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Commands;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Power;


public class BrokenSpear() : PicklerFrigilCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromCard<Cryospear>();
            yield return HoverTipFactory.FromCard<ShatteredSpear>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BrokenSpearPower>(2),
        new EnergyVar(1)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<BrokenSpearPower>(choiceContext, Owner.Creature, DynamicVars["BrokenSpearPower"].BaseValue, Owner.Creature, this);

        List<Cryospear> spears = AccumulateCmd.GetCryospears(Owner, false, false).ToList();
        foreach (Cryospear spear in spears)
        {
            spear.EnergyCost.SetCustomBaseCost(1);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["BrokenSpearPower"].UpgradeValueBy(1);
    }
}