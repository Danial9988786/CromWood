using CromWood.Models;

namespace CromWood.Helper
{
    public static class CromwoodConstants
    {
        public static List<FilterOption> NumberOption = new List<FilterOption>()
    {
        new FilterOption(){ Key= "Is Equal", Value ="eq"},
        new FilterOption(){ Key= "Is More Than", Value ="mt"},
        new FilterOption(){ Key= "Is More Than Equal", Value ="mte"},
        new FilterOption(){ Key= "Is Less Than", Value ="lt"},
        new FilterOption(){ Key= "Is Less Than Equal", Value ="lte"},
        new FilterOption(){ Key= "Is Between", Value ="in"}, // Requires two integers
    };

        public static List<FilterOption> StringOption = new List<FilterOption>()
    {
        new FilterOption() { Key = "Is Equal", Value = "eq" },
        new FilterOption() { Key = "Contains", Value = "ct" },
        new FilterOption() { Key = "Doesn't Contain", Value = "dct" },
        new FilterOption() { Key = "Is Empty", Value = "ie" },
        new FilterOption() { Key = "Is Not Empty", Value = "ine" },
    };

        public static List<FilterOption> BoolOption = new List<FilterOption>()
    {
        new FilterOption() { Key = "Is True", Value = "eqt" },
        new FilterOption() { Key = "Is False", Value = "eqf" },
    };

        public static List<FilterOption> GuidOption = new List<FilterOption>()
    {
        new FilterOption() { Key = "Is Equal", Value = "eq" },
        new FilterOption() { Key = "Is In List", Value = "inl" }, // Requires a list of values
    };

        public static List<FilterOption> DateTimeOption = new List<FilterOption>()
    {
        new FilterOption() { Key = "Is Equal", Value = "eq" },
        new FilterOption() { Key = "Is Between", Value = "in" }, // Requires two dates
        new FilterOption() { Key = "Is Earlier Than", Value = "iet" },
        new FilterOption() { Key = "Is Later Than", Value = "ilt" }
    };

    }
}
