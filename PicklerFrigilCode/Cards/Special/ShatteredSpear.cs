using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PicklerFrigil.PicklerFrigilCode.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Special;


public class ShatteredSpear : Cryospear
{
    public void setDamage(decimal damage)
    {
        DynamicVars.Damage.BaseValue = damage;
    }
}