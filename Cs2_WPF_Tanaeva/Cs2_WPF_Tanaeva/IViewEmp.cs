using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs2_WPF_Tanaeva
{
    interface IViewEmp
    {
        string name { get; set; }
        string surname { get; set; }
        string depName { get; set; }
        int age { get; set; }
        double salary { get; set; }
    }
}
