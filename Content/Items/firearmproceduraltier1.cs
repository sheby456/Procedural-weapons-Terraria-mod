using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Proceduralweapons.Content.Projectiles;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Terraria.Utilities;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Terraria.GameContent;

namespace Proceduralweapons.Content.Items
{ 
	public class firearmproceduraltier1 : ModItem
	{
		Color colour = Color.White;

        int barrelid = 0;
        int bodyid = 0;
        int magazineid = 0;
        int stockid = 0;
		int scopeid = 0;

        int minimaldamage = 4;
        int deltadamage = 15;

        int minimalusetime = 8;
        int deltausetime = 25;

        int CustomDamage = 10;
        int CustomUseTime = 15;

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 15;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool? UseItem(Player player)
        {
            int projectileIndex = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<projfirearmtier1>(), 0, 0, player.whoAmI);

            if (Main.projectile[projectileIndex].ModProjectile is projfirearmtier1 Gunproj)
            {
                Gunproj.rotation = (Main.MouseWorld - player.Center).ToRotation();
                Gunproj.barrelid = barrelid;
                Gunproj.bodyid = bodyid;
                Gunproj.magazineid = magazineid;
                Gunproj.scopeid = scopeid;
                Gunproj.stockid = stockid;
                Gunproj.colour = colour;
                Gunproj.timeleft = Item.useTime;
            }

            return true;
        }
        public override void SaveData(TagCompound tag)
        {
			tag.Add("color", colour);

            tag.Add("barrelid", barrelid);
            tag.Add("bodyid", bodyid);
            tag.Add("magazineid", magazineid);
            tag.Add("scopeid", scopeid);
            tag.Add("stockid", stockid);

            //tag.Add("damage", Item.damage);
            //tag.Add("useTime", Item.useTime);

            tag.Add("customDamage", CustomDamage);
            tag.Add("customUseTime", CustomUseTime);
        }
        public override void LoadData(TagCompound tag)
        {
            colour = tag.Get<Color>("color");

            barrelid = tag.GetInt("barrelid");
            bodyid = tag.GetInt("bodyid");
            magazineid = tag.GetInt("magazineid");
            scopeid = tag.GetInt("scopeid");
            stockid = tag.GetInt("stockid");

            //Item.damage = tag.GetInt("damage");
            //Item.useTime = tag.GetInt("useTime");

            CustomDamage = tag.GetInt("customDamage");
            CustomUseTime = tag.GetInt("customUseTime");

            //Item.useAnimation = Item.useTime;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(colour.PackedValue);

            writer.Write(barrelid);
            writer.Write(bodyid);
            writer.Write(magazineid);
            writer.Write(scopeid);
            writer.Write(stockid);

            //writer.Write(Item.damage);
            //writer.Write(Item.useTime);

            writer.Write(CustomDamage);
            writer.Write(CustomUseTime);
        }
        public override void NetReceive(BinaryReader reader)
        {
            colour = new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

            barrelid = reader.ReadInt32();
            bodyid = reader.ReadInt32();
            magazineid = reader.ReadInt32();
            scopeid = reader.ReadInt32();
            stockid = reader.ReadInt32();

            //Item.damage = reader.ReadInt32();
            //Item.useTime = reader.ReadInt32();

            CustomDamage = reader.ReadInt32();
            CustomUseTime = reader.ReadInt32();
        }
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            Item.damage = CustomDamage;
            Item.useTime = CustomUseTime;
            Item.useAnimation = Item.useTime;
        }
        public override void OnCreated(ItemCreationContext context)
        {
			Random random = new Random();

			colour = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

            barrelid = random.Next(Assetdirectory.Barrel);
            bodyid = random.Next(Assetdirectory.Body);
            magazineid = random.Next(Assetdirectory.Magazine);
            scopeid = random.Next(Assetdirectory.Scope);
            stockid = random.Next(Assetdirectory.Stock);

            //Item.damage = random.Next(deltadamage) + minimaldamage;
            //Item.useTime = random.Next(deltausetime) + minimalusetime;

            CustomDamage = random.Next(deltadamage) + minimaldamage;
            CustomUseTime = random.Next(deltausetime) + minimalusetime;

            Item.damage = CustomDamage;
            Item.useTime = CustomUseTime;

            Item.useAnimation = Item.useTime;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 6);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.Glass, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D barrel = Assetdirectory.BarrelTextures[barrelid];
            Texture2D body = Assetdirectory.BodyTextures[bodyid];
            Texture2D magazine = Assetdirectory.MagazineTextures[magazineid];
            Texture2D scope = Assetdirectory.ScopeTextures[scopeid];
            Texture2D stock = Assetdirectory.StockTextures[stockid];

            Color color = new Color(lightColor.ToVector3() * colour.ToVector3());

            spriteBatch.Draw(stock, Item.position - Main.screenPosition, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(body, Item.position + Vector2.UnitX * stock.Width * scale - Main.screenPosition, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(barrel, Item.position + Vector2.UnitX * (stock.Width + body.Width) * scale - Main.screenPosition, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(scope, Item.position - (Vector2.UnitY * scope.Height - Vector2.UnitX * (stock.Width + body.Width / 4)) * scale - Main.screenPosition, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(magazine, Item.position + (Vector2.UnitX * (stock.Width + body.Width) + Vector2.UnitY * body.Height - new Vector2(10f, 10f)) * scale - Main.screenPosition, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);

            Item.width = body.Width;
			Item.height = body.Height;

			return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D barrel = Assetdirectory.BarrelTextures[barrelid];
            Texture2D body = Assetdirectory.BodyTextures[bodyid];
            Texture2D magazine = Assetdirectory.MagazineTextures[magazineid];
            Texture2D scope = Assetdirectory.ScopeTextures[scopeid];
            Texture2D stock = Assetdirectory.StockTextures[stockid];

            position -= new Vector2(15f, 5f);

            Color color = new Color(colour.ToVector3());
            spriteBatch.Draw(stock, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(body, position + (Vector2.UnitX * stock.Width) * scale, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(barrel, position + (Vector2.UnitX * (stock.Width + body.Width)) * scale, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(scope, position - (Vector2.UnitY * scope.Height - Vector2.UnitX * (stock.Width + body.Width / 4)) * scale, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(magazine, position + (Vector2.UnitX * (stock.Width + body.Width) + Vector2.UnitY * body.Height - new Vector2(10f, 10f)) * scale, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);

            return false;
        }
    }
}
