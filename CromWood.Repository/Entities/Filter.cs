namespace CromWood.Data.Entities
{
    public class Filter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PageName { get; set; }
        public List<FilterAndCondition> AndCondition { get; set; }
        public List<FilterOrCondition> OrCondition { get; set; }
    }

    public class FilterAndCondition
    {
        public Guid Id { get; set; }
        public Guid FilterId { get; set; }
        public Filter Filter { get; set; }
        public string Condition { get; set; }
    }

    public class FilterOrCondition
    {
        public Guid Id { get; set; }
        public Guid FilterId { get; set; }
        public Filter Filter { get; set; }
        public string Condition { get; set; }
    }

    public class FilterColumn
    {
        public string Type { get; set; }
        public object FetchType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
    public class FilterColumnforEdit
    {
        public string Type { get; set; }
        public object FetchType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Alignment { get; set; }
        public int Width { get; set; }
        public string Append { get; set; }
        public string Prepend { get; set; }
    }
}
