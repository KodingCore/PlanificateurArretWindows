using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using static System.Windows.Forms.AxHost;

namespace PlanificateurArret
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TaskService ts = new TaskService())
            {

                Microsoft.Win32.TaskScheduler.Task tsk = ts.FindTask("Eteindre PC");
                if (tsk != null)
                {
                    ts.RootFolder.DeleteTask("Eteindre PC");
                }

                TaskDefinition td = ts.NewTask();
                TimeTrigger dt = (TimeTrigger)td.Triggers.Add(new TimeTrigger());
                DateTime timeExtinction = dateTimePicker1.Value;
                dt.StartBoundary = timeExtinction;
                td.Actions.Add(new ExecAction("shutdown", "/S", null));
                ts.RootFolder.RegisterTaskDefinition("Eteindre PC", td);
                MessageBox.Show("Tache créer à la date/heure suivante : " + timeExtinction);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (TaskService ts = new TaskService())
            {
                Microsoft.Win32.TaskScheduler.Task tsk = ts.FindTask("Eteindre PC");
                if (tsk != null)
                {
                    ts.RootFolder.DeleteTask("Eteindre PC");
                    MessageBox.Show("Tache supprimée");
                }
                else
                {
                    MessageBox.Show("Aucune tâche d'arrêt programmé");
                }
            }
        }
    }
}
