namespace SiaERP.Models
{
    public class Documents
    {
        //Define private varibales
        private int _Id;
        private string _Name;
        private byte[] _Document;
        private string _Extension;

        //Define public properties
        public int Id
        { 
            get => _Id; 
            set => _Id = value; 
        }

        public string Name
        {
            get => _Name;
            set => _Name = value;
        }

        public byte[] Document
        { 
            get => _Document; 
            set => _Document = value;
        }

        public string Extension
        {
            get => _Extension;
            set => _Extension = value;
        }
    }
}
