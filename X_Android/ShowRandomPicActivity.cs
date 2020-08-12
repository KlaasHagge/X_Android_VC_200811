using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace X_Android
{
    [Activity(Label = "Show Random Pic")]
    public class ShowRandomPicActivity : Activity
    {
        //Control-Properties
        public EditText Etx_Height { get; set; }
        public EditText Etx_Width { get; set; }
        public Button Btn_GetRandomPic { get; set; }
        public ImageView Img_RandomPic { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Aufruf der Base-Methode (Grundlegende Activity-Initialisierung)
            base.OnCreate(savedInstanceState);

            //Verknüpfen und Öffenen der Layout-Datei (im layout-Ordner)
            SetContentView(Resource.Layout.activity_showRandomPic);

            //Zuweisung und Initialisierung der Controls
            Etx_Height = FindViewById<EditText>(Resource.Id.etx_randomPic_height);
            Etx_Width = FindViewById<EditText>(Resource.Id.etx_randomPic_width);
            Btn_GetRandomPic = FindViewById<Button>(Resource.Id.btn_goRandomPic);
            Img_RandomPic = FindViewById<ImageView>(Resource.Id.imgRandomPic);

            //Zuweisung einer Methode zum Click-Event des Buttons
            Btn_GetRandomPic.Click += async (s, e) =>
            {
                //Parsen der durch den Benutzer eingegebenen Höhe und Breite
                int breite = int.Parse(Etx_Width.Text);
                int hoehe = int.Parse(Etx_Height.Text);

                //Initialisieren des ProgressDialogs
                ProgressDialog progressDialog = new ProgressDialog(this);
                progressDialog.SetMessage("Downloading Picture...");

                //Öffnen des WebClients
                using (WebClient client = new WebClient())
                {
                    //Öffnen des ProgressDialogs
                    progressDialog.Show();

                    //Herunterladen des Bilds als Byte-Array
                    byte[] bild = await client.DownloadDataTaskAsync($"https://placeimg.com/{breite}/{hoehe}/any");
                    //Umwandlung des Arrays in ein Bitmap
                    Bitmap bitmap = BitmapFactory.DecodeByteArray(bild, 0, bild.Length);

                    //Setzen des Bildes in den ImageView
                    Img_RandomPic.SetImageBitmap(bitmap);

                    //Schließen des ProgressDialogs
                    progressDialog.Dismiss();
                };
            };
        }
    }
}