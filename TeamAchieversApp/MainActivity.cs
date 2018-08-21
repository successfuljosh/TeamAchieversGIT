using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Bluetooth;
using Android.Content;
using Java.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamAchieversApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BluetoothAdapter.ILeScanCallback
    {
        TextView textLabel;
        BluetoothAdapter bluetoothAdapter;
        Java.Util.UUID uUID_Secure;

   

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.home);


             uUID_Secure = Java.Util.UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

            BluetoothManager manager = (BluetoothManager)GetSystemService(Context.BluetoothService);
            bluetoothAdapter = manager.Adapter;

            textLabel = (TextView)FindViewById(Resource.Id.textLabel);
        }


        protected override void OnResume()
        {
            base.OnResume();

            //check
            if (bluetoothAdapter==null || !bluetoothAdapter.IsEnabled)
            {
                Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivity(enableIntent);
                Finish();
                return;
            }

            //start Scan
            startScan();
        }

        protected override void OnPause()
        {
            bluetoothAdapter.StopLeScan(this);
        }

   


        private void startScan()
        {
            bluetoothAdapter.StartLeScan(new UUID[] { uUID_Secure }, this);
        }
        private void stopScan()
        {
            bluetoothAdapter.StopLeScan(this);
        }




    public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            //int index = 0;
            //while (index < scanRecord.Length)
            //{
            //    int length = scanRecord[index++];
            //    if (length == 0) break;

            //    int type = scanRecord[index];
            //    if (type == 0) break;

            //    byte[] data = Arrays.CopyOfRange(scanRecord, index + 1, index + length);

            //    index += length;
            //}
            byte[] data = Arrays.CopyOfRange(scanRecord,0,scanRecord.Length);
            float val = (data[0] & 0xff);

            if ((data[0] & 0xff) !=0)
            {
                val += 0.5f;
            }

            textLabel.Text = "Value: " + val;
        }
    }
}

