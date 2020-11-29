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
            this.shapeDict = new Dictionary<VShapeType, int>();
            this.shapeDict.Add(VShapeType.VEllipse, 0);
            this.shapeDict.Add(VShapeType.VRectangle, 0);
            this.shapeDict.Add(VShapeType.VTriangle, 0);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Выделяет указанную фигуру
        /// </summary>
        /// <param name="i">Индекс фигуры в списке фигур</param>
        public void SetSelectedShape(int i) {
            if (i < -1 || i >= this.shapes.Count) throw new Exception();
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

        #region Override methods

        /// <summary>
        /// Добавляет новую фигуру в коллекцие и делает её выделенной
        /// </summary>
        /// <param name="shape"></param>
        public override void Add(VShape shape) {
            base.Add(shape);
            this.selectedShape = this.shapes.Count - 1;
            
            if (shape is VRectangle) {
                shapeDict[VShapeType.VRectangle]++;
                shape.SetName($"Rectangle {shapeDict[VShapeType.VRectangle]}");
            } else if (shape is VEllipse) {
                shapeDict[VShapeType.VEllipse]++;
                shape.SetName($"Ellipse {shapeDict[VShapeType.VEllipse]}");
            } else if (shape is VTriangle) {
                shapeDict[VShapeType.VTriangle]++;
                shape.SetName($"Triangle {shapeDict[VShapeType.VTriangle]}");
            } 
        }

        /// <summary>
        /// Убирает фигуру из коллекции и меняет выдел
        /// </summary>
        /// <param name="shape">Фигура, которую надо убрать</param>
        public override void Remove(VShape shape) {
            base.Remove(shape);
            this.selectedShape = this.shapes.Count - 1;

            if (shape is VRectangle) {
                if (shapeDict[VShapeType.VTriangle] == 0) return;
                shapeDict[VShapeType.VRectangle]--;
                shape.SetName($"Rectangle {shapeDict[VShapeType.VRectangle]}");
            } else if (shape is VEllipse) {
                if (shapeDict[VShapeType.VEllipse] == 0) return;
                shapeDict[VShapeType.VEllipse]++;
                shape.SetName($"Ellipse {shapeDict[VShapeType.VEllipse]}");
            } else if (shape is VTriangle) {
                if (shapeDict[VShapeType.VTriangle] == 0) return;
                shapeDict[VShapeType.VTriangle]++;
                shape.SetName($"Triangle {shapeDict[VShapeType.VTriangle]}");
            }
        }

        /// <summary>
        /// Убирает фигуру из коллекции и меняет индекс выделенной, если требуется
        /// </summary>
        /// <param name="i">Индекс убираемой фигуры</param>
        public override void RemoveAt(int i) {
            base.RemoveAt(i);
            if (i < selectedShape) selectedShape--;
            else if (i == selectedShape) selectedShape = -1;

            VShape shape = shapes[i];
            if (shape is VRectangle) {
                if (shapeDict[VShapeType.VTriangle] == 0) return;
                shapeDict[VShapeType.VRectangle]--;
                shape.SetName($"Rectangle {shapeDict[VShapeType.VRectangle]}");
            } else if (shape is VEllipse) {
                if (shapeDict[VShapeType.VEllipse] == 0) return;
                shapeDict[VShapeType.VEllipse]++;
                shape.SetName($"Ellipse {shapeDict[VShapeType.VEllipse]}");
            } else if (shape is VTriangle) {
                if (shapeDict[VShapeType.VTriangle] == 0) return;
                shapeDict[VShapeType.VTriangle]++;
                shape.SetName($"Triangle {shapeDict[VShapeType.VTriangle]}");
            }
        }

        /// <summary>
        /// Меняет фигуры местами и меняет индекс выделения, если требуется
        /// </summary>
        /// <param name="i">Индекс первой фигуры в коллекции</param>
        /// <param name="j">Индекс второй фигуры в коллекции</param>
        public override void Exchange(int i, int j) {
            base.Exchange(i, j);
            if (selectedShape == i) selectedShape = j;
            else if (selectedShape == j) selectedShape = i;
        }

        /// <summary>
        /// Меняет фигуры местами и меняет индекс выделения, если требуется
        /// </summary>
        /// <param name="sh1">Первая фигура</param>
        /// <param name="sh2">Вторая фигура</param>
        public override void Exchange(VShape sh1, VShape sh2) {
            base.Exchange(sh1, sh2);
            int sh1SelectedIndex = GetShapeIndex(sh1);
            int sh2SelectedIndex = GetShapeIndex(sh2);
            if (selectedShape == sh1SelectedIndex) selectedShape = sh2SelectedIndex;
            else if (selectedShape == sh2SelectedIndex) selectedShape = sh2SelectedIndex;
        }

        /// <summary>
        /// Опускает фигуру на план ниже и меняет индекс выделения, если была выделена данная фигура
        /// </summary>
        /// <param name="i">Индекс фигуры в коллекции</param>
        public override void LiftDown(int i) {
            base.LiftDown(i);
            if (selectedShape == i) selectedShape--;
        }

        /// <summary>
        /// Опускает фигуру на план ниже и меняет индекс выделения, если была выделена данная фигура
        /// </summary>
        /// <param name="sh">Фигура</param>
        public override void LiftDown(VShape sh) {
            base.LiftDown(sh);
            int shIndex = GetShapeIndex(sh);
            if (selectedShape == shIndex && selectedShape > 0) selectedShape--;
        }

        /// <summary>
        /// Поднимает фигуру на план ниже и меняет индекс выделения, если была выделена данная фигура
        /// </summary>
        /// <param name="i">Индекс фигуры, которую надо поднять</param>
        public override void LiftUp(int i) {
            base.LiftUp(i);
            if (selectedShape == i && selectedShape < shapes.Count - 1) selectedShape++;
        }

        /// <summary>
        /// Поднимает фигуру на план ниже и меняет индекс выделения, если была выделена данная фигура
        /// </summary>
        /// <param name="sh">Фигура, которую надо поднять</param>
        public override void LiftUp(VShape sh) {
            base.LiftUp(sh);
            int shIndex = GetShapeIndex(sh);
            if (selectedShape == shIndex && selectedShape < shapes.Count - 1) selectedShape++;
        }

        /// <summary>
        /// Поднимает фигуру на передний план и меняет индекс выденеия, если была выделена даная фигура
        /// </summary>
        /// <param name="i">Индекс фигуры, которую надо поднять</param>
        public override void LiftStraightUp(int i) {
            base.LiftStraightUp(i);
            if (selectedShape == i && i != -1) selectedShape = shapes.Count - 1;
        }

        /// <summary>
        /// Поднимает фигуру на передний план и меняет индекс выденеия, если была выделена даная фигура
        /// </summary>
        /// <param name="sh">Фигура, которую надо поднять</param>
        public override void LiftStraightUp(VShape sh) {
            base.LiftStraightUp(sh);
            int shIndex = GetShapeIndex(sh);
            if (selectedShape == shIndex && shIndex != -1) selectedShape = shapes.Count - 1;
        }

        /// <summary>
        /// Помещает фигуру на задний план и меняет индекс выденеия, если была выделена даная фигура
        /// </summary>
        /// <param name="i">Индекс фигуры, которую надо поместить на задний план</param>
        public override void LiftStraightDown(int i) {
            base.LiftStraightDown(i);
            if (selectedShape == i) selectedShape = 0;
        }

        /// <summary>
        /// Помещает фигуру на задний план и меняет индекс выденеия, если была выделена даная фигура
        /// </summary>
        /// <param name="sh">Фигура, которую надо поместить на задний план</param>
        public override void LiftStraightDown(VShape sh) {
            base.LiftStraightDown(sh);
            int shIndex = GetShapeIndex(sh);
            if (selectedShape == shIndex) selectedShape = 0;
        }
        #endregion

        #endregion
    }
}
