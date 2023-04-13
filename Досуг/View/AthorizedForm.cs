using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Досуг.View;

namespace Досуг
{
    public partial class AutorizedForm : Form
    {
        public AutorizedForm ( )
        {
            InitializeComponent ( );
            Class.Helper.DB = new Entity.DBEventEntities ( );
        }

        
        void CreateCaptcha ( )
        {
            Bitmap bitmap =new Bitmap( pictureBoxCaptcha.Width, pictureBoxCaptcha.Height );
            Graphics g = Graphics.FromImage( bitmap );
            string captcha = "";
            Random random=new Random();
            for (int i = 0; i < 6; i++)
            {
                char c = (char) random.Next ( 42, 90 );
                captcha += c;
            }
            g.DrawString(captcha,new Font("Arial",24),new SolidBrush(Color.Black),10,10);
            pictureBoxCaptcha.Image = bitmap;
        }

        private void buttonEnter_Click ( object sender, EventArgs e )
        {
            int number = 0;
            string password = "";
            try
            {
                 number = Convert.ToInt32 ( textBoxNumber.Text );
                 password = textBoxPassword.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Неправильный формат данных" );
                return;
            }
            Class.Helper.User = Class.Helper.DB.User.Where ( x => x.NumberId == number && x.Password == password ).ToList().FirstOrDefault();
            if(Class.Helper.User != null)
            {
                MessageBox.Show ( "Вы вошли за пользователя: " + Class.Helper.User.FirstName + " " + Class.Helper.User.LastName + Environment.NewLine+
                    "Роль: " + Class.Helper.User.UserRole1.UserRoleName );
                EventForm eventForm = new EventForm();
                eventForm.Show ( );
                this.Hide ();
            }
                
        }

        private void AutorizedForm_Load ( object sender, EventArgs e )
        {
            CreateCaptcha ( );
        }
    }
}
