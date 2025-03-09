using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Proceduralweapons
{
    public static class Assetdirectory
    {
        public static Texture2D[] BarrelTextures;
        public static Texture2D[] BodyTextures;
        public static Texture2D[] MagazineTextures;
        public static Texture2D[] ScopeTextures;
        public static Texture2D[] StockTextures;

        public static int Barrel = 5;
        public static int Body = 3;
        public static int Magazine = 3;
        public static int Scope = 4;
        public static int Stock = 6;
        static Assetdirectory()
        {
            BarrelTextures = new Texture2D[Barrel];
            BodyTextures = new Texture2D[Body];
            MagazineTextures = new Texture2D[Magazine];
            ScopeTextures = new Texture2D[Scope];
            StockTextures = new Texture2D[Stock];

            for (int i = 0; i < Barrel; i++)
            {
                BarrelTextures[i] = ModContent.Request<Texture2D>($"Proceduralweapons/Assets/Textures/Barrels/Barrel{i}", AssetRequestMode.ImmediateLoad).Value;
            }
            for (int i = 0; i < Body; i++)
            {
                BodyTextures[i] = ModContent.Request<Texture2D>($"Proceduralweapons/Assets/Textures/Bodies/Body{i}", AssetRequestMode.ImmediateLoad).Value;
            }
            for (int i = 0; i < Magazine; i++)
            {
                MagazineTextures[i] = ModContent.Request<Texture2D>($"Proceduralweapons/Assets/Textures/Magazines/Magazine{i}", AssetRequestMode.ImmediateLoad).Value;
            }
            for (int i = 0; i < Scope; i++)
            {
                ScopeTextures[i] = ModContent.Request<Texture2D>($"Proceduralweapons/Assets/Textures/Scopes/Scope{i}", AssetRequestMode.ImmediateLoad).Value;
            }
            for (int i = 0; i < Stock; i++)
            {
                StockTextures[i] = ModContent.Request<Texture2D>($"Proceduralweapons/Assets/Textures/Stocks/Stock{i}", AssetRequestMode.ImmediateLoad).Value;
            }
        }
    }
}