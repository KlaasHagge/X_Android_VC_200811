using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Org.Apache.Http.Protocol;

namespace X_Android
{
    //Jede Android-Activity steht für eine Aktion oder ein Layout, welche durch die App durchgeführt
    //oder angezeigt wird. Der 'Code Behind' einer Activity ist eine C#-Klasse, welche mit dem 'Activity'
    //-Attribut gekennzeichnet ist. Hier kann auch der evtl. angezeigte Titel und der verwendete Style
    //definiert werden.
    //Soll die Activity die zuerst angezeigte Activity (=Startseite) der App sein, muss hier die Property
    //'MainLauncher' auf true stehen.
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //Properties, in welchen die Steuerelemente des Layouts für den Zugriff durch C# abgelegt werden
        public EditText editTextInput { get; set; }
        public Button btnKlickMich { get; set; }

        //Methode, welche beim Starten (Initialisieren) der Activity ausgeführt wird
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Aufruf der Base-OnCreate()-Methode (Grundlegende Activity-Initialisierung)
            base.OnCreate(savedInstanceState);
            //Initialisierung der Xamarin-Essentials
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //Zuweisung und Aktivierung eines Layouts (aus dem layout-Ordner) zu dieser Activity. Dies
            //erfolgt mittels der Ressourcen-Klassen).
            SetContentView(Resource.Layout.activity_main);

            //Zuweisung der UI-Elemente zu den Properties mittels der FindViewById<>()-Methode,
            //welche die Resource-Klassen nach der angegebenen Id durchsucht.
            editTextInput = FindViewById<EditText>(Resource.Id.editTextInput);
            btnKlickMich = FindViewById<Button>(Resource.Id.btnKlickMich);

            //Zuweisung einer Methode zu einem Click-Event eines Buttons.
            //Diese Methode kreiert einen Toast (kl. Anzeige am unteren Bildschirmrand) und zeigt ihn an.
            btnKlickMich.Click += (s, e) => Toast.MakeText(this, $"Ihre gewählte Zahl ist {editTextInput.Text}.", ToastLength.Long).Show();

            //Impliziter Intent (Verweis auf eine Activity, welche mit dem ihrem Typen zugeordneten
            //Standartprogramm geöffnet wird) am Beispiel eines Webpage-Aufrufs im Standartbrowser.

            //Zuweisung des Buttons
            Button btnOpenWebpage = FindViewById<Button>(Resource.Id.btnOpenWebpage);
            //Erstellung des Intents
            Intent implizieterIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://www.google.de"));
            //Zuweisung des Click-Events mit der StartActivity()-Methode, welcher der Intent übergeben wird
            btnOpenWebpage.Click += (s, e) => StartActivity(implizieterIntent);

            //Expliziter Intent (Verweis auf eine Activity, welche in einer genau definierten App ausgeführt wird)
            //Am Beispiel des Öffnens eines neuen Layouts

            //Zuweisung des Buttons
            Button btnGeheWeiter = FindViewById<Button>(Resource.Id.btnExplicit);
            //Erstellung des Intents
            Intent expliziterIntent = new Intent(this, typeof(ShowRandomPicActivity));
            //Zuweisung des Click-Events
            btnGeheWeiter.Click += (s, e) => StartActivity(expliziterIntent);

            //Todo-Übung
            Button btnTodo = FindViewById<Button>(Resource.Id.btntodos);
            btnTodo.Click += (s, e) => StartActivity(new Intent(this, typeof(TodoActivity)));

        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}