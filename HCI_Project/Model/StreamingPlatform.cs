using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class StreamingPlatform
    {
        private int rating;
        private string name;
        private string image;
        private string description;
        private DateTime date;

        public StreamingPlatform(int rating, string name, string image)
        {
            this.rating = rating;
            this.name = name;
            this.image = image;
            this.description = name + ".rtf";
            this.date = DateTime.Now;
        }

        public int Rating { get => rating; set => rating = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Date { get => date; set => date = value; }


    }
}
