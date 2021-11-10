namespace ModernBoxes.Model
{
    public class OneWordModel
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public string hitokoto { get; set; }
        public string type { get; set; }
        public string from { get; set; }
        public object from_who { get; set; }
        public string creator { get; set; }
        public int creator_uid { get; set; }
        public int reviewer { get; set; }
        public string commit_from { get; set; }
        public string created_at { get; set; }
        public int length { get; set; }
    }
}