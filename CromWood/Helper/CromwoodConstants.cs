using CromWood.Data.Entities.Default;
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

        public static List<KeyValue> BuildingDates = new List<KeyValue>()
        {
            new KeyValue()
            {
                Id = Guid.Parse("20c391d3-2674-4bd5-887b-ea811ddff9c5"),
                Name = "1960's",
            },
            new KeyValue()
            {
                Id = Guid.Parse("38c002a3-f234-4408-a00b-ec0fcdca6df3"),
                Name = "1970's",
            },
            new KeyValue()
            {
                Id = Guid.Parse("93a093a0-7bbc-402c-a5ff-ea9b716fb7ee"),
                Name = "Post 1974",
            },
            new KeyValue()
            {
                Id = Guid.Parse("f4f7d564-360b-4fb6-bd4e-b6cf299e901a"),
                Name = "Post 1985",
            },
        };
    }
}
