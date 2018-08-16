using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Cs2_WPF_Tanaeva
{

    public class Departament : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string depName { get; set; }

        public Departament()
        {
            this.depName = depName;
        }

        public void LoadData()
        {
            
        }


        /// <summary>
        /// Метод, возвращающий всю информацию о департаменте в строке
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.depName;
        }

        public void EditDep(string depName)
        {
            this.depName = depName;
        }

        public void AddDep(string name)
        {
            this.depName = name;
        }
    }
}
