using BaseLib.Abstracts;
using BaseLib.Utils;
using PicklerFrigil.PicklerFrigilCode.Character;

namespace PicklerFrigil.PicklerFrigilCode.Potions;

[Pool(typeof(PicklerFrigilPotionPool))]
public abstract class PicklerFrigilPotion : CustomPotionModel;