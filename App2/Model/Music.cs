using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace App2.Model
{
    public class Music
    {
        public String Title { get; set; }
        public String Artist { get; set; }
        public StorageFile MusicFile { get; set; }
        public IRandomAccessStream MusicStream { get; set; }
    }
}
