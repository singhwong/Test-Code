using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    class PlayList
    {
        public String PlayListName { get; set; }
        public AdvancedCollectionView Songs = new AdvancedCollectionView(new ObservableCollection<Music>());
    }
}
