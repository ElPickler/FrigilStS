using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class LetItGo() : PicklerFrigilCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
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
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, 99);
        List<CardModel> list = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).ToList();

        foreach (CardModel c in list)
        {
            await CardCmd.Exhaust(choiceContext, c);
            
            Creature? enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState!.HittableEnemies);
            if(enemy != null) await PowerCmd.Apply<HypothermiaPower>(choiceContext, enemy, DynamicVars["HypothermiaPower"].BaseValue , Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["HypothermiaPower"].UpgradeValueBy(2);
    }
}