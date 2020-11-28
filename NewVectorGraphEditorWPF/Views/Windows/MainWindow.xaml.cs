using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewVectorGraphEditorWPF {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        #region Properties
        private MainWindowVM vm;
        private DrawCollection<Shape> shapes;
        private int selectedShapeIndex;
        private bool isPressed;
        #endregion

        public MainWindow() {
            InitializeComponent();
            vm = (MainWindowVM)DataContext;
            selectedShapeIndex = -1;
        }

        #region Methods
        #region Drawing Methods

        /// <summary>
        /// Создает на векторном поле эллипс и рисует его на канвасе
        /// </summary>
        /// <param name="x">Координата верхнего левого угла описанного прямоугольника по оси OX</param>
        /// <param name="y">Координата верхнего левого угла описанного прямоугольника по оси OY</param>
        private void DrawEllipse(double x, double y) {
            VEllipse vEl = new VEllipse(0, 0);
            vm.Field.VField.Add(vEl);
            vEl.SetPosition(x, y);

            Shape el = new Ellipse();
            el.Width = 0;
            el.Height = 0;
            el.Fill = new SolidColorBrush(Color.FromArgb(vEl.Fill.Alpha, vEl.Fill.Red, vEl.Fill.Green, vEl.Fill.Blue));
            el.Stroke = new SolidColorBrush(Color.FromArgb(vEl.Stroke.Alpha, vEl.Stroke.Red, vEl.Stroke.Green, vEl.Stroke.Blue));
            el.StrokeThickness = vEl.StrokeThickness;
            Canvas.SetLeft(el, x);
            Canvas.SetTop(el, y);

            canvas.Children.Add(el);
            shapes.Add(el);
        }

        /// <summary>
        /// Создает на векторном поле прямоугольник и рисует его на канвасе
        /// </summary>
        private void DrawRectangle(double x, double y) {
            VRectangle vRect = new VRectangle(0, 0);
            vm.Field.VField.Add(vRect);
            vRect.SetPosition(x, y);

            Shape rect = new Rectangle();
            rect.Width = 0;
            rect.Height = 0;
            rect.Fill = new SolidColorBrush(Color.FromArgb(vRect.Fill.Alpha, vRect.Fill.Red, vRect.Fill.Green, vRect.Fill.Blue));
            rect.Stroke = new SolidColorBrush(Color.FromArgb(vRect.Stroke.Alpha, vRect.Stroke.Red, vRect.Stroke.Green, vRect.Stroke.Blue));
            rect.StrokeThickness = vRect.StrokeThickness;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            canvas.Children.Add(rect);
            shapes.Add(rect);
        }

        /// <summary>
        /// Создает на векторном поле треугольник и рисует его на канвасе
        /// </summary>
        private void DrawTriangle(double x, double y) {
            VTriangle vTr = new VTriangle(0, 0);
            vm.Field.VField.Add(vTr);
            vTr.SetPosition(x, y);

            Polygon tr = new Polygon();
            tr.Width = 0;
            tr.Height = 0;
            tr.Fill = new SolidColorBrush(Color.FromArgb(vTr.Fill.Alpha, vTr.Fill.Red, vTr.Fill.Green, vTr.Fill.Blue));
            tr.Stroke = new SolidColorBrush(Color.FromArgb(vTr.Stroke.Alpha, vTr.Stroke.Red, vTr.Stroke.Green, vTr.Stroke.Blue));
            Canvas.SetLeft(tr, x);
            Canvas.SetTop(tr, y);
            canvas.Children.Add(tr);
            shapes.Add(tr);
        }

        private void UpdateTriangle(double x, double y) {
            int selectedShapeIndex = vm.Field.VField.GetSelectedShapeIndex();
            if (!(vm.Field.VField.GetSelectedShape() is VTriangle)) return;
            if (selectedShapeIndex == -1) return;

            double selectedPositionX = vm.Field.VField.GetSelectedShape().PositionX;
            double selectedPositionY = vm.Field.VField.GetSelectedShape().PositionY;
            double minX = Math.Min(x, selectedPositionX);
            double minY = Math.Min(y, selectedPositionY);
            double width = Math.Abs(x - selectedPositionX);
            double height = Math.Abs(y - selectedPositionY);

            Polygon tr = (Polygon)shapes[selectedShapeIndex];
            tr.Points = new PointCollection() {
                new Point(minX, minY + height),
                new Point(minX + width, minY + height),
                new Point(minX + width/2, minY)
            };
        }
        /// <summary>
        /// Обновляет отрисовку текущей (выделенной) фигуры. Должен выполняться в фоновом потоке!
        /// </summary>
        private void UpdateSelectedShape(double x, double y) {
            if (vm.Mode == EditMode.DrawTrMode) {
                UpdateTriangle(x, y);
                return;
            }

            int selectedShapeIndex = vm.Field.VField.GetSelectedShapeIndex();
            if (selectedShapeIndex == -1) return;

            double selectedPosX = vm.Field.VField.GetSelectedShape().PositionX;
            double selectedPosY = vm.Field.VField.GetSelectedShape().PositionY;

            Shape shape = shapes[selectedShapeIndex];
            Canvas.SetLeft(shape, Math.Min(selectedPosX, x));
            Canvas.SetTop(shape, Math.Min(selectedPosY, y));
        }
        #endregion
        /// <summary>
        /// Метод для события нажатия левой кнопки мыши по канвасу
        /// </summary>
        private void CanvasMouseLeftDown() {
            
        }

        /// <summary>
        /// Метод для события отпускания левой кнопки мыши по канвасу
        /// </summary>
        private void CanvasMouseLeftUp() {

        }

        /// <summary>
        /// Меняет цвет заливки внутренности фигуры выбранной на поле
        /// </summary>
        private void Fill() {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                vm.Field.VField.GetSelectedShape().ChangeFillColor(new VColor {
                    Alpha = cd.Color.A,
                    Red = cd.Color.R,
                    Green = cd.Color.G,
                    Blue = cd.Color.B
                });
            }
        }

        /// <summary>
        /// Меняет цвет границ фигуры, выбранной на поле
        /// </summary>
        private void Border() {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                vm.Field.VField.GetSelectedShape().ChangeBorderColor(new VColor {
                    Alpha = cd.Color.A,
                    Red = cd.Color.R,
                    Green = cd.Color.G,
                    Blue = cd.Color.B
                });
            }
        }

        /// <summary>
        /// Меняет режим на режим "Выбора"
        /// </summary>
        private void SetSelectMode() => vm.Mode = EditMode.SelectMode;

        /// <summary>
        /// Меняет режим на режим "Рисование эллипса
        /// </summary>
        private void SetDrawElMode() => vm.Mode = EditMode.DrawElMode;

        /// <summary>
        /// Меняет режим на режим "Рисование прямоугольника"
        /// </summary>
        private void SetDrawRectMode() => vm.Mode = EditMode.DrawRectMode;

        /// <summary>
        /// Меняет режим на режим "Риования треугольника
        /// </summary>
        private void SetDrawTrMode() => vm.Mode = EditMode.DrawTrMode;

        private void LiftUp() {
            
        }

        private void LiftStraightUp() {

        }

        private void LiftDown() {

        }

        private void LiftStraightDown() {

        }

        #endregion

        #region Events
        /// <summary>
        /// Событие нажатия на кнопку заливки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fillBtn_Click(object sender, RoutedEventArgs e) => Fill();

        /// <summary>
        /// Событие нажатия на кнопку окраски границ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void borderBtn_Click(object sender, RoutedEventArgs e) => Border();

        /// <summary>
        /// Событие нажатия на кнопку режима выделения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectModeButton_Click(object sender, RoutedEventArgs e) => SetSelectMode();

        /// <summary>
        /// Событие нажатия на кнопку режима эллипса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawElModeButton_Click(object sender, RoutedEventArgs e) => SetDrawElMode();

        /// <summary>
        /// Событие нажатия на кнопку режима прямоугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawRectModeButton_Click(object sender, RoutedEventArgs e) => SetDrawRectMode();


        /// <summary>
        /// Событие нажатия на кпоку режима треугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawTrModeButton_Click(object sender, RoutedEventArgs e) => SetDrawTrMode();


        /// <summary>
        /// Событие нажатия на кнопку "Открыть файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "Сохранить файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "На передний план"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void straightUpButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "Поместить выше"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "Поместить ниже"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "На задний план"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void straightDownButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на левую кнопку мыши на канвасе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => CanvasMouseLeftDown();

        /// <summary>
        /// Событие отпускания левой кнопки мыши на канвасе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => CanvasMouseLeftUp();
        #endregion


    }
}
