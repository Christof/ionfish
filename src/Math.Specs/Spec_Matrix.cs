using Machine.Specifications;

namespace Math
{
    public class Spec_Matrix
    {
        [Subject(typeof(Matrix))]
        public class initialization_with_16_components
        {
            static Matrix matrix;

            Because of = () => matrix = new Matrix(
                1, 2, 3, 4, 
                5, 6, 7, 8,
                9, 10, 11, 12, 
                13, 14, 15, 16);

            It should_have_assigned_value_to_Row_1_Colum_1 = () => matrix.R1C1.ShouldEqual(1);
            It should_have_assigned_value_to_Row_1_Colum_2 = () => matrix.R1C2.ShouldEqual(2);
            It should_have_assigned_value_to_Row_1_Colum_3 = () => matrix.R1C3.ShouldEqual(3);
            It should_have_assigned_value_to_Row_1_Colum_4 = () => matrix.R1C4.ShouldEqual(4);

            It should_have_assigned_value_to_Row_2_Colum_1 = () => matrix.R2C1.ShouldEqual(5);
            It should_have_assigned_value_to_Row_2_Colum_2 = () => matrix.R2C2.ShouldEqual(6);
            It should_have_assigned_value_to_Row_2_Colum_3 = () => matrix.R2C3.ShouldEqual(7);
            It should_have_assigned_value_to_Row_2_Colum_4 = () => matrix.R2C4.ShouldEqual(8);

            It should_have_assigned_value_to_Row_3_Colum_1 = () => matrix.R3C1.ShouldEqual(9);
            It should_have_assigned_value_to_Row_3_Colum_2 = () => matrix.R3C2.ShouldEqual(10);
            It should_have_assigned_value_to_Row_3_Colum_3 = () => matrix.R3C3.ShouldEqual(11);
            It should_have_assigned_value_to_Row_3_Colum_4 = () => matrix.R3C4.ShouldEqual(12);

            It should_have_assigned_value_to_Row_4_Colum_1 = () => matrix.R4C1.ShouldEqual(13);
            It should_have_assigned_value_to_Row_4_Colum_2 = () => matrix.R4C2.ShouldEqual(14);
            It should_have_assigned_value_to_Row_4_Colum_3 = () => matrix.R4C3.ShouldEqual(15);
            It should_have_assigned_value_to_Row_4_Colum_4 = () => matrix.R4C4.ShouldEqual(16);
        }
    }
}
