public class GUI{
	Form form;
	public GUI(){
		form = new Form();
	}
	public void set(){
		form.Text = "Hello World";
		form.Size = new Size(300, 300);
		form.BackColor = Color.Black;
		form.ForeColor = Color.White;
		form.FormBorderStyle = FormBorderStyle.FixedDialog;
		form.MaximizeBox = false;
		form.MinimizeBox = false;
		form.StartPosition = FormStartPosition.CenterScreen;

		TextBox textBox = new TextBox();
		textBox.Text = "Hello World";
		textBox.Location = new Point(10, 10);
		textBox.Size = new Size(200, 50);
		form.Controls.Add(textBox);

		Button button = new Button();
		button.Text = "Click Me";
		button.Location = new Point(10, 40);
		button.Size = new Size(100, 20);
		form.Controls.Add(button);
	}
	public void show(){
		form.Show();
		Application.Run(form);
		// MessageBox.Show("MsgBox");
	}
}
