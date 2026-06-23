using System.Runtime.InteropServices;
using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using BaseLib.Utils.NodeFactories;
using PicklerFrigil.PicklerFrigilCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.Cards.Basic;
using PicklerFrigil.PicklerFrigilCode.Cards.Basic;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Character;


public class PicklerFrigil : CustomCharacterModel
{
    
    public const string CharacterId = "PicklerFrigil";
    public virtual string PlaceholderID => "ironclad";

    //Colors
    public static readonly Color Color = new("a7e7eb");
    public override Color NameColor => Color;
    public override Color EnergyLabelOutlineColor => new("380d4d");
    public override Color MapDrawingColor => new ("482675");
    
    public override CharacterGender Gender => CharacterGender.Masculine;

    //Run Start Properties
    public override int StartingHp => 72;
    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeFrigil>(), 
        ModelDb.Card<StrikeFrigil>(),
        ModelDb.Card<StrikeFrigil>(),
        ModelDb.Card<StrikeFrigil>(),
        ModelDb.Card<IcicleKick>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DefendFrigil>(),
        ModelDb.Card<DeepChill>()
    ];
    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<GlisteningAmethyst>()
    ];
    
    //Animation Delays
    public override float AttackAnimDelay => 0.2f;
    public override float CastAnimDelay => 0.35f;

    //Pools
    public override CardPoolModel CardPool => ModelDb.CardPool<PicklerFrigilCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<PicklerFrigilRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<PicklerFrigilPotionPool>();
        
    //Textures
        //Generic
    public override string CustomIconTexturePath => "res://PicklerFrigil/images/charui/character_icon_frigil2.png";
    public override string CustomCharacterSelectIconPath => "char_select_frigil.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_frigil.png".CharacterUiPath();

        //Chest Screen
    public override string CustomArmPointingTexturePath => "res://PicklerFrigil/images/character/ui/hand_point.png";
    public override string CustomArmRockTexturePath => "res://PicklerFrigil/images/character/ui/hand_rock.png";
    public override string CustomArmPaperTexturePath => "res://PicklerFrigil/images/character/ui/hand_paper.png";
    public override string CustomArmScissorsTexturePath => "res://PicklerFrigil/images/character/ui/hand_scissors.png";
    public override RelicIconData CustomYummyCookie => new (
        "C:/Users/aleja/RiderProjects/PicklerFrigil/PicklerFrigil/images/relics/big/yummy_cookie_frigil.png",
        "C:/Users/aleja/RiderProjects/PicklerFrigil/PicklerFrigil/images/relics/yummy_cookie_frigil.png",
        "C:/Users/aleja/RiderProjects/PicklerFrigil/PicklerFrigil/images/relics/yummy_cookie_frigil_outline.png"
    );
    
    //Scenes
    public override string CustomVisualPath => "res://PicklerFrigil/scenes/frigil.tscn";
    public override string CustomCharacterSelectBg => "res://PicklerFrigil/scenes/CharSelect/char_select_bg_frigil.tscn";
    public override string CustomCharacterSelectTransitionPath => "res://PicklerFrigil/materials/frigil_transition_mat.tres";
    public override string CustomEnergyCounterPath => "res://PicklerFrigil/scenes/ui/FrigilEnergyCounter.tscn";
    public override string CustomRestSiteAnimPath => "res://PicklerFrigil/scenes/RestSite/frigilRestSite2.tscn";
    public override string CustomMerchantAnimPath => "res://PicklerFrigil/scenes/Merchant/frigilmerchant.tscn";
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }
    
        //PLACEHOLDER
    public override string CustomTrailPath
    {
        get => SceneHelper.GetScenePath("vfx/card_trail_" + this.PlaceholderID);
    }
    
    
    //Audio
    public override string CharacterSelectSfx => "res://PicklerFrigil/audio/FrigilCharSelect.ogg";
    public override string CharacterTransitionSfx => "res://PicklerFrigil/audio/FrigilTransition.ogg";
    
        //PLACEHOLDER
    public override string CustomAttackSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_attack";
    }
    public override string CustomCastSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_cast";
    }
    
    public override string CustomDeathSfx
    {
        get => $"event:/sfx/characters/{this.PlaceholderID}/{this.PlaceholderID}_die";
    }

    //Special

    public override List<string> GetArchitectAttackVfx()
    {
        int num = 5;
        List<string> list = new List<string>(num);
        CollectionsMarshal.SetCount<string>(list, num);
        Span<string> span = CollectionsMarshal.AsSpan<string>(list);
        int index1 = 0;
        span[index1] = "vfx/vfx_attack_slash";
        int index2 = index1 + 1;
        span[index2] = "vfx/vfx_heavy_blunt";
        int index3 = index2 + 1;
        span[index3] = "vfx/vfx_attack_slash";
        int index4 = index3 + 1;
        span[index4] = "vfx/vfx_bloody_impact";
        int index5 = index4 + 1;
        span[index5] = "vfx/vfx_big_slash_impact";
        return list;
    }


    
    public override string CustomIconPath
    {
        get => SceneHelper.GetScenePath($"ui/character_icons/ironclad_icon");
    }
    
}