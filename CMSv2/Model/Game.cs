using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv2.Model
{
    public class GameRequest : Game
    {
        public string type;       
    }

    public class Game
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AndroidLink { get; set; }
        public string AppleLink { get; set; }
        public string WebLink { get; set; }
        public string AgentLink { get; set; }
        public string ImageUrl { get; set; }
        public string ImageCaption { get; set; }
        public string ImageBase64 { get; set; }
        public Boolean IfComingSoon { get; set; }
        public Boolean IfMaintenance { get; set; }
        public Boolean IfHot { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
