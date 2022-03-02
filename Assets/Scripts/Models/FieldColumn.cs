using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class FieldColumn
    {
        private List<FieldCell> fieldCells;
        public List<FieldCell> FieldCells
        {
            get
            {
                return fieldCells;
            }
            set
            {
                fieldCells = value;
            }
        }


        public FieldColumn()
        {
            fieldCells = new List<FieldCell>();
        }
    }
}