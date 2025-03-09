using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using static System.Formats.Asn1.AsnWriter;

namespace Proceduralweapons.Content.Projectiles
{
    public class projfirearmtier1 : ModProjectile
    {
        private int playerId;

        public float rotation;

        public int bodyid = 0;
        public int barrelid = 0;
        public int stockid = 0;
        public int scopeid = 0;
        public int magazineid = 0;

        public Color colour;

        public int timeleft = 20;
        public bool timeleftset = false;
        public override void SetDefaults()
        {
            Projectile.height = 16;
            Projectile.width = 16;
            Projectile.tileCollide = false;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.knockBack = 0;
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = timeleft;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_ItemUse itemSource)
            {
                playerId = itemSource.Player.whoAmI;
            }
        }
        public override void AI()
        {
            if (playerId >= 0 && playerId < Main.maxPlayers && Main.player[playerId].active)
            {
                Player player = Main.player[playerId];
                Projectile.position = player.Center;
            }
            else
            {
                Projectile.Kill();
            }
            if (!timeleftset)
            {
                timeleftset = true;
                Projectile.timeLeft = timeleft + 1; //add 1 so the gun doesn't blink
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;

            Texture2D barrel = Assetdirectory.BarrelTextures[barrelid];
            Texture2D body = Assetdirectory.BodyTextures[bodyid];
            Texture2D magazine = Assetdirectory.MagazineTextures[magazineid];
            Texture2D scope = Assetdirectory.ScopeTextures[scopeid];
            Texture2D stock = Assetdirectory.StockTextures[stockid];

            Color color = new Color(lightColor.ToVector3() * colour.ToVector3());

            Main.EntitySpriteDraw(stock, Projectile.position - Main.screenPosition, null, color, rotation, Vector2.Zero, Projectile.scale, spriteEffects);
            Main.EntitySpriteDraw(body, Projectile.position + (Vector2.UnitX * stock.Width * Projectile.scale).RotatedBy(rotation) - Main.screenPosition, null, color, rotation, Vector2.Zero, Projectile.scale, spriteEffects);
            Main.EntitySpriteDraw(barrel, Projectile.position + (Vector2.UnitX * (stock.Width + body.Width) * Projectile.scale).RotatedBy(rotation) - Main.screenPosition, null, color, rotation, Vector2.Zero, Projectile.scale, spriteEffects);
            Main.EntitySpriteDraw(scope, Projectile.position - ((Vector2.UnitY * scope.Height - Vector2.UnitX * (stock.Width + body.Width / 4)) * Projectile.scale).RotatedBy(rotation) - Main.screenPosition, null, color, rotation, Vector2.Zero, Projectile.scale, spriteEffects);
            Main.EntitySpriteDraw(magazine, Projectile.position + ((Vector2.UnitX * (stock.Width + body.Width) + Vector2.UnitY * body.Height - new Vector2(10f, 10f)) * Projectile.scale).RotatedBy(rotation) - Main.screenPosition, null, color, rotation, Vector2.Zero, Projectile.scale, spriteEffects);
            
            return false;
        }
    }
}
