using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;
using Math.Collision;

namespace Sandbox
{
    public class CollisionDetectionDemo : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mSphereMaterial;
        private MeshMaterialBinding mSphereBinding;
        private CommandBindingManagerBase mCameraCommandBindingManager;

        private BoundingSphere mSpheraA = new BoundingSphere(Vector3.Zero, 1);
        private BoundingSphere mSphereB = new BoundingSphere(new Vector3(1, 0, 0), 0.5f);

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

            mCameraCommandBindingManager = new FirstPersonCameraCommandBindingManager(commands, mInputCommandBinder, stand, this);

        }

        protected override void OnFrame()
        {
            mCameraCommandBindingManager.Update(Frametime);
            mKeyboard.Update();
            mInputCommandBinder.Update();

            var worldMatrixA = Matrix.CreateTranslation(mSpheraA.Center) * Matrix.Scale(mSpheraA.Radius);
            mSphereMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * worldMatrixA);
            mSphereBinding.Draw();

            var worldMatrixB = Matrix.CreateTranslation(mSphereB.Center) * Matrix.Scale(mSphereB.Radius);
            mSphereMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * worldMatrixB);
            mSphereBinding.Draw();
        }
    }
}
