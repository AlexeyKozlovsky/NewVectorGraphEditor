using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.ViewModels {
    class VRectangleVM : ObservableObject {
        #region Fields
        private VRectangle vRect;
        #endregion

        #region Properties
        public VRectangle VRect {
            get => vRect; set {
                vRect = value;
                OnPropertyChanged("VRect");
            }
        }
        #endregion
    }
}
