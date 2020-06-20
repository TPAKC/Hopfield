using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace HopfieldNET
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TPole Pole;
        readonly string FName;
        public string name = "";
        public string names = "";
        TBoxes Boxes;

        public MainWindow()
        {
            InitializeComponent();

            FName = "boxes.dat";
            name = textBox.Text;
            Pole = new TPole(g, name);

            LoadBox();
        }

        private void CmClear(object sender, RoutedEventArgs e)
        {
            textBox.Text = name;
            Pole = new TPole(g, name);
        }

        void LoadBox()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            if (File.Exists(FName))
            {
                using (FileStream fs = new FileStream(FName, FileMode.OpenOrCreate))
                {
                    Boxes = (TBoxes)formatter.Deserialize(fs);
                }

                if (Boxes.NN == Pole.NN)
                {
                    return;
                }
            }

            Boxes = new TBoxes(Pole.NN);

            using (FileStream fs = new FileStream(FName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Boxes);
            }

        }

        void StoreBox()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(FName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Boxes);
            }
        }

        private void CmAddBox(object sender, RoutedEventArgs e)
        {
            LoadBox();
            Pole.Box.name = textBox.Text;
            Boxes.Add(Pole.Box);

            StoreBox();
        }

        private void CmClearBoxes(object sender, RoutedEventArgs e)
        {
            Boxes = new TBoxes(Pole.NN);
            
            StoreBox();
        }

        int CurentBox = 0;

        private void CmShow(object sender, RoutedEventArgs e)
        {
            if (Boxes.Count == 0)
            {
                return;
            }

            if (CurentBox >= Boxes.Count)
            { 
                CurentBox = 0;
            }

            Pole.Box = new TBox(Boxes[CurentBox]);
            CurentBox++;

            textBox.Text = Pole.Draw();
            names = textBox.Text;
        }

        private void CmFind(object sender, RoutedEventArgs e)
        {
            if (Boxes.Count < 1)
            {
                return;
            }

            THopfield Hopfield = new THopfield(Boxes);

            TBox Box = Hopfield.Find(Pole.Box, 100);

            if (Box == null)
            {
                MessageBox.Show("Не знайдено дiагнозiв, схожих по характеристикам!");
            }
            else
            {
                Pole.Box = Box;
                name = Pole.Draw();
                textBox.Text = names;
            }

        }

        private void CmClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
