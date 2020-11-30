using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Перечисление, задающее тип фигуры
    /// </summary>
    [Serializable]
    enum VShapeType {
        VRectangle,         // Прямоугольник
        VEllipse,           // Эллипс
        VTriangle           // Треугольник
    }
}
