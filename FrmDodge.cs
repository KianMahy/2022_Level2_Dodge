using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace _2022_Level2_Dodge
{
    public partial class FrmDodge : Form
    {
        bool left, right;
        string move;
        int score, lives;
        bool x, y;

        Graphics g; //declare a graphics object called g
                    // declare space for an array of 7 objects called planet 
        Planet[] planet = new Planet[7];
        Random yspeed = new Random();
        Spaceship spaceship = new Spaceship();


        public FrmDodge()
        {
            InitializeComponent();
            for (int i = 0; i < 7; i++)
            {
                int x = 10 + (i * 75);
                planet[i] = new Planet(x);

            }
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PnlGame, new object[] { true });


        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {

            //get the graphics used to paint on the panel control
            g = e.Graphics;
            //call the Planet class's DrawPlanet method to draw the image planet1 
            for (int i = 0; i < 7; i++)
            {
                // generate a random number from 5 to 20 and put it in rndmspeed
                int rndmspeed = yspeed.Next(5, 20);
                planet[i].y += rndmspeed;
                //call the Planet class's drawPlanet method to draw the images
                planet[i].DrawPlanet(g);
            }
            spaceship.DrawSpaceship(g);

        }



        private void TmrPlanet_Tick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                planet[i].MovePlanet();

                //if a planet reaches the bottom of the Game Area reposition it at the top
                if (planet[i].y >= PnlGame.Height)
                {
                    score += 1;//update the score
                    LblScore.Text = score.ToString();// display score
                    planet[i].y = 30;
                }

                if (spaceship.spaceRec.IntersectsWith(planet[i].planetRec))
                {
                    //reset planet[i] back to top of panel
                    planet[i].y = 30; // set  y value of planetRec
                    lives -= 1;// lose a life
                    LblLives.Text = lives.ToString();// display number of lives
                    CheckLives();
                }

               

            }
            PnlGame.Invalidate();//makes the paint event fire to redraw the panel
        }

        private void FrmDodge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = true; }
            if (e.KeyData == Keys.Right) { right = true; }

        }

        private void FrmDodge_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Left) { left = false; }
            if (e.KeyData == Keys.Right) { right = false; }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TmrShip_Tick(object sender, EventArgs e)
        {
            if (right) // if right arrow key pressed
            {
                move = "right";
                spaceship.MoveSpaceship(move);
            }
            if (left) // if left arrow key pressed
            {
                move = "left";
                spaceship.MoveSpaceship(move);
            }

            if (score == 20)
            {
                spaceship.x = 50;
                spaceship.y = 20;
                TmrShip.Enabled = true;
                TmrShip_move.Enabled = true;
                
            }

        }
       

        private void MnuStart_Click(object sender, EventArgs e)
        {
            score = 0;
            LblScore.Text = score.ToString();
            // pass lives from LblLives Text property to lives variable
            lives = int.Parse(LblLives.Text);

            TmrPlanet.Enabled = true;
            TmrShip.Enabled = true;
        }

        private void MnuStop_Click(object sender, EventArgs e)
        {
            TmrShip.Enabled = false;
            TmrPlanet.Enabled = false;

        }

        private void FrmDodge_Load(object sender, EventArgs e)
        {
            // pass lives from LblLives Text property to lives variable
            lives = int.Parse(LblLives.Text);

        }

        private void TmrShip_move_Tick(object sender, EventArgs e)
        {
            int RotateFlipType = 180;
        }

        private void LblScore_Click(object sender, EventArgs e)
        {
       
        }

       

        private void LblLives_Click(object sender, EventArgs e)
        {
        
        }
        private void CheckLives()
        {
            if (lives == 0)
            {
                TmrPlanet.Enabled = false;
                TmrShip.Enabled = false;
                MessageBox.Show("Game Over");

            }
        }

    }
}


 