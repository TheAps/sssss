﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LiteChat
{
    public partial class regis : Form
    {
        string imgd = "";
        public regis()
        {
            InitializeComponent();
        }

        private void regis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(username.Text))
            {
                var client = new MongoClient("mongodb://125.27.10.67:27017");
                var mongo = client.GetDatabase("LiteChat");
                var coll = mongo.GetCollection<BsonDocument>("USER");



                var filter = Builders<BsonDocument>.Filter.Eq("user", username.Text);
                
                var check = coll.Find(filter).Count();

                if (check > 0)
                {
                    MessageBox.Show("This User Already Registed");

                }
                else
                {
                    if (password.Text != repassword.Text||String.IsNullOrEmpty(password.Text)||String.IsNullOrEmpty(repassword.Text))
                    {
                        MessageBox.Show("Please Enter Correct Password");
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(name.Text))
                        {   
                            
                            
                            if (string.IsNullOrEmpty(imgd))
                            {

                            }
                            var img = Image.FromFile(imgd);
                            byte[] imageArray = System.IO.File.ReadAllBytes(imgd);
                            string image = Convert.ToBase64String(imageArray);
                            var data = new BsonDocument
                            {
                                {"user",username.Text },
                                {"password",password.Text },
                                {"name",name.Text },
                                {"image",image },
                                {"status","member" }
                            };
                            
                            coll.InsertOneAsync(data);
                            MessageBox.Show("Regised");
                            this.Visible = false;

                        }
                        else
                        {
                            MessageBox.Show("Please Enter Your Name");
                        }
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int size = -1;
            var result = new OpenFileDialog(); // Show the dialog.
            
            result.Title = "Browse Your Profile Image";
            result.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
            DialogResult re = result.ShowDialog();
            if (re==DialogResult.OK) // Test result.
            {
                string File = result.FileName;
                try
                {
                    imgd = File;
                    var img = Image.FromFile(imgd);
                    
                    pic.BackgroundImage = img;
                    

                }
                catch (System.IO.IOException)
                {

                }
            }
        }

        private void regis_Load(object sender, EventArgs e)
        {

        }
    }
}
