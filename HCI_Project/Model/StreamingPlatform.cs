using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;

namespace HCI_Project.Model
{
    [Serializable]
    public class StreamingPlatform
    {
        private double rating;
        private string name;
        private string image;
        private string description;
        private DateTime date;
        private bool isChecked;

        public StreamingPlatform()
        {

        }
        public StreamingPlatform(double rating, string name, string image)
        {
            this.rating = rating;
            this.name = name;
            this.image = image;
            this.description = name + ".rtf";
            this.date = DateTime.Now;
            this.IsChecked = false;
        }



        public double Rating { get => rating; set => rating = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool IsChecked { get => isChecked; set => isChecked = value; }

        public void SaveAsRTF(FlowDocument doc)
        {
            TextRange range = new TextRange(doc.ContentStart, doc.ContentEnd);
            using (var stream = System.IO.File.Create(Name + ".rtf"))
            {
                range.Save(stream, DataFormats.Rtf);
            }
        }

        public FlowDocument ReadFromRTF()
        {
            FlowDocument flowDocument = new FlowDocument();

            try
            {
                using (var stream = File.OpenRead(description))
                {
                    TextRange range = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                    range.Load(stream, DataFormats.Rtf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading RTF file: {ex.Message}");
            }

            return flowDocument;
        }


    }
}
