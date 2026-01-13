namespace PHBAPI.Model
{
    public class DocCaptureImages
    {
        public int ID { get; set; }
        public string SoPhieu { get; set; }
        public byte[] ImageData { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
    }

}
