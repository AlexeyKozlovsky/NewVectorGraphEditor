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
            VEllipse el = new VEllipse(0, 0);
            vm.Field.VField.Add(el);
            

            Shape el = new Ellipse();

        }

        /// <summary>
        /// Создает на векторном поле прямоугольник и рисует его на канвасе
        /// </summary>
        private void DrawRectangle() {

        }

        /// <summary>
        /// Создает на векторном поле треугольник и рисует его на канвасе
        /// </summary>
        private void DrawTriangle() {

        }

        /// <summary>
        /// Обновляет отрисовку текущей (выделенной) фигуры. Должен выполняться в фоновом потоке!
        /// </summary>
        private void UpdateSelectedShape() {

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
