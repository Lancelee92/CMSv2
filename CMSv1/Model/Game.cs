using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Model
{
    public class GameRequest : Game
    {
        public string type { get; set; }

        public Game getGame(Game Data)
        {
            return new Game()
            {
                ID = Data.ID,
                Title = Data.Title,
                Description = Data.Description,
                AndroidLink = Data.AndroidLink,
                AppleLink = Data.AppleLink,
                WebLink = Data.WebLink,
                AgentLink = Data.AgentLink,
                ImageUrl = Data.ImageUrl,
                ImageCaption = Data.ImageCaption,
                IfComingSoon = Data.IfComingSoon,
                IfMaintenance = Data.IfMaintenance,
                IfHot = Data.IfHot,
            };
        }
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
        public Boolean IfComingSoon { get; set; }
        public Boolean IfMaintenance { get; set; }
        public Boolean IfHot { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
