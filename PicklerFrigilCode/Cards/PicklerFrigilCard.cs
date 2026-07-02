using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.Content;
using BaseLib.Utils;
using Godot;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Cards;

  


[Pool(typeof(PicklerFrigilCardPool))]
public abstract class PicklerFrigilCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    [CustomEnum] public static CardTag IcyTag;
    [CustomEnum] public static CardTag GemTag;

    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword IcyKeyword;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword AccumulateKeyword;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword GemstoneKeyword;
    
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    
    //public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string CustomPortraitPath => GetBigPortraitPath();
    
    private string GetBigPortraitPath()
    {
        string path;
        if (FrigilModConfig.BetaCards)
        {
             path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_beta.png".BigCardImagePath();
             if (ResourceLoader.Exists(path))
             {
                 return path;
             }
                 
        }
        return $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    }
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    
    
    
    public override string PortraitPath => GetSmallPortraitPath();
    
    
    private string GetSmallPortraitPath()
    {
        string path;
        if (FrigilModConfig.BetaCards)
        {
            path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_beta.png".CardImagePath();
            if (ResourceLoader.Exists(path))
            {
                return path;
            }
                 
        }
        return $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    }
    
    
    //public override string BetaPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_beta.png".CardImagePath();
}