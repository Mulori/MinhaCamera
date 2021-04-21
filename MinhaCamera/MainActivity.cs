using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Provider;
using Android.Runtime;
using Android.Graphics;
using AndroidX.AppCompat.App;
using Android.Util;
using Java.IO;
using System.IO;

namespace MinhaCamera
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView imgView1;
        ImageView imgView2;
        Button btnCamera;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SetContentView(Resource.Layout.activity_main);
            btnCamera = FindViewById<Button>(Resource.Id.button1);
            imgView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            imgView2 = FindViewById<ImageView>(Resource.Id.imageView2);

            btnCamera.Click += button1_Click;

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            global::Android.Graphics.Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            
            var base_64 = encodeImage(bitmap);

            var bitmap_image = decodeImage(base_64);

            imgView1.SetImageBitmap(bitmap);

            imgView2.SetImageBitmap(bitmap_image); 
        }

        private string encodeImage(Bitmap bm)
        {
            MemoryStream baos = new MemoryStream();
            bm.Compress(Bitmap.CompressFormat.Jpeg, 100, baos);
            byte[] b = baos.ToArray();
            string encImage = Base64.EncodeToString(b, Base64Flags.Default);

            return encImage;
        }

        private Bitmap decodeImage(string base64)
        {
            byte[] decodedString = Base64.Decode(base64, Base64Flags.Default);
            Bitmap decodedByte = BitmapFactory.DecodeByteArray(decodedString, 0, decodedString.Length);

            return decodedByte;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }
}