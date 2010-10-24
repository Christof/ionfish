using System.Drawing;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.DirectWrite;
using SlimDX.DXGI;

namespace Graphics.Materials
{
    public class TextRenderTarget
    {
        private readonly RenderTarget mRenderTarget;
        private readonly SolidColorBrush mBrush;
        private TextFormat mTextFormat;

        public TextRenderTarget(RenderTargetTexture renderTargetTexture)
        {
            var surface = renderTargetTexture.AsSurface();
            var factory2D = new SlimDX.Direct2D.Factory(SlimDX.Direct2D.FactoryType.SingleThreaded);

            var renderTargetProperties = new RenderTargetProperties
            {
                PixelFormat = new PixelFormat(Format.Unknown, AlphaMode.Premultiplied),
                Usage = RenderTargetUsage.None,
                HorizontalDpi = 96,
                VerticalDpi = 96,
                MinimumFeatureLevel = FeatureLevel.Direct3D10,
                Type = RenderTargetType.Default
            };

            mRenderTarget = RenderTarget.FromDXGI(factory2D, surface,
                renderTargetProperties);

            mBrush = new SolidColorBrush(mRenderTarget, new Color4(1, 1, 1));

            CreateTextFormat();
        }

        private void CreateTextFormat()
        {
            var factory = new SlimDX.DirectWrite.Factory(SlimDX.DirectWrite.FactoryType.Shared);
            mTextFormat = factory.CreateTextFormat("Consola", FontWeight.Normal,
                SlimDX.DirectWrite.FontStyle.Normal, FontStretch.Normal, 100, "en-us");
            mTextFormat.TextAlignment = TextAlignment.Center;
            mTextFormat.ParagraphAlignment = ParagraphAlignment.Center;
        }

        public void Write(string text)
        {
            mRenderTarget.BeginDraw();

            mRenderTarget.Transform = Matrix3x2.Identity;
            mRenderTarget.Clear(new Color4(0, 0, 0, 0));
            var layoutRectangle = new RectangleF(0, 0, 1024, 1024);
            mRenderTarget.DrawText(text, mTextFormat, layoutRectangle, mBrush);

            mRenderTarget.EndDraw();
        }
      
    }
}