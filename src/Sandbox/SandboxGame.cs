using System.Drawing;
using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.DirectWrite;
using Factory = SlimDX.DirectWrite.Factory;
using FactoryType = SlimDX.DirectWrite.FactoryType;
using FontStyle = SlimDX.DirectWrite.FontStyle;
using Matrix = Math.Matrix;
using Triangle = Graphics.Primitives.Triangle;
using Vector3 = Math.Vector3;
using SlimDX.DXGI;

namespace Sandbox
{
    public class SandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private Material mSphereMaterial;
        private MeshMaterialBinding mQuadBinding;
        private MeshMaterialBinding mTriangleBinding;
        private MeshMaterialBinding mCubeBinding;
        private MeshMaterialBinding mSphereBinding;
        private Vector3RandomGenerator mVector3Random;
        private Vector3[] mCubePositions;
        private Vector3[] mTrianglePositions;
        private Vector3[] mQuadPositions;
        private CameraCommandManager mCameraCommandManager;
        private WindowRenderTarget mWindowRenderTarget;
        private TextFormat mTextFormat;
        private SolidColorBrush mBrush;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";
        private const string TURN_LEFT = "turn left";
        private const string TURN_RIGHT = "turn right";
        private const string TURN_UP = "turn up";
        private const string TURN_DOWN = "turn down";
        private const string ROLL_LEFT = "roll left";
        private const string ROLL_RIGHT = "roll right";

        protected override void Initialize()
        {
            var quadMesh = new Quad(Window.Device);
            var triangleMesh = new Triangle(Window.Device);
            var cubeMesh = new Cube(Window.Device);
            var sphereMesh = new Sphere(Window.Device);

            mMaterial = new Material("shader.fx", Window.Device);
            mSphereMaterial = new Material("normal_as_color.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, quadMesh);
            mTriangleBinding = new MeshMaterialBinding(Window.Device, mMaterial, triangleMesh);
            mCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, cubeMesh);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mSphereMaterial, sphereMesh);

            mKeyboard = new Keyboard();

            var stand = new FirstPersonStand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);
            commands.Add(TURN_LEFT, () => stand.Yaw(-Frametime));
            commands.Add(TURN_RIGHT, () => stand.Yaw(Frametime));
            commands.Add(TURN_UP, () => stand.Pitch(-Frametime));
            commands.Add(TURN_DOWN, () => stand.Pitch(Frametime));
            commands.Add(ROLL_LEFT, () => stand.Roll(Frametime));
            commands.Add(ROLL_RIGHT, () => stand.Roll(-Frametime));

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);
            mInputCommandBinder.Bind(Button.J, TURN_LEFT);
            mInputCommandBinder.Bind(Button.L, TURN_RIGHT);
            mInputCommandBinder.Bind(Button.I, TURN_UP);
            mInputCommandBinder.Bind(Button.K, TURN_DOWN);
            mInputCommandBinder.Bind(Button.U, ROLL_LEFT);
            mInputCommandBinder.Bind(Button.O, ROLL_RIGHT);
            
            mCameraCommandManager = new CameraCommandManager(commands, mInputCommandBinder, stand);

            mVector3Random = new Vector3RandomGenerator(new Vector3(-2, -1, -2), new Vector3(2, 1, 2), 1);
            mCubePositions = new[]
            {
                mVector3Random.Next(),
                mVector3Random.Next(),
                mVector3Random.Next()
            };

            mTrianglePositions = new[]
            {
                mVector3Random.Next(),
                mVector3Random.Next(),
                mVector3Random.Next()
            };

            mQuadPositions = new[]
            {
                mVector3Random.Next(),
                mVector3Random.Next(),
                mVector3Random.Next()
            };

            var factory = new Factory(FactoryType.Shared);
            mTextFormat = factory.CreateTextFormat("Consola", FontWeight.Normal, 
                FontStyle.Normal, FontStretch.Normal, 10, "en-us");
            mTextFormat.TextAlignment = TextAlignment.Center;
            mTextFormat.ParagraphAlignment = ParagraphAlignment.Center;
            mWindowRenderTarget = new WindowRenderTarget(new SlimDX.Direct2D.Factory(),
                new RenderTargetProperties
                {
                    PixelFormat = new PixelFormat(Format.Unknown, AlphaMode.Premultiplied),
                    Usage = RenderTargetUsage.None,
                    HorizontalDpi = 96,
                    VerticalDpi = 96,
                    MinimumFeatureLevel = FeatureLevel.Direct3D10,
                    Type = RenderTargetType.Default
                },
                new WindowRenderTargetProperties
                {
                    Handle = Window.Handle,
                    PixelSize = new Size(5, 5)
                });
            mBrush = new SolidColorBrush(mWindowRenderTarget, new Color4(1, 1, 1));
        }

        protected override void OnFrame()
        {
            mCameraCommandManager.Update(Frametime);
            mKeyboard.Update();
            mInputCommandBinder.Update();

            foreach (var position in mCubePositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
                mCubeBinding.Draw();
            }

            foreach (var position in mTrianglePositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
                mTriangleBinding.Draw();
            }

            foreach (var position in mQuadPositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
                mQuadBinding.Draw();
            }

            mSphereMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * Matrix.RotateX(Gametime));
            mSphereBinding.Draw();

            mWindowRenderTarget.BeginDraw();
            mWindowRenderTarget.Transform = Matrix3x2.Identity;
            mWindowRenderTarget.Clear(new Color4(0, 0, 0, 0));
            var layoutRectangle = new RectangleF(0, 0, 100, 100);
            mWindowRenderTarget.DrawText(string.Format("{0:0000} fps", 1.0 / Frametime),
                mTextFormat, layoutRectangle, mBrush);
            mWindowRenderTarget.EndDraw();
        }
    }
}
