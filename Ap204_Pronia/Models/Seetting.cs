using System.Collections.Generic;

namespace Ap204_Pronia.Models
{
    public class Seetting
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string IconUrl { get; set; }
        public List<SocialMedia> medias { get; set; }

    }
}
