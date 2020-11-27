using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.ViewModels {
    class VEllipseVM : ObservableObject {
        #region Fields
        private VEllipse vEllipse;
        #endregion

        #region Properties
        public VEllipse VEllipse {
            get => vEllipse; set {
                vEllipse = value;
                OnPropertyChanged("VEllipse");
            }
        }
        #endregion
    }
}
