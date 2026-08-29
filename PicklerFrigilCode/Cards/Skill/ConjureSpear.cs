using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.GameInfo.Objects;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Commands;


namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;

public class ConjureSpear() : PicklerFrigilCard(1
    ,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Accumulate", 14M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
            yield return HoverTipFactory.FromCard<Cryospear>();
        }
    }
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await AccumulateCmd.Accumulate(choiceContext, DynamicVars["Accumulate"].BaseValue, Owner, this);

        if (Enchantment == null)
            return;
        
        List<Cryospear> spears = AccumulateCmd.GetCryospears(Owner, false, true).ToList();
        foreach (Cryospear spear in spears)
        {
            if (spear.Enchantment != null) continue;
            EnchantmentModel enchantment = Enchantment.CanonicalInstance.ToMutable();
            CardCmd.Enchant(enchantment, spear, Enchantment.Amount);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Accumulate"].UpgradeValueBy(4m);
    }
}