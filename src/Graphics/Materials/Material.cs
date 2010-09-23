using System;
using Math;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D10;

namespace Graphics.Materials
{
    public class Material
    {
        private readonly Device mDevice;
        private readonly Effect mEffect;

        public Material(string fileName, Device device)
        {
            mDevice = device;
            string errors;
            mEffect = Effect.FromFile(device, fileName, "fx_4_0",
                ShaderFlags.Debug, EffectFlags.None, null, null, out errors);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new Exception(string.Format("Compilation of {0} failed with error message: {1}.", 
                    fileName, errors));
            }
        }

        public void SetWorldViewProjectionMatrix(Matrix matrix)
        {
            mEffect.GetVariableBySemantic("WorldViewProjection")
                .AsMatrix().SetMatrix(matrix.ToSlimDX());
        }
        
        public EffectPass GetFirstPass()
        {
            return mEffect.GetTechniqueByIndex(0).GetPassByIndex(0);
        }

        public void SetWorld(Matrix worldMatrix)
        {
            mEffect.GetVariableBySemantic("World")
                .AsMatrix().SetMatrix(worldMatrix.ToSlimDX());
        }

        public void SetTexture(Texture2D texture)
        {
            mEffect.GetVariableByName("Texture").AsResource().SetResource(
                new ShaderResourceView(mDevice, texture));
        }
    }
}