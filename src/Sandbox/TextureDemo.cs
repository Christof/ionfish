using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class TextureDemo : Game
    {
        private Keyboard mKeyboard;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mQuadBinding;
        private Texture mTexture;
        private OrbitingCameraCommandBindingManager mOrbitingCameraCommandBindingManager;

        protected override void Initialize()
        {
            mMaterial = new Material("texture.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.6f, 0.6f, 0.6f, 0)));
            mTexture = new Texture(Window.Device, "texture.png");

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

            mQuadBinding.Draw();
        }

        public override void Dispose()
        {
            base.Dispose();
            mTexture.Dispose();
        }       
    }
}
