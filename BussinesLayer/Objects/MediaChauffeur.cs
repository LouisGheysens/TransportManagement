namespace BussinesLayer.Objects
{
    public class MediaChauffeur
    {
        public Dictionary<string, Image> images = new Dictionary<string, Image>();

        public MediaChauffeur(Dictionary<string, Image> images)
        {
            this.images = images;
        }
    }
}
