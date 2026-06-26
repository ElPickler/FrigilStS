using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class IceAge() : PicklerFrigilCard(-1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AllEnemies)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<HypothermiaPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<HypothermiaPower>( 4M)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Ethereal,
        CardKeyword.Unplayable
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    { }

    protected override void OnUpgrade()
    {
        DynamicVars["HypothermiaPower"].UpgradeValueBy(2);
    }
    
    public override async Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw)
    {
        if (card == this)
        {
            await PowerCmd.Apply<HypothermiaPower>(choiceContext, CombatState!.HittableEnemies, DynamicVars["HypothermiaPower"].BaseValue, Owner.Creature, this);
        }
    }
}