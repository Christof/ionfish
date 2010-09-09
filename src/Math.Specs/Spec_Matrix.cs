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

        [Subject(typeof(Matrix))]
        public class identity_matrix
        {
            static Matrix resultMatrix;

            Because of = () => resultMatrix = Matrix.Identity;

            It should_have_assigned_value_to_Row_1_Colum_1 = () => resultMatrix.R1C1.ShouldEqual(1);
            It should_have_assigned_value_to_Row_1_Colum_2 = () => resultMatrix.R1C2.ShouldEqual(0);
            It should_have_assigned_value_to_Row_1_Colum_3 = () => resultMatrix.R1C3.ShouldEqual(0);
            It should_have_assigned_value_to_Row_1_Colum_4 = () => resultMatrix.R1C4.ShouldEqual(0);

            It should_have_assigned_value_to_Row_2_Colum_1 = () => resultMatrix.R2C1.ShouldEqual(0);
            It should_have_assigned_value_to_Row_2_Colum_2 = () => resultMatrix.R2C2.ShouldEqual(1);
            It should_have_assigned_value_to_Row_2_Colum_3 = () => resultMatrix.R2C3.ShouldEqual(0);
            It should_have_assigned_value_to_Row_2_Colum_4 = () => resultMatrix.R2C4.ShouldEqual(0);

            It should_have_assigned_value_to_Row_3_Colum_1 = () => resultMatrix.R3C1.ShouldEqual(0);
            It should_have_assigned_value_to_Row_3_Colum_2 = () => resultMatrix.R3C2.ShouldEqual(0);
            It should_have_assigned_value_to_Row_3_Colum_3 = () => resultMatrix.R3C3.ShouldEqual(1);
            It should_have_assigned_value_to_Row_3_Colum_4 = () => resultMatrix.R3C4.ShouldEqual(0);

            It should_have_assigned_value_to_Row_4_Colum_1 = () => resultMatrix.R4C1.ShouldEqual(0);
            It should_have_assigned_value_to_Row_4_Colum_2 = () => resultMatrix.R4C2.ShouldEqual(0);
            It should_have_assigned_value_to_Row_4_Colum_3 = () => resultMatrix.R4C3.ShouldEqual(0);
            It should_have_assigned_value_to_Row_4_Colum_4 = () => resultMatrix.R4C4.ShouldEqual(1);
        }

        [Subject(typeof(Matrix))]
        public class multiplication_of_two_matrices
        {
            static Matrix resultMatrix;

            static Matrix matrix1 = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            static Matrix matrix2 = new Matrix(
                55, 56, 57, 58,
                65, 66, 67, 68,
                75, 76, 77, 78,
                85, 86, 87, 88);

            Because of = () => resultMatrix = matrix1 * matrix2;

            It should_have_assigned_value_to_Row_1_Colum_1 = () => resultMatrix.R1C1.ShouldEqual(3550);
            It should_have_assigned_value_to_Row_1_Colum_2 = () => resultMatrix.R1C2.ShouldEqual(3600);
            It should_have_assigned_value_to_Row_1_Colum_3 = () => resultMatrix.R1C3.ShouldEqual(3650);
            It should_have_assigned_value_to_Row_1_Colum_4 = () => resultMatrix.R1C4.ShouldEqual(3700);

            It should_have_assigned_value_to_Row_2_Colum_1 = () => resultMatrix.R2C1.ShouldEqual(6350);
            It should_have_assigned_value_to_Row_2_Colum_2 = () => resultMatrix.R2C2.ShouldEqual(6440);
            It should_have_assigned_value_to_Row_2_Colum_3 = () => resultMatrix.R2C3.ShouldEqual(6530);
            It should_have_assigned_value_to_Row_2_Colum_4 = () => resultMatrix.R2C4.ShouldEqual(6620);

            It should_have_assigned_value_to_Row_3_Colum_1 = () => resultMatrix.R3C1.ShouldEqual(9150);
            It should_have_assigned_value_to_Row_3_Colum_2 = () => resultMatrix.R3C2.ShouldEqual(9280);
            It should_have_assigned_value_to_Row_3_Colum_3 = () => resultMatrix.R3C3.ShouldEqual(9410);
            It should_have_assigned_value_to_Row_3_Colum_4 = () => resultMatrix.R3C4.ShouldEqual(9540);

            It should_have_assigned_value_to_Row_4_Colum_1 = () => resultMatrix.R4C1.ShouldEqual(11950);
            It should_have_assigned_value_to_Row_4_Colum_2 = () => resultMatrix.R4C2.ShouldEqual(12120);
            It should_have_assigned_value_to_Row_4_Colum_3 = () => resultMatrix.R4C3.ShouldEqual(12290);
            It should_have_assigned_value_to_Row_4_Colum_4 = () => resultMatrix.R4C4.ShouldEqual(12460);
        }

        [Subject(typeof (Matrix))]
        public class translation
        {
            static Matrix translationMatrix;

            Because of = () => translationMatrix = Matrix.CreateTranslation(new Vector3(1, 2, 3));

            It should_create_a_translation_matrix = () => translationMatrix.ShouldEqualWithDelta(new Matrix(
                1, 0, 0, 1,
                0, 1, 0, 2,
                0, 0, 1, 3,
                0, 0, 0, 1));

            It should_translate_a_vector = () =>
                (translationMatrix * new Vector4(4, 5, 6, 1)).ShouldEqual(new Vector4(5, 7, 9, 1));
        }

        [Subject(typeof (Matrix))]
        public class multiplication_matrix_and_vector4
        {
            private static Vector4 resultVector;
            private static Vector4 vector = new Vector4(1,2,3,4);
            static Matrix matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            private Because of = () => resultVector = matrix * vector;

            private It should_calculate_the_new_vector = () => resultVector.ShouldEqual(
                new Vector4(
                    11 * 1 + 12 * 2 + 13 * 3 + 14 * 4,
                    21 * 1 + 22 * 2 + 23 * 3 + 24 * 4,
                    31 * 1 + 32 * 2 + 33 * 3 + 34 * 4,  
                    41 * 1 + 42 * 2 + 43 * 3 + 44 * 4));
        }

        [Subject(typeof (Matrix))]
        public class rotation_x
        {
            static Vector4 rotatedVector;

            Because of = () => rotatedVector = Matrix.RotateX(Constants.HALF_PI) * new Vector4(3, 2, 1, 1);

            It should_have_rotated_the_vector = () =>
                rotatedVector.ShouldEqual(new Vector4(3, -1, 2, 1));
        }

        [Subject(typeof (Matrix))]
        public class scale
        {
            static Vector4 scaledVector;

            Because of = () => scaledVector = Matrix.Scale(2) * new Vector4(1, 2, 3, 1);

            It should_have_scaled_the_vector = () =>
                scaledVector.ShouldEqual(new Vector4(2, 4, 6, 1));
        }
    }
}
