using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace DysproseTwo.Services
{
    public class ShareService
    {
        // Singleton Pattern with "Lazy"
        private ShareService _shareService = null;
        private static Lazy<ShareService> lazy =
            new Lazy<ShareService>(() => new ShareService());

        public static ShareService Instance => lazy.Value;

        private DataTransferManager _dataTransferManager;

        private string _textToShare = "";

        private ShareService()
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetText(_textToShare);
            request.Data.Properties.Title = "Dysprose: Share the work you've done in this session.";
        }

        public void ShareSessionText(string sessionText)
        {
            if (sessionText != null && sessionText.Length > 0)
            {
                _textToShare = sessionText;
                DataTransferManager.ShowShareUI();
            }
        }

        public static void CopySessionText(string sessionText)
        {
            if (sessionText != null)
            {
                if (sessionText.Length > 0)
                {
                    DataPackage dataPackage = new DataPackage();
                    dataPackage.RequestedOperation = DataPackageOperation.Copy;
                    dataPackage.SetText(sessionText);
                    Clipboard.SetContent(dataPackage);
                }
            }
        }
    }
}
