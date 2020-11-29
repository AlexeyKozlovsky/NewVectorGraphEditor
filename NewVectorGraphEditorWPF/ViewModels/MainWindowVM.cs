using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.ViewModels {
    class MainWindowVM : ObservableObject {
        #region Fields
        private string selectedShapeNameString;
        private string selectedShapeWidthString;
        private string selectedShapeHeightString;
        private string selectedShapeThicknessString;

        private DrawCollection<string> elementsListBoxStrings;
        #endregion

        #region Properties
        public VFieldVM Field { get; set; }
        public EditMode Mode { get; set; }

        public string SelectedShapeNameString {
            get => selectedShapeNameString; set {
                selectedShapeNameString = value;
                OnPropertyChanged("SelectedShapeNameString");
            }
        }

        public string SelectedShapeWidthString {
            get => selectedShapeWidthString; set {
                selectedShapeWidthString = value;
                OnPropertyChanged("SelectedShapeWidthString");
            }
        }

        public string SelectedShapeHeightString {
            get => selectedShapeHeightString; set {
                SelectedShapeHeightString = value;
                OnPropertyChanged("SelectedShapeHeightString");
            }
        }

        public string SelectedShapeThicknessString {
            get => selectedShapeThicknessString; set {
                selectedShapeThicknessString = value;
                OnPropertyChanged("SelectedShapeThicknessString");
            }
        }

        public DrawCollection<string> ElementsListBoxStrings {
            get => elementsListBoxStrings; set {
                elementsListBoxStrings = value;
                OnPropertyChanged("ElementsListBoxString");
            }
        }
        #endregion

        #region Constructors
        public MainWindowVM() {
            Field = new VFieldVM();
        }
        #endregion


    }
}
