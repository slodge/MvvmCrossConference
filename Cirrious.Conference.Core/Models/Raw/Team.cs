namespace Cirrious.Conference.Core.Models.Raw
{
    public class Team
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get { return string.Format("{0} {1}", Firstname, Lastname); } }
        public string Description { get; set; }
        public string Image { get; set; }
        public int DisplayOrder { get; set; }
    }
}