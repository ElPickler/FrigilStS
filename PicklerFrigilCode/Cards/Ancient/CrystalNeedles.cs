using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Ancient;

public class CrystalNeedles() : PicklerFrigilCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(2, ValueProp.Move),
        new DynamicVar("Hypothermia", 1M),
        new DynamicVar("RepeatBase", 2M),
        new DynamicVar("RepeatMod", 0M),
        new DynamicVar("RepeatTotal", 2M) //FIXME: FIX THE DESCRIPTION FOR THISO SO THAT IT FUCKING SAYS WHAT IT DEOS ON UPGRADE
    ];

    
    protected override HashSet<CardTag> CanonicalTags => [IcyTag];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(IcyKeyword); 
            yield return HoverTipFactory.FromPower < HypothermiaPower>(); 
        }
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        for (int i = 0; i < DynamicVars["RepeatTotal"].BaseValue; i++)
        {
            await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
            if (play.Target != null)
            {
                await PowerCmd.Apply<HypothermiaPower>(play.Target, 1, this.Owner.Creature, this, false);
            }
        }
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars["RepeatBase"].UpgradeValueBy(1m);
    }
    
    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        DynamicVars["RepeatTotal"].BaseValue = DynamicVars["RepeatBase"].BaseValue + DynamicVars["RepeatMod"].BaseValue;
    }
    
    public override Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw)
    {
        if (card != this)
            return Task.CompletedTask;
        DynamicVars["RepeatMod"].BaseValue += 1;
        DynamicVars["RepeatTotal"].BaseValue = DynamicVars["RepeatBase"].BaseValue + DynamicVars["RepeatMod"].BaseValue;
        
        return Task.CompletedTask;
    }
}