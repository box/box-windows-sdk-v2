using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Samples.WPF.ViewModels
{
    public class ViewModelLocator
    {
        private static MainViewModel _main;

        public static MainViewModel Main
        {
            get
            {
                if (_main == null)
                    _main = new MainViewModel();

                return _main;
            }
        }
    }
}