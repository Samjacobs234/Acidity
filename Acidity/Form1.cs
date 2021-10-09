using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acidity
{
    public partial class Form1 : Form
    {
        //Arrays for color matricies
        public float[] Negative;
        public float[] Identity;
        public float[] Red;
        public float[] Blue;
        public float[] Redinverse;
        public float[] Blueinverse;
        public float[] Colorshift180;
        public float[] Colorshift180B;

        public float colormultiplier=1;


        //Tracking Arrays, used for setting one of the colors to the screen's "current array" or setting a color as the next array in a cycle
        public float[] CurrentArray;
        public float[] NextArray;
        public float[] Transitioningarray;

        //controls how fast color shifting happens
        public float ColorshiftSpeed;

        //turns on and off color cycling
        public bool cycleon = true;
        public int Sleeptime;

        //For adding the array/colornames to the combo box.
        public float[][] Arraylist;
        public string[] Arraylistnames;


        public Form1()
        {
            Sleeptime = 1;
            
            InitializeComponent();
            #region Color Matrices
            Identity = new float[] {
                1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
                0.0f,  0.0f,  1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};

            Negative = new float[] {
                -1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.0f,  -1.0f,  0.0f,  0.0f, 0.0f,
                0.0f,  0.0f,  -1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                1.0f,  1.0f,  1.0f,  0.0f,  1.0f};

            Red = new float[] {
                0.3f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.6f,  0.0f,  0.0f,  0.0f, 0.0f,
                0.1f,  0.0f,  0.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};


            Blue = new float[] {
                0.0f,  0.0f,  0.3f,  0.0f,  0.0f ,
                0.0f,  0.0f,  0.6f,  0.0f, 0.0f,
                0.0f,  0.0f,  0.1f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};


            Redinverse = new float[] {
                -0.3f,  0.0f,  0.0f,  0.0f,  0.0f ,
                -0.6f,  0.0f,  0.0f,  0.0f, 0.0f,
                -0.1f,  0.0f,  0.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                1.0f,  0.0f,  0.0f,  0.0f,  1.0f};


            Blueinverse = new float[] {
                0.0f,  0.0f,  -0.3f,  0.0f,  0.0f ,
                0.0f,  0.0f,  -0.6f,  0.0f, 0.0f,
                0.0f,  0.0f,  -0.1f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  1.0f,  0.0f,  1.0f};

            Colorshift180 = new float[] {
               -0.333f,  0.666f, 0.666f,  0.0f,  0.0f ,
                0.666f, -0.333f,  0.666f,  0.0f, 0.0f,
                0.666f, 0.666f,  -0.333f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};

            Colorshift180B = new float[] {
               -1.0f,  1.0f, 1.0f,  0.0f,  0.0f ,
                1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
                1.0f, 1.0f,  -1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};
            #endregion
            CreateArrayList();
            SetCombobox();
            if (!Program.MagInitialize())
            {
                Program.MagInitialize();
            }

        }

        private void CreateArrayList()
        {
            Arraylist = new float[8][];
            Arraylist[0] = Identity;
            Arraylist[1] = Negative;
            Arraylist[2] = Red;
            Arraylist[3] = Blue;
            Arraylist[4] = Redinverse;
            Arraylist[5] = Blueinverse;
            Arraylist[6] = Colorshift180;
            Arraylist[7] = Colorshift180B;

            Arraylistnames = new string[8];
            Arraylistnames[0] = nameof(Identity);
            Arraylistnames[1] = nameof(Negative);
            Arraylistnames[2] = nameof(Red);
            Arraylistnames[3] = nameof(Blue);
            Arraylistnames[4] = nameof(Redinverse);
            Arraylistnames[5] = nameof(Blueinverse);
            Arraylistnames[6] = nameof(Colorshift180);
            Arraylistnames[7] = nameof(Colorshift180B);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cycleon = false; //Turn off any color cycles currently operating.

            Program.MagSetFullscreenColorEffect(CurrentArray);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cycleon = false; //Turn off any color cycles currently operating.
            Program.MagSetFullscreenColorEffect(Identity);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userchoice = comboBox1.Text;
            ArraySelection(userchoice);

        }

        public void SetCombobox() //Just adds all these choices to the combobox
        {
            comboBox1.Items.AddRange(Arraylistnames);
        }

        public void ArraySelection(string userchoice) //If the combo box is changed to one of the colors, set the screen to this color
        {

            //TO DO: Make a foreach loop or something to get rid of all these dumb reads
            switch (userchoice)
            {
                case "Identity":
                    CurrentArray = Identity;
                    break;

                case "Negative":
                    CurrentArray = Negative;
                    break;

                case "Red":
                    CurrentArray = Red;
                    break;

                case "Blue":
                    CurrentArray = Blue;
                    break;

                case "Redinverse":
                    CurrentArray = Redinverse;
                    break;

                case "Blueinverse":
                    CurrentArray = Blueinverse;
                    break;

                case "Colorshift180":
                    CurrentArray = Colorshift180;
                    break;

                case "Colorshift180B":
                    CurrentArray = Colorshift180B;
                    break;

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            cycleon = false; //Turn off any color cycles currently operating.
            CurrentArray = Identity;
            int m = 0;
            Random rnd = new Random();
            float[] tempArray = new float[25];//create an empty temporary array

            foreach (float elements in CurrentArray) //THIS IS AWESOME. CYCLES THRU TEXT BOXES LIKE A BREEZE
            {

                tempArray[m] = (float)rnd.NextDouble() * 2 - 1; //Put that box's value into a temp array, in the place corresponding to J
                m = m + 1;
            }


            CurrentArray = tempArray;
            Program.MagSetFullscreenColorEffect(CurrentArray);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cycleon = true;
            newArrayScreater();
            TransitionArray();
        }

        private void newArrayScreater()
        {
            CurrentArray = Identity;

            NextArray = new float[] {
                1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
                0.0f,  0.0f,  1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};
            }

        public void TransitionArray()
        {

            while (cycleon == true)
            {

                //The matrices "Identity" and "colorshiftB" must be specified again because i think my "Arraycomparison" accidently changes them to be equal.....
                Identity = new float[] {
                1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
                0.0f,  0.0f,  1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};

                Colorshift180B = new float[] {
               -1.0f,  1.0f, 1.0f,  0.0f,  0.0f ,
                1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
                1.0f, 1.0f,  -1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};

                Identity.CopyTo(NextArray, 0);
                Colorshift180B.CopyTo(CurrentArray, 0);
                ArrayComparisons();

                //The matrices "Identity" and "colorshiftB" must be specified again because i think my "Arraycomparison" accidently changes them to be equal.....
                Identity = new float[] {
                1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
                0.0f,  0.0f,  1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};

                Colorshift180B = new float[] {
               -1.0f,  1.0f, 1.0f,  0.0f,  0.0f ,
                1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
                1.0f, 1.0f,  -1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};

                Identity.CopyTo(CurrentArray, 0);
                Colorshift180B.CopyTo(NextArray, 0);
                ArrayComparisons();

            }
        }

        private void ArrayComparisons()
        {
            int j;
            Console.WriteLine("Identity");
            j = 0;



            Application.DoEvents();
            Console.WriteLine(j);
            while (j < CurrentArray.Length & cycleon==true) //Slowly changes the current array into the next targeted color
            {
                Console.WriteLine("conducting loop");
                Application.DoEvents();
                float compare = CurrentArray[j] - NextArray[j]; //First determine the difference between the current array Jth value to the next array

                if (compare < ColorshiftSpeed & compare > -ColorshiftSpeed) //Stops an infinite loop. If the two arrays are within a step of eachother set them equal and move on.
                {
                    CurrentArray[j] = NextArray[j]; //set them equal 
                    j++;//and move on to the next number in the array.

                }

                if (compare > 0) //If the difference is greater than 0, then subtract a small amount from current array
                {
                    CurrentArray[j] = CurrentArray[j] - ColorshiftSpeed;//Subtract this to get current array closer to next array

                    Program.MagSetFullscreenColorEffect(CurrentArray);//Set the screen to the new smaller color

                    System.Threading.Thread.Sleep(Sleeptime);
     
                }

                if (compare < 0)//If the difference is less than 0, then add a small amount from current array
                {
                    CurrentArray[j] = CurrentArray[j] + ColorshiftSpeed;
                    Program.MagSetFullscreenColorEffect(CurrentArray);
                    System.Threading.Thread.Sleep(Sleeptime);
                }








            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int trackbarvalue = trackBar1.Value;
            colormultiplier = (float)trackbarvalue;
            Console.WriteLine(colormultiplier);
            ColorshiftSpeed = .005f * colormultiplier;
            Console.WriteLine(ColorshiftSpeed);
           
        }


    }
}


