using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class CollisionDetectionDemo : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mSphereMaterial;
        private MeshMaterialBinding mSphereBinding;
        private CameraCommandBindingManager mCameraCommandBindingManager;

        protected override void Initialize()
        {
            var sphereMesh = new Sphere(Window.Device);

            mSphereMaterial = new Material("normal_as_color.fx", Window.Device);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mSphereMaterial, sphereMesh);

            mKeyboard = new Keyboard();

            var stand = new FirstPersonStand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            
            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            
            mCameraCommandBindingManager = new CameraCommandBindingManager(commands, mInputCommandBinder, stand, this);

        }

        protected override void OnFrame()
        {
            mCameraCommandBindingManager.Update(Frametime);
            mKeyboard.Update();
            mInputCommandBinder.Update();
            
            mSphereMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * Matrix.RotateX(Gametime));
            mSphereBinding.Draw();
        }
    }
}
