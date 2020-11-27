using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.ViewModels {
    class VFieldVM : ObservableObject {
        #region Fields
        private VField vField;
        #endregion

        #region Properties
        public VField VField {
            get => vField; set {
                vField = value;
                OnPropertyChanged("VField");
            }
        }
        #endregion
    }
}
