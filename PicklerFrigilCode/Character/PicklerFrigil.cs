using System.Runtime.InteropServices;
using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using PicklerFrigil.PicklerFrigilCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.Cards;
using PicklerFrigil.Cards.Basic;
using PicklerFrigil.PicklerFrigilCode.Cards.Basic;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Character;


public class PicklerFrigil : PlaceholderCharacterModel
{
    public const string CharacterId = "PicklerFrigil";

    public static readonly Color Color = new("a7e7eb");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeFrigil>(), 
        ModelDb.Card<StrikeFrigil>(),
        ModelDb.Card<StrikeFrigil>(),
        ModelDb.Card<StrikeFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<IcicleKick>(),
        ModelDb.Card<DeepChill>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<GlisteningAmethyst>()
    ];

    public override float AttackAnimDelay => 0.2f;
    public override float CastAnimDelay => 0.35f;

    public override CardPoolModel CardPool => ModelDb.CardPool<PicklerFrigilCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<PicklerFrigilRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<PicklerFrigilPotionPool>();
        
    public override Color MapDrawingColor => new ("482675");
    
    //Already replaced
    
    //public override string CustomIconPath => "res://PicklerFrigil/scenes/ui/character_icon_frigil.tscn"; Not sure why this is breaking
    public override string CustomIconTexturePath => "res://PicklerFrigil/images/charui/character_icon_frigil2.png";
    public override string CustomCharacterSelectIconPath => "char_select_frigil.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_frigil.png".CharacterUiPath();

    public override string CustomArmPointingTexturePath => "res://PicklerFrigil/images/character/ui/hand_point.png";
    public override string CustomArmRockTexturePath => "res://PicklerFrigil/images/character/ui/hand_rock.png";
    public override string CustomArmPaperTexturePath => "res://PicklerFrigil/images/character/ui/hand_paper.png";
    public override string CustomArmScissorsTexturePath => "res://PicklerFrigil/images/character/ui/hand_scissors.png";
    
    public override string CustomVisualPath => "res://PicklerFrigil/scenes/frigil.tscn";
    public override string CustomCharacterSelectBg => "res://PicklerFrigil/scenes/char_select_bg_frigil.tscn";
    
    
    public override string CustomCharacterSelectTransitionPath => "res://PicklerFrigil/materials/frigil_transition_mat.tres";
    
    
    //public override string CustomRestSiteAnimPath => "res://PicklerFrigil/scenes/frigil_rest_site.tscn"; FUUUCK this

    
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }
    
    
    
    //


    
    public override string CustomMerchantAnimPath => "res://PicklerFrigil/scenes/frigilmerchant.tscn";
    
    
    //To be replaced
    public virtual string PlaceholderID => "ironclad";
    
    public override string CustomTrailPath
    {
        get => SceneHelper.GetScenePath("vfx/card_trail_" + this.PlaceholderID);
    }
    
    public override string CustomEnergyCounterPath
    {
        get => SceneHelper.GetScenePath($"combat/energy_counters/{this.PlaceholderID}_energy_counter");
    }
    
    public override string CustomRestSiteAnimPath
    {
        get => SceneHelper.GetScenePath($"rest_site/characters/{this.PlaceholderID}_rest_site");
    }
    
    
    
    
    
    public override string CustomIconPath
    {
        get => SceneHelper.GetScenePath($"ui/character_icons/{this.PlaceholderID}_icon");
    }
    
    public override string CharacterSelectSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_select";
    }
    
    public override string CustomCastSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_cast";
    }
    
    public override string CustomDeathSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_die";
    }
    
    public override string CustomAttackSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_attack";
    }
    
    public override List<string> GetArchitectAttackVfx()
    {
        int num = 5;
        List<string> list = new List<string>(num);
        CollectionsMarshal.SetCount<string>(list, num);
        Span<string> span = CollectionsMarshal.AsSpan<string>(list);
        int index1 = 0;
        span[index1] = "vfx/vfx_attack_blunt";
        int index2 = index1 + 1;
        span[index2] = "vfx/vfx_heavy_blunt";
        int index3 = index2 + 1;
        span[index3] = "vfx/vfx_attack_slash";
        int index4 = index3 + 1;
        span[index4] = "vfx/vfx_bloody_impact";
        int index5 = index4 + 1;
        span[index5] = "vfx/vfx_rock_shatter";
        return list;
    }
}