using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class LightingDemo : Game
    {
        private Keyboard mKeyboard;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mSphereBinding;
        private MeshMaterialBinding mQuadBinding;
        MeshMaterialBinding mCube;
        private OrbitingCameraCommandManager mOrbitingCameraCommandManager;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";

        protected override void Initialize()
        {
            var sphereMesh = new Sphere(Window.Device);

            mMaterial = new Material("lighting.fx", Window.Device);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mMaterial, sphereMesh);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.6f, 0.6f, 0.6f, 0)));
            mCube = new MeshMaterialBinding(Window.Device, mMaterial, new CubeWithNormals(Window.Device));

            mKeyboard = new Keyboard();

            var stand = new OrbitingStand { Radius = 7 };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);

            var inputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            inputCommandBinder.Bind(Button.Escape, ESCAPE);
            inputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);
            mOrbitingCameraCommandManager = new OrbitingCameraCommandManager(commands, inputCommandBinder, stand);
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mOrbitingCameraCommandManager.Update(Frametime);

            //RenderSphere();
            RenderCube();

            RenderGround();
            RenderBackwall();
            RenderLeftwall();
        }

        void RenderGround()
        {
            var rotation = Matrix.RotateX(-Constants.HALF_PI);
            var world = Matrix.CreateTranslation(new Vector3(0f, -1f, 0f)) * Matrix.Scale(5) * rotation;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotation);

            mQuadBinding.Draw();
        }

        void RenderBackwall()
        {
            var world = Matrix.CreateTranslation(new Vector3(0f, 1.5f, -2.5f)) * Matrix.Scale(5);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(Matrix.Identity);

            mQuadBinding.Draw();
        }

        void RenderLeftwall()
        {
            var rotation = Matrix.RotateY(Constants.HALF_PI);
            var world = Matrix.CreateTranslation(new Vector3(-2.5f, 1.5f, 0)) * Matrix.Scale(5) * rotation;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotation);

            mQuadBinding.Draw();
        }

        void RenderSphere()
        {
            var rotateX = Matrix.RotateX(Gametime);
            var world = rotateX;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotateX);

            mSphereBinding.Draw();
        }

        void RenderCube()
        {
            var rotateX = Matrix.RotateX(Gametime) * Matrix.RotateZ(Gametime) * Matrix.RotateY(Gametime);
            var world = rotateX;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotateX);

            mCube.Draw();
        }
    }
}
