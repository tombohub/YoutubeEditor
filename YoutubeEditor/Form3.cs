using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.GData.YouTube;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;


namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3(List<string> videoId)
        {
            InitializeComponent();
            createFormEntry(videoId);

        }

        private void createFormEntry(List<string> videoId)
        {
            string uri = "http://gdata.youtube.com/feeds/api/users/default/uploads/";
            int i = 0;
            foreach (string id in videoId)
            {
                string videoUri = uri + id;
                Uri videoEntry = new Uri(videoUri);
                Video video = Form2.request.Retrieve<Video>(videoEntry);

                TextBox title = new TextBox();
                title.Location = new System.Drawing.Point(32, 52+(i*269));
                title.Name = "title";
                title.Tag = id;
                title.Size = new System.Drawing.Size(306, 20);
                title.TabIndex = i;
                title.Text = video.Title;
                this.Controls.Add(title);

                TextBox description = new TextBox();
                description.Location = new System.Drawing.Point(32, 88+(i*269));
                description.Multiline = true;
                description.Name = "description";
                description.Tag = id;
                description.Size = new System.Drawing.Size(306, 136);
                description.TabIndex = i+1;
                description.Text = video.Description;
                this.Controls.Add(description);

                TextBox tags = new TextBox();
                tags.Location = new System.Drawing.Point(32, 240+(i*269));
                tags.Multiline = true;
                tags.Name = "tags";
                tags.Tag = id;
                tags.Size = new System.Drawing.Size(306, 47);
                tags.TabIndex = i+2;
                tags.Text = video.Keywords;
                this.Controls.Add(tags);

                GroupBox line = new GroupBox();
                line.Location = new System.Drawing.Point(32, 293+(i*269));
                line.Name = "line"+i;
                line.Size = new System.Drawing.Size(315, 2);
                line.BackColor = SystemColors.WindowText;
                this.Controls.Add(line);

                i++;                
            } //foreach

            Button edit = new Button();
            edit.Text = "Edit";
            edit.Location = new Point(140, 30 + (i * 269));
            edit.Click += new EventHandler(edit_Click);
            this.Controls.Add(edit);

            Label shared = new Label();
            shared.Name = "shared";
            shared.AutoSize = true;
            shared.Location = new Point(13, 8);
            shared.Text = "Shared with:";
            this.Controls.Add(shared);

            LinkLabel link = new LinkLabel();
            link.Name = "link";
            link.AutoSize = true;
            link.Location = new Point(76, 8);
            link.Text = "www.filecake.net";
            link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            this.Controls.Add(link);
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                string uri = "http://gdata.youtube.com/feeds/api/users/default/uploads/";
                foreach (TextBox tb in this.Controls.OfType<TextBox>())
                {
                    string videoUrl = uri + tb.Tag;
                    Uri videoEntry = new Uri(videoUrl);
                    Video video = Form2.request.Retrieve<Video>(videoEntry);
                    switch (tb.Name)
                    {
                        case "title":
                            video.Title = tb.Text;
                            Form2.request.Update(video);
                            break;

                        case "description":
                            video.Description = tb.Text;
                            Form2.request.Update(video);
                            break;

                        case "tags":
                            video.Keywords = tb.Text;
                            Form2.request.Update(video);
                            break;
                    } //switch
                }//foreach
                MessageBox.Show("Check you profile");
            }//try
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.filecake.net");
        }

    }
}
