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
    public partial class Form2 : Form
    {
        public static YouTubeRequest request;
        public Form2(string username, string password)
        {
            InitializeComponent();
            Feed<Video> videoFeed = null;
            string devKey = "yt-dev-key";
            try
            {
                YouTubeRequestSettings settings =
                    new YouTubeRequestSettings("MassVideoEdit", devKey, username, password);
                request = new YouTubeRequest(settings);

                //getting all videos entry
                string uri = "http://gdata.youtube.com/feeds/api/users/default/uploads";
                videoFeed = request.Get<Video>(new Uri(uri));
                //Uri videoEntryUrl = new Uri(
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            createFormEntry(videoFeed);
        }

        //creating labels and checkboxes for video titles
        public void createFormEntry(Feed<Video> videoFeed)
        {
            int i = 1;
            try
            {
                foreach (Video videoEntry in videoFeed.Entries)
                {
                    Label label = new Label();
                    label.Name = "videoTitle" + i;
                    label.Text = videoEntry.Title;
                    label.TabIndex = i - 1;
                    label.AutoSize = true;
                    label.Location = new Point(45, 9 + (i * 25));
                    this.Controls.Add(label);

                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = "checkBox" + i;
                    checkBox.TabIndex = i - 1;
                    checkBox.Tag = videoEntry.VideoId;
                    checkBox.Location = new Point(25, 4 + (i * 25));
                    this.Controls.Add(checkBox);
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            Button edit = new Button();
            edit.Text = "Edit";
            edit.Location = new Point(120, 20 + (i * 25));
            edit.Click += new EventHandler(edit_Click);
            this.Controls.Add(edit);

            Label shared = new Label();
            shared.Name = "shared";
            shared.AutoSize = true;
            shared.Location = new Point(13,8);
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
            List<string> videoId = new List<string>();
            foreach (Control c in this.Controls.OfType<CheckBox>())
            {
                if (((CheckBox)c).Checked)
                {
                    videoId.Add((string)c.Tag);
                    
                } //if
            } //foreach
            
            Form3 form3 = new Form3(videoId);
            form3.ShowDialog();
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.filecake.net");
        }

 
    }//class
} //namespace