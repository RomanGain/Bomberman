using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class FieldCell
    {
        private CellType cellType;
        private Vector3 cellCenterPosition;

        public CellType CellType
        {
            get
            {
                return cellType;
            }
            set
            {
                cellType = value;
            }
        }

        public Vector3 CellCenterPosition
        {
            get
            {
                return cellCenterPosition;
            }
            set
            {
                cellCenterPosition = value;
            }
        }
    }
}