namespace OrderSystem.Win.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void toggleSidebarButton_Click(object sender, EventArgs e)
        {
            if (!mainContainer.Panel1Collapsed)
            {
                toggleSidebarButton.Image = resources.right;
                mainContainer.Panel1Collapsed = true;
            }
            else
            {
                toggleSidebarButton.Image = resources.left;
                mainContainer.Panel1Collapsed = false;
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void navProducts_Click(object sender, EventArgs e)
        {
        }

        private void navProducts_MouseEnter(object sender, EventArgs e)
        {
            pictureProduct.BackColor = Color.LightGray;
            labelProduct.BackColor = Color.LightGray;
            panelProduct.BackColor = Color.LightGray;
        }

        private void navProducts_MouseLeave(object sender, EventArgs e)
        {
            pictureProduct.BackColor = Color.White;
            labelProduct.BackColor = Color.White;
            panelProduct.BackColor = Color.White;
        }

        private void navCustomers_Click(object sender, EventArgs e)
        {
        }

        private void navOrders_Click(object sender, EventArgs e)
        {
        }

        private void navOrder_MouseEnter(object sender, EventArgs e)
        {
        }

        private void navOrder_MouseLeave(object sender, EventArgs e)
        {
        }

        private void navCustomer_MouseEnter(object sender, EventArgs e)
        {
        }

        private void navCustomer_MouseLeave(object sender, EventArgs e)
        {
        }
    }
}