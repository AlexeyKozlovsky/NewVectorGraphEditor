using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.ViewModels {
    class VTriangleVM : ObservableObject {
        #region Fields
        private VTriangle vTriangle;
        #endregion

        #region Properties
        public VTriangle VTriangle {
            get => vTriangle; set {
                vTriangle = value;
                OnPropertyChanged("VTriangle");
            }
        }
        #endregion
    }
}
