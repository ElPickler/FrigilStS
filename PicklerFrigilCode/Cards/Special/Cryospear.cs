using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Special;

[Pool(typeof(TokenCardPool))]
public class Cryospear() : PicklerFrigilCard(2,
    CardType.Attack, CardRarity.Token,
    TargetType.AnyEnemy)
{
    private Decimal _currentDamage = 0M;
    
    private Decimal CurrentDamage
    {
        set
        {
            AssertMutable();
            _currentDamage = value;
        }
    }
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain, CardKeyword.Exhaust];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(IcyKeyword); 
            yield return HoverTipFactory.FromPower < HypothermiaPower>(); 
        }
    }
    
    protected override HashSet<CardTag> CanonicalTags => [IcyTag];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(0, ValueProp.Move)];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
    
    public void AddDamage(Decimal amount)
    {
        DamageVar damage = DynamicVars.Damage;
        damage.BaseValue += amount;
        CurrentDamage = DynamicVars.Damage.BaseValue;
    }
}