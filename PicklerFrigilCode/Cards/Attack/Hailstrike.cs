using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;

[Pool(typeof(PicklerFrigilCardPool))]
public class Hailstrike() : PicklerFrigilCard(-1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.RandomEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6, ValueProp.Move),
    ];
    protected override HashSet<CardTag> CanonicalTags => [IcyTag, CardTag.Strike];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play) {}

    protected override bool IsPlayable => false;

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(CardKeyword.Unplayable);
            yield return HoverTipFactory.FromKeyword(IcyKeyword); 
        }
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }

    public override async Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw)
    {
        if (card == this)
        {
            Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
            await CommonActions.CardAttack(this, enemy).Execute(choiceContext);
        }
    }
}