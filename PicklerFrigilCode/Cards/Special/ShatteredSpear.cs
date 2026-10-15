using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PicklerFrigil.PicklerFrigilCode.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Special;


public class ShatteredSpear : Cryospear
{
    protected override int CanonicalEnergyCost => 0;

    public void setDamage(decimal damage)
    {
        DynamicVars.Damage.BaseValue = damage;
    }
}