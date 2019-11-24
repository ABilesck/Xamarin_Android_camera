using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using System;

namespace Camera
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView imgView1;
        Button btnCamera;
        Button btnGaleria;
        bool NovaFoto;
        public static readonly int PickImageId = 1000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
            Button button = FindViewById<Button>(Resource.Id.btnGaleria); 
            imgView1 = FindViewById<ImageView>(Resource.Id.imgvw1);

            btnCamera.Click += BtnCamera_Click;
            btnGaleria.Click += BtnGaleria_Click;
            button.Click += BtnGaleria_Click;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (NovaFoto)
            {
                base.OnActivityResult(requestCode, resultCode, data);
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                imgView1.SetImageBitmap(bitmap);
            }
            else
            {
                if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
                {
                    Android.Net.Uri uri = data.Data;
                    imgView1.SetImageURI(uri);
                }
            }
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            NovaFoto = true;
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        private void BtnGaleria_Click(object sender, EventArgs eventArgs)
        {
            NovaFoto = false;
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }
    }
}