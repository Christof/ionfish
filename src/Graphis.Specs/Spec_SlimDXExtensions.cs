using Machine.Specifications;
using Math;

namespace Graphics
{
    [Subject(typeof(SlimDXExtensions))]
    public class convert_our_matrix_to_slimdx_matrix
    {
        public static Matrix ourMatrix = new Matrix(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16);
        public static SlimDX.Matrix slimDXMatrix;

        Because of = () => slimDXMatrix = ourMatrix.ToSlimDX();

        It should_have_assigned_value_to_Row_1_Colum_1 = () => slimDXMatrix.M11.ShouldEqual(1);
        It should_have_assigned_value_to_Row_1_Colum_2 = () => slimDXMatrix.M12.ShouldEqual(2);
        It should_have_assigned_value_to_Row_1_Colum_3 = () => slimDXMatrix.M13.ShouldEqual(3);
        It should_have_assigned_value_to_Row_1_Colum_4 = () => slimDXMatrix.M14.ShouldEqual(4);
                                                               
        It should_have_assigned_value_to_Row_2_Colum_1 = () => slimDXMatrix.M21.ShouldEqual(5);
        It should_have_assigned_value_to_Row_2_Colum_2 = () => slimDXMatrix.M22.ShouldEqual(6);
        It should_have_assigned_value_to_Row_2_Colum_3 = () => slimDXMatrix.M23.ShouldEqual(7);
        It should_have_assigned_value_to_Row_2_Colum_4 = () => slimDXMatrix.M24.ShouldEqual(8);
                                                               
        It should_have_assigned_value_to_Row_3_Colum_1 = () => slimDXMatrix.M31.ShouldEqual(9);
        It should_have_assigned_value_to_Row_3_Colum_2 = () => slimDXMatrix.M32.ShouldEqual(10);
        It should_have_assigned_value_to_Row_3_Colum_3 = () => slimDXMatrix.M33.ShouldEqual(11);
        It should_have_assigned_value_to_Row_3_Colum_4 = () => slimDXMatrix.M34.ShouldEqual(12);
                                                               
        It should_have_assigned_value_to_Row_4_Colum_1 = () => slimDXMatrix.M41.ShouldEqual(13);
        It should_have_assigned_value_to_Row_4_Colum_2 = () => slimDXMatrix.M42.ShouldEqual(14);
        It should_have_assigned_value_to_Row_4_Colum_3 = () => slimDXMatrix.M43.ShouldEqual(15);
        It should_have_assigned_value_to_Row_4_Colum_4 = () => slimDXMatrix.M44.ShouldEqual(16);
    }
}