using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;
using SharpDX.Direct2D1;
using SharpDX.Direct2D1.Effects;
using csproject2024.src;
using Texture = csproject2024.src.Texture;

internal class Foliage
{
    private Texture texture;
    private Vector2 tilePosition;
    private Vector2 drawPosition;
    private Rectangle collisionBox;

    private Color baseColor;
    private Color targetColor;

    private float fadeDuration = 0.5f;
    private float fadeProgress = 0f;
    private bool isFadingToGray = false;
    private bool previousFadeState = false;

    private bool willFade;

    float randomOffset;

    public Foliage(Texture texture, Vector2 tilePosition, bool willFade)
    {
        baseColor = Color.White;
        targetColor = Color.White;

        this.willFade = willFade;

        Random random = new Random();
        randomOffset = SharpDX.RandomUtil.NextFloat(random, 0.0001f, 0.0002f);
        this.texture = texture;
        this.tilePosition = tilePosition;
        this.drawPosition = tilePosition * 16;

        Vector2 topLeft = drawPosition - texture.origin;
        collisionBox = new Rectangle((int)topLeft.X, (int)topLeft.Y, texture.texture.Width, texture.texture.Height-10);
    }

    private float GetLayerHeight(Player player)
    {
        int foliageY = (int)Math.Floor(Vector2.Transform(drawPosition, Globals.camera.Transform).Y);
        int playerY = (int)Math.Floor(Vector2.Transform(player.position, Globals.camera.Transform).Y);

        Vector2 screenPos = Vector2.Transform(drawPosition, Globals.camera.Transform);
        float normalisedY = screenPos.Y / Globals.GraphicsDevice.Viewport.Height;
        float scaledY = normalisedY * 0.1f;
        scaledY = MathHelper.Clamp(scaledY, 0, 0.1f);
        float offset = ((drawPosition.X + drawPosition.Y) % 16) * 0.0001f;
        float layerHeight = 0.130f;

        if (playerY > foliageY)
        {
            layerHeight = 0.270f;
        }

        layerHeight -= scaledY += offset += randomOffset;

        return layerHeight;
    }

    public Color FadeColor(Color startColor, Color endColor, float progress)
    {
        progress = MathHelper.Clamp(progress, 0, 1);

        return Color.Lerp(startColor, endColor, progress);
    }

    public void Draw(Player player)
    {
        if (willFade)
        {
            if (collisionBox.Contains(new Point((int)Math.Round(player.position.X), (int)Math.Round(player.position.Y))))
            {
                isFadingToGray = true;
            }
            else
            {
                isFadingToGray = false;
            }

            if (isFadingToGray != previousFadeState)
            {
                fadeProgress = 0f;
                previousFadeState = isFadingToGray;
                targetColor = isFadingToGray ? new Color(80, 80, 80, 80) : Color.White;
            }

            fadeProgress += Globals.ElapsedSeconds / fadeDuration;
            fadeProgress = MathHelper.Clamp(fadeProgress, 0, 1);

            baseColor = FadeColor(baseColor, targetColor, fadeProgress);
        }
        
        float layerHeight = GetLayerHeight(player);

        Globals.SpriteBatch.Draw(texture.texture, drawPosition, null, baseColor, 0f, texture.origin, Vector2.One, SpriteEffects.None, layerHeight);
    }



}

