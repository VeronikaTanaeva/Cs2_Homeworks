using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs2_WPF_Tanaeva
{
    class Presenter
    {
        private Departament dep;
        private IViewDeps viewDeps;
        private Employee emp;
        private IViewEmp viewEmp;

        public Presenter(IViewDeps viewDeps)
        {
            this.viewDeps = viewDeps;
            dep = new Departament();
        }

        public Presenter(IViewEmp viewEmp)
        {
            this.viewEmp = viewEmp;
            emp = new Employee();
        }

        public void LoadDeps()
        {
            dep.LoadData();
        }

        public void LoadEmp()
        {
            emp.LoadData();
        }

        public void AddDep()
        {

        }

    }
}
