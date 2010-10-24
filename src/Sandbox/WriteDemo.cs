using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class WriteDemo : Game
    {
        private Keyboard mKeyboard;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mQuadBinding;
        private RenderTargetTexture mTexture;
        private TextRenderTarget mTextRendererTarget;
        private OrbitingCameraCommandBindingManager mOrbitingCameraCommandBindingManager;

        protected override void Initialize()
        {
            mMaterial = new Material("texture.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.6f, 0.6f, 0.6f, 0)));
            
            mTexture = new RenderTargetTexture(Window.Device, 1024, 1024);
            mTextRendererTarget = new TextRenderTarget(mTexture);

            mKeyboard = new Keyboard();

            var stand = new OrbitingStand {Radius = 7, Azimuth = Constants.HALF_PI};
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();

            var inputCommandBinder = new InputCommandBinder(commands, mKeyboard);

            mOrbitingCameraCommandBindingManager = new OrbitingCameraCommandBindingManager(commands, inputCommandBinder, stand, this);
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mOrbitingCameraCommandBindingManager.Update(Frametime);

            RenderQuad();
        }

        void RenderQuad()
        {
            var world = Matrix.Identity;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetTexture(mTexture);

            mTextRendererTarget.Write(string.Format("{0:0000} fps", 1.0 / Frametime));

            mQuadBinding.Draw();
        }

        public override void Dispose()
        {
            base.Dispose();
            mTexture.Dispose();
        }       
    }
}
