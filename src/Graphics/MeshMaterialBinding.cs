using Graphics.Materials;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics
{
    public class MeshMaterialBinding
    {
        private readonly Device mDevice;
        private readonly Mesh mMesh;
        private readonly InputLayout mInputLayout;
        private EffectPass mPass;

        public MeshMaterialBinding(Device device, Material material, Mesh mesh)
        {
            mDevice = device;
            mMesh = mesh;
            mPass = material.GetFirstPass();

            mInputLayout = new InputLayout(device, mPass.Description.Signature, mesh.GetInputElements());
        }

        public void Draw()
        {
            mDevice.InputAssembler.SetInputLayout(mInputLayout);

            mMesh.OnFrame();

            mPass.Apply();
            mMesh.Draw();
        }
    }
}