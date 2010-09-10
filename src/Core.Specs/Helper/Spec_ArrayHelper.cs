using Machine.Specifications;

namespace Core.Helper
{
    public class Spec_ArrayHelper
    {
        [Subject(typeof(Spec_ArrayHelper))]
        public class create_an_array
        {
            static int[] resultArray;
            static int[] controlArray = new int[] {4,4};

            Because of = () => resultArray = ArrayHelper.Create(2, 4);

            It should_have_the_values = () => resultArray.ShouldEqual(controlArray);
        }
    }
}