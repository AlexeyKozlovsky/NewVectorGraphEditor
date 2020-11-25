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
        public byte Alpha { get; set; }     // Параметр альфа
        public byte Red { get; set; }       // Параметр красного цвета
        public byte Green { get; set; }     // Параметр зеленого цвета
        public byte Blue { get; set; }      // Параметр синего цвета
    }
}
