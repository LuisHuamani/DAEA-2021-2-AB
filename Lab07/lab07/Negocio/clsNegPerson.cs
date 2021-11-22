using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;

namespace Negocio
{
    public class clsNegPerson
    {
        clsDAOPerson daoPerson = new clsDAOPerson();

        public DataTable getAll()
        {
            return daoPerson.GetAll();
        }

        public DataTable getByValue(String strValue)
        {
            return daoPerson.GetByValue(strValue);
        } 
    }
}
