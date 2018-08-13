using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs2_WPF_Tanaeva
{

    class Departament
    {
        public string depName;

        public Departament(string depName)
        {
            this.depName = depName;
        }

        /// <summary>
        /// Метод, возвращающий всю информацию о департаменте в строке
        /// </summary>
        /// <returns></returns>
        public string DepToString()
        {
            return depName;
        }
    }
}
