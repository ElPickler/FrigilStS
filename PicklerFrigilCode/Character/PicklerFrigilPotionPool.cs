using BaseLib.Abstracts;
using PicklerFrigil.PicklerFrigilCode.Extensions;
using Godot;

namespace PicklerFrigil.PicklerFrigilCode.Character;

public class PicklerFrigilPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => PicklerFrigil.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}