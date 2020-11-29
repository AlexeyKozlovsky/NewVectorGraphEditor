using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Структура, определяющая цвет
    /// </summary>
    struct VColor {
        #region Properties
        public byte Alpha { get; set; }     // Параметр альфа
        public byte Red { get; set; }       // Параметр красного цвета
        public byte Green { get; set; }     // Параметр зеленого цвета
        public byte Blue { get; set; }      // Параметр синего цвета
        #endregion

        #region Constants
        public static readonly VColor BLACK = new VColor() { Alpha = 255, Red = 0, Green = 0, Blue = 0};
        public static readonly VColor WHITE = new VColor() { Alpha = 255, Red = 255, Green = 255, Blue = 255};
        #endregion



    }
}
