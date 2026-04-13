namespace StoerreOpgaveBackend.models
{
    public class DrMusic
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public int duration { get; set; }
        public int publicationDate { get; set; }

        override public string ToString()
        {
            return $"Id: {Id}, Title: {title}, Artist: {artist}, Duration: {duration}, Publication Date: {publicationDate}";
        }
        //hej
        //hej  
    }
}
