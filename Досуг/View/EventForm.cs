using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Досуг.View
{
    public partial class EventForm : Form
    {
        public EventForm ( )
        {
            InitializeComponent ( );
            Class.Helper.DB = new Entity.DBEventEntities();

        }

        string path = Application.StartupPath;
        public List<Entity.Event> ListEvent;
        private void EventForm_Load ( object sender, EventArgs e )
        {
            dataGridEvent.Rows.Clear();
            ListEvent = Class.Helper.DB.Event.ToList ( );
            int row = 0;
            Bitmap bitmap;
            string fullImage;
            string photo;
            foreach (var item in ListEvent)
            {
                dataGridEvent.Rows.Add ( );
                photo = item.EventPhoto;
                if(String.IsNullOrEmpty(photo) )
                {
                    bitmap = Properties.Resources.picture;
                }
                else
                {
                    // Задайте название изображения в ресурсах
                    bitmap = (Bitmap) Properties.Resources.ResourceManager.GetObject ( photo );
                    //fullImage = path + "\\Resources\\" + photo;
                    //if (File.Exists ( fullImage ))
                    //{
                    //    bitmap = new Bitmap ( fullImage );
                    //}
                    //else
                    //{
                    //    bitmap = Properties.Resources.picture;
                    //}
                }
                dataGridEvent.Rows [row].Cells [0].Value = bitmap;
                dataGridEvent.Rows [row].Cells [1].Value = item.EventName;
                dataGridEvent.Rows [row].Height = 95;
                row++;
            }
        }

        private void buttonCancel_Click ( object sender, EventArgs e )
        {
           
        }
    }
}
