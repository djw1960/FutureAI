using EF.Entitys;
using Serv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Future.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_50_Click(object sender, EventArgs e)
        {
            var ibll = OperationContext.BLLSession;
            //50种生产资料导入
            string content = this.txt_content.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("数据为空");
                return;
            }
            List<string> clist = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in clist)
            {
                FDataMaterial model = new FDataMaterial();
                List<string> trlist = item.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!trlist.Contains("PName")&& !trlist.Contains("PCode"))
                {
                    model.PName = trlist[0];
                    model.PCode = trlist[1];
                    model.PCate = trlist[2];
                    model.Unit = trlist[3];
                    model.Price = Convert.ToDecimal(trlist[4]);
                    model.RisePrice = Convert.ToDecimal(trlist[5]);
                    model.RiseRange = Convert.ToDecimal(trlist[6]);
                    model.DateTime = Convert.ToInt32(trlist[7]);
                    ibll.FDataMaterial.Add(model);
                }
            }

            int num = ibll.FDataMaterial.SaveChanges();

        }
    }
}
