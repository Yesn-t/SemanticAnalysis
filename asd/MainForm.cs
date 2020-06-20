/*
 * Created by SharpDevelop.
 * User: Usuario
 * Date: 22/01/2019
 * Time: 09:23 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace asd
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		Sintactico analisis;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			analisis = new Sintactico();
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			
		}
		void TableLayoutPanel1Paint(object sender, PaintEventArgs e)
		{
			
		}
		void Button1Click(object sender, EventArgs e)
		{
			Semantico.variables.Clear();

			string copia;
			int indice;
			analisis = new Sintactico();
			analisis.setCadena(textBox1.Text);
			answerLabel.Text = analisis.Estado();
			
			copia = textBox1.Text;
			
			listView1.Items.Clear();
			listView2.Items.Clear();
			
			foreach(Error a in analisis.erroresDeCodigo()){
				ListViewItem lvi = new ListViewItem();
				lvi.Text = (a.indice.ToString());
				lvi.SubItems.Add(a.tipo);
				listView1.Items.Add(lvi);

			}
			
			indice = 0;
			string lineas = textBox1.Text;
			string[] partes = lineas.Split('\n');
			foreach(string linea in partes){
				copia = linea;
				copia = copia.Replace("\r","");
				indice++;
				ListViewItem lvi = new ListViewItem();
				lvi.Text = ((indice).ToString());
				lvi.SubItems.Add(copia);
				listView2.Items.Add(lvi);
			}
			
			foreach(Error a in analisis.erroresDeCodigo()){
				listView2.Items[a.indice-1].BackColor = Color.Red;
			}
			
//			foreach(Variable a in Semantico.variables){
//				ListViewItem lvi = new ListViewItem();
//				lvi.Text = (a.tipo);
//				lvi.SubItems.Add(a.nombre);
//				listView1.Items.Add(lvi);
//			}
		}
	}
}
