using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс, определяющий поле для фигур
    /// </summary>
    class VField : VShapeCollection {
        #region Fields
        private double width;                           // Ширина поля
        private double height;                          // Высота поля
        private int selectedShape;                      // Индекс выделенной фигуры
        private Dictionary<VShapeType, int> shapeDict;  // Словарь фигур, показывающий сколько фигур каждого типа есть на поле
        #endregion

        #region Constructors
        public VField(double width, double height) : base() {
            this.width = width;
            this.height = height;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Выделяет указанную фигуру
        /// </summary>
        /// <param name="i">Индекс фигуры в списке фигур</param>
        public void SetSelectedShape(int i) {
            if (i < 0 || i >= this.shapes.Count) throw new Exception();
            this.selectedShape = i;
        }

        /// <summary>
        /// Получает индекс выделенной фигуры
        /// </summary>
        /// <returns>Возвращает индекс выделенной фигуры</returns>
        public int GetSelectedShapeIndex() => this.selectedShape;

        /// <summary>
        /// Получает выделенную фигуру
        /// </summary>
        /// <returns>Возвращает фигуру, выделенную в данный момент</returns>
        public VShape GetSelectedShape() => this.shapes[this.selectedShape];

        /// <summary>
        /// Меняет размер выделенной фигуры
        /// </summary>
        /// <param name="width">Новая ширина фигуры</param>
        /// <param name="height">Новая высота фигуры</param>
        public void ChangeSelectedShapeSize(double width, double height) => GetSelectedShape().ChangeSize(width, height);

        /// <summary>
        /// Меняет цвет заливки выделенной фигуры
        /// </summary>
        /// <param name="fill">Новый цвет заливки фигуры</param>
        public void ChangeSelectedShapeFillColor(VColor fill) => GetSelectedShape().ChangeFillColor(fill);

        /// <summary>
        /// Меняет цвет границ выделенной фигуры
        /// </summary>
        /// <param name="stroke">Новый цвет границ фигуры</param>
        public void ChangeSelectedShapeStrokeColor(VColor stroke) => GetSelectedShape().ChangeBorderColor(stroke);

        /// <summary>
        /// Меняет толщину границ выделенной фигуры
        /// </summary>
        /// <param name="strokeThickness">Новая толщина границ фигуры</param>
        public void ChangeSelectedShapeThickness(int strokeThickness) => GetSelectedShape().ChangeStrokeThickness(strokeThickness);

        #endregion
    }
}
