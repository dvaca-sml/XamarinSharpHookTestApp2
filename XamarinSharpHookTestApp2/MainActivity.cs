using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using SharpHook;
using SharpHook.Logging;
using System;

namespace XamarinSharpHookTestApp2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly SimpleGlobalHook _hook;

        public MainActivity()
        {
            _hook = new SimpleGlobalHook();

            _hook.HookEnabled += _hook_OnHookEnabled;
            _hook.HookDisabled += _hook_OnHookDisabled;

            _hook.KeyTyped += _hook_KeyTyped;
            _hook.KeyPressed += _hook_KeyPressed;
            _hook.KeyPressed += _hook_KeyReleased;

            System.Diagnostics.Debug.WriteLine($"[dv-trc] ctor - Hook Status= IsRunning:{_hook.IsRunning}, IsDisposed:{_hook.IsDisposed}");
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            _hook.RunAsync();

            System.Diagnostics.Debug.WriteLine($"[dv-trc] OnCreate - Hook Status= IsRunning:{_hook.IsRunning}, IsDisposed:{_hook.IsDisposed}");
        }


        private void _hook_OnMessageLogged(object? sender, LogEventArgs e) => System.Diagnostics.Debug.WriteLine("[dv-trc] _hook_MessageLogged");
        private void _hook_OnHookEnabled(object? sender, HookEventArgs e) => System.Diagnostics.Debug.WriteLine($"[dv-trc] _hook_OnHookEnabled ");
        private void _hook_OnHookDisabled(object? sender, HookEventArgs e) => System.Diagnostics.Debug.WriteLine($"[dv-trc] _hook_OnHookDisabled ");
        private void _hook_KeyReleased(object? sender, KeyboardHookEventArgs e) => System.Diagnostics.Debug.WriteLine($"[dv-trc] _hook_KeyReleased Got a KeyRelease event: KeyCode: {e.Data.KeyCode} KeyChar: {e.Data.KeyChar}");
        private void _hook_KeyPressed(object? sender, KeyboardHookEventArgs e) => System.Diagnostics.Debug.WriteLine($"[dv-trc] _hook_KeyPressed");
        private void _hook_KeyTyped(object? sender, KeyboardHookEventArgs e) =>System.Diagnostics.Debug.WriteLine($"[dv-trc] _hook_KeyTyped");

        private void FabOnClick(object sender, EventArgs eventArgs) =>  System.Diagnostics.Debug.WriteLine($"[dv-trc] FabOnClick - Hook Status= IsRunning:{_hook.IsRunning}, IsDisposed:{_hook.IsDisposed}");

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}