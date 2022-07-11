namespace MongoExample.Infrastructure.DAL.Models
{
    public class SortByInfos
    {
        public string FieldName { get; set; }

        public bool IsAscending { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SortByInfos()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        { }

        public SortByInfos(string fileName, bool isAscending)
        {
            FieldName = fileName;
            IsAscending = isAscending;
        }
    }
}
