using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class GemSlam() : PicklerFrigilCard(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(11M),
        new ExtraDamageVar(2M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, target) => GetGemstones(card)),
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).WithHitFx("vfx/vfx_heavy_blunt", tmpSfx: "heavy_attack.mp3").WithHitVfxSpawnedAtBase().Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
        DynamicVars.CalculationBase.UpgradeValueBy(3);
    }

    private static int GetGemstones(CardModel card)
    {
        IEnumerable<CardModel> list = PileType.Draw.GetPile(card.Owner).Cards.Where(c => c is AbstractGem).ToList();
        list = list.Concat(PileType.Discard.GetPile(card.Owner).Cards.Where(c => c is AbstractGem).ToList());
        list = list.Concat(PileType.Hand.GetPile(card.Owner).Cards.Where(c => c is AbstractGem).ToList());
        
        return list.Count();
    }
}