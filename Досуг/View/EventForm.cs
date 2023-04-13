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
using Досуг.Class;

namespace Досуг.View
{
    public partial class EventForm : Form
    {
        public EventForm ( )
        {
            InitializeComponent ( );
            Class.Helper.DB = new Entity.DBEventEntities();

        }
        int filterDiraction;
        string search;
    
        public List<Entity.Event> ListEvent;
        private void EventForm_Load ( object sender, EventArgs e )
        {
            var direction = Helper.DB.EventDirection.Select(x=>x.EventDirectionName).ToList();
            direction.Insert ( 0, "Все направление" );
            comboBoxDirection.Items.Clear ( );
            comboBoxDirection.DataSource = direction;
            Data ( );
        }
        void Data ( )
        {
            dataGridEvent.Rows.Clear ( );

            ListEvent = Class.Helper.DB.Event.ToList ( );

            int row = 0;
            Bitmap bitmap;
            if(filterDiraction != 0)
            {
                ListEvent= ListEvent.Where(x=>x.EventDirection ==filterDiraction).ToList( );
            }
            if (!String.IsNullOrEmpty ( search ))
            {
                ListEvent = ListEvent.Where ( x => x.EventName.Contains ( search ) ).ToList ( );
            }

            foreach (var item in ListEvent)
            {
                dataGridEvent.Rows.Add ( );
             
                if (String.IsNullOrEmpty ( item.EventPhoto ))
                {
                    bitmap = Properties.Resources.picture;
                }
                else
                {
                    bitmap = (Bitmap) Properties.Resources.ResourceManager.GetObject ( item.EventPhoto );
                    if (bitmap == null)
                    {
                        bitmap = Properties.Resources.picture;
                    }
                }
                dataGridEvent.Rows [row].Cells [0].Value = bitmap;
                dataGridEvent.Rows [row].Cells [1].Value = item.EventName;
                dataGridEvent.Rows [row].Height = 95;

                dataGridEvent.Rows [row].DefaultCellStyle.BackColor = Color.FromArgb ( 4, 160, 255 );
                row++;
            }
        }
        private void buttonCancel_Click ( object sender, EventArgs e )
        {
           
        }

        private void comboBoxDirection_SelectedIndexChanged ( object sender, EventArgs e )
        {
            filterDiraction = comboBoxDirection.SelectedIndex;
            Data ( );
        }

        private void textBoxSearch_TextChanged ( object sender, EventArgs e )
        {
            search = textBoxSearch.Text;
            Data ( );
        }

        private void button1_Click ( object sender, EventArgs e )
        {
            string date = dateTimePicker1.Value.ToString ( );
        }

        private void maskedTextBox2_Leave ( object sender, EventArgs e )
        {
           
             string text =maskedTextBox2.Text;
            string [] parts = text.Split ( ':' );
            int hour = Convert.ToInt32(parts [0]);
            int minute = Convert.ToInt32 ( parts [1] );
            if(hour > 23 ||minute>59 )
            {
                MessageBox.Show ( "Неправильный формат времени" );
                maskedTextBox2.Text = null;
            }
        }
    }
}
