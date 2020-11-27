using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.ViewModels {
    class VShapeVM : ObservableObject {
        #region Fields
        private VShape vShape;
        #endregion

        #region Properties
        public VShape VShape {
            get => vShape; set {
                vShape = value;
                OnPropertyChanged("VShape");
            }
        }
        #endregion
    }
}
